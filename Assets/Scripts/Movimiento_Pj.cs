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

    private bool estaMuerto= false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tamañoOriginal = cajaColision.size;
        offsetOriginal = cajaColision.offset;
        animator = GetComponent<Animator>();
        animator.SetBool("Agacharse",false);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            estaMuerto = true;
            //animator.SetBool("Muerte",true);
            animator.SetFloat("Salto", 0f);   // Desactivar animación de salto
            animator.SetFloat("Caminar", 0f);
        }
    }
   void Update()
    {
        if (estaMuerto == true) 
        {
            return;
        }    // Obtener input de movimiento
        float movimiento = Input.GetAxisRaw("Horizontal");
        float move = Input.GetAxisRaw("Vertical");

        velocity.x = movimiento * speed;

        // 🔺 PRIORIDAD 1: Saltar
        if (Mathf.Abs(rb.linearVelocity.y) > 0.1f)  // Verifica si el personaje está en el aire (saltando o cayendo)
        {
            animator.SetFloat("Salto", 1f);   // Activar animación de salto
            animator.SetFloat("Caminar", 0f); // Desactivar animación de caminar
            animator.SetBool("Agacharse", false); // Desactivar animación de agacharse
        }
        // 🔺 PRIORIDAD 2: Agacharse
        else if (agacharse)  // Si el personaje está agachado
        {
            animator.SetFloat("Salto", 0f);   // Desactivar animación de salto
            animator.SetFloat("Caminar", 0f); // Desactivar animación de caminar
            animator.SetBool("Agacharse", true); // Activar animación de agacharse
        }
        // 🔺 PRIORIDAD 3: Caminar
        else if (movimiento != 0)  // Si el personaje está caminando
        {
            animator.SetFloat("Caminar", 1f); // Activar animación de caminar
            animator.SetFloat("Salto", 0f);   // Desactivar animación de salto
            animator.SetBool("Agacharse", false); // Desactivar animación de agacharse
        }
        // 🔺 PRIORIDAD 4: Idle (si no se está moviendo)
        else
        {
            animator.SetFloat("Caminar", 0f);   // Desactivar animación de caminar
            animator.SetFloat("Salto", 0f);     // Desactivar animación de salto
            animator.SetBool("Agacharse", false); // Desactivar animación de agacharse
        }

        // Si el personaje está parado y no agachado, lo dejamos de agachar
        if (!agacharse)
        { 
            Pararse();
        }

        // Girar al personaje si es necesario
        if ((movimiento > 0 && !miraDerecha) || (movimiento < 0 && miraDerecha))
        {
            Girar();
        }

        // Logica para agacharse
        if (Input.GetKeyDown(KeyCode.DownArrow) && !agacharse && rb.linearVelocity.y == 0 || move < 0 && !agacharse && rb.linearVelocity.y == 0)
        {
            Debug.Log("Se ejecuta esto");
            Agacharse();
            salto = false;
        }

        // Lógica para dejar de agacharse
        if (Input.GetKeyUp(KeyCode.DownArrow) && agacharse || move >= 0 && agacharse)
        {
            animator.SetBool("Agacharse", false);
            Pararse();
        }

        // Movimiento del personaje
        transform.position += (Vector3)(velocity * Time.deltaTime);

        // Si el personaje está en el aire, mostrar animación de salto
        if (rb.linearVelocity.y != 0f)
        {
            animator.SetFloat("Salto", 1f);
        }
        else
        {
            animator.SetFloat("Salto", 0f);
        }

        // Lógica de agua y escalada (sin cambios)
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
    
    private void FixedUpdate() 
    {
        if (Input.GetKeyDown(KeyCode.X) && !salto && !agacharse && enSuelo || Input.GetButton("Fire2") && !salto && !agacharse && enSuelo)
        {
            salto = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void Girar()
    {
        miraDerecha = !miraDerecha;
        transform.eulerAngles = new Vector3(0,miraDerecha ? 0:180, 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo") )
        {
            salto = false;
            enSuelo = true;
            enAgua = false;
        }
        if (other.gameObject.CompareTag("Puente"))
        {
            salto = false;
            enSuelo = true;
            enAgua = false;
        }
        if (other.gameObject.CompareTag("Agua"))
        {
            animator.SetBool("Nado",true);
            Debug.Log("en agua");
            cajaColision.size = new Vector2(tamañoOriginal.x, tamañoOriginal.y / 2f); // Reducir altura
            cajaColision.offset = new Vector2(offsetOriginal.x, offsetOriginal.y - (tamañoOriginal.y / 4f)); // Ajustar posición
            enAgua = true;
        }    
    }
     void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Suelo") || other.gameObject.CompareTag("Puente"))
        {
            enSuelo = false;
        }
        if (other.gameObject.CompareTag("Agua"))
        {
            animator.SetBool("Nado",false);
            Debug.Log("salio del agua");
            cajaColision.size = tamañoOriginal; // Restaurar altura
            cajaColision.offset = offsetOriginal; // Restaurar posición
            enAgua = false;
        }
    }
    public void Agacharse()
    {
        animator.SetBool("Agacharse", true);
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
        Gizmos.DrawLine(detectorEscaladaDer.position, detectorEscaladaDer.position + Vector3.left * distanciaLateral);
    }
    

}
