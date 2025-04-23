using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento_Pj : MonoBehaviour
{
    public Animator animator;
    [Header("Movimiento lateral y salto")]

    public float speed;
    public float jumpForce;
    private bool miraDerecha = true;
    private bool salto = false;
    private Rigidbody2D  rb;
    private Vector2 velocity;
    private bool enSuelo = false;
    
    [Header("Agacharse")]
    [SerializeField] private BoxCollider2D cajaColision; 
    private Vector2 tamañoOriginal;
    private Vector2 offsetOriginal;
    private bool agacharse = false;

    [Header ("Detectar suelo para pasar de largo")]
    [SerializeField] private LayerMask suelo;

    [Header("Mecanica del agua")]
    [SerializeField] private Transform detectorEscaladaIzq;
    [SerializeField] private Transform detectorEscaladaDer;
    [SerializeField] private float distanciaLateral = 0.2f;
    [SerializeField] private float alturaEscalada = 1.5f;
    [SerializeField] private LayerMask plataformaEscalable;
    private bool enAgua = false;
    private bool puedeEscalar = true; // Evita escalar muchas veces seguidas
    [SerializeField] private float cooldownEscalada = 0.5f;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tamañoOriginal = cajaColision.size;
        offsetOriginal = cajaColision.offset;
        animator = GetComponent<Animator>();
        animator.SetBool("Agacharse",false);
    }
   void Update()
    {
        float movimiento = Input.GetAxisRaw("Horizontal");
        float move = Input.GetAxisRaw("Vertical");

        velocity.x = movimiento * speed;
        if (movimiento != 0)
        {
            animator.SetFloat("Caminar",1f);
        }
        else
        {
            animator.SetFloat("Caminar",0f);
        }

        if (!agacharse)
        { 
            Pararse();
        }
        if ((movimiento > 0 && !miraDerecha) || (movimiento < 0 && miraDerecha))
        {
            Girar();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !agacharse || move < 0 && !agacharse)
        {
            Debug.Log("Se ejecuta esto");
            animator.SetBool("Agacharse",true);
            Agacharse();
            salto = false;
        }

        if (Input.GetKeyDown(KeyCode.X) && !salto && !agacharse && enSuelo || Input.GetButtonDown("Jump") && !salto && !agacharse && enSuelo)
        {
            salto = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && agacharse || Input.GetButtonUp("Jump") && agacharse)
        {
            animator.SetBool("Agacharse",false);
            Pararse();
        }
        transform.position += (Vector3)(velocity * Time.deltaTime);
        //Debug.Log(velocity);
        

        if (enAgua && puedeEscalar)
        {
            bool escalableLadoIzq = Physics2D.Raycast(detectorEscaladaIzq.position, Vector2.left, distanciaLateral, plataformaEscalable);
            bool escalableLadoDer = Physics2D.Raycast(detectorEscaladaDer.position, Vector2.right, distanciaLateral, plataformaEscalable);

            if ((escalableLadoIzq && movimiento < 0) || (escalableLadoDer && movimiento > 0))
            {
                Vector3 direccion = escalableLadoIzq ? new Vector3(-1, 1, 0) : new Vector3(1, 1, 0);
                transform.position += direccion.normalized * alturaEscalada;

                puedeEscalar = false;
                Invoke(nameof(HabilitarEscalada), cooldownEscalada);
            }
        }
    }

    private void Girar()
    {
        miraDerecha = !miraDerecha;
        transform.eulerAngles = new Vector3(0,miraDerecha ? 0:180, 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo") || other.gameObject.CompareTag("Puente"))
        {
            salto = false;
            enSuelo = true;
            enAgua = false;
        }
        if (other.gameObject.CompareTag("Agua"))
        {
            Debug.Log("en agua");
            enAgua = true;
            cajaColision.size = new Vector2(tamañoOriginal.x, tamañoOriginal.y / 2f); // Reducir altura
            cajaColision.offset = new Vector2(offsetOriginal.x, offsetOriginal.y - (tamañoOriginal.y / 4f)); // Ajustar posición
        }    
    }
     void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Suelo") || other.gameObject.CompareTag("Puente"))
        {
            enSuelo = false;
        }
        if (other.gameObject.CompareTag("Agua"))
        {
            Debug.Log("salio del agua");
            enAgua = false;
            cajaColision.size = tamañoOriginal; // Restaurar altura
            cajaColision.offset = offsetOriginal; // Restaurar posición
        }
    }
    public void Agacharse()
    {
        
        agacharse = true;
        cajaColision.size = new Vector2(tamañoOriginal.x, tamañoOriginal.y / 2f); // Reducir altura
        cajaColision.offset = new Vector2(offsetOriginal.x, offsetOriginal.y - (tamañoOriginal.y / 4f)); // Ajustar posición
        
    }
    public void Pararse ()
    {
        animator.SetBool("Agacharse",false);
        agacharse = false;
        cajaColision.size = tamañoOriginal;
        cajaColision.offset = offsetOriginal;
    }
    private void HabilitarEscalada()
    {
        puedeEscalar = true;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectorEscaladaIzq.position, detectorEscaladaIzq.position + Vector3.left * distanciaLateral);
    }

}
