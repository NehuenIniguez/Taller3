using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento_Pj : MonoBehaviour
{
    [Header("Movimiento lateral y salto")]
    public float speed = 0.6f;
    public float jumpForce = 30f;
    private bool miraDerecha = true;
    private bool salto = false;
    private Rigidbody2D  rb;
    private Vector2 velocity;
    
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
    }
   void FixedUpdate()
    {
        float movimiento = Input.GetAxisRaw("Horizontal");
        velocity.x = movimiento * speed;

        if (!agacharse)
        {
            Pararse();
        }
        if ((movimiento > 0 && !miraDerecha) || (movimiento < 0 && miraDerecha))
        {
            Girar();
        }

        if (Input.GetKeyDown("down") && !agacharse)
        {
            Agacharse();
            salto = false;
        }

        if (Input.GetKeyDown(KeyCode.X) && !salto && !agacharse)
        {
            salto = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyUp("down") && agacharse)
        {
            Pararse();
        }
        transform.position += (Vector3)(velocity * Time.deltaTime);
        Debug.Log(velocity);
        

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Agua"))
        {
            enAgua = true;
            cajaColision.size = new Vector2(tamañoOriginal.x, tamañoOriginal.y / 2f); // Reducir altura
            cajaColision.offset = new Vector2(offsetOriginal.x, offsetOriginal.y - (tamañoOriginal.y / 4f)); // Ajustar posición
        }   
        if (collision.gameObject.CompareTag("Suelo") || collision.gameObject.CompareTag("Puente"))
        {
            salto = false;
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
