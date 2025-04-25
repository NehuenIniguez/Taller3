using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MotorBala : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] public float daño;
    [SerializeField] private float tiempoVida;
    public Collider2D balaCol;
    [SerializeField] private GameObject balasBoom;
    [SerializeField] private float velocidadDisparo;
    private bool explotó = false;
    public Collider2D explotion;

    void Start()
    {
        balaCol = GetComponent<Collider2D>();
        explotion = GetComponent<Collider2D>();
        // Destruir después de X segundos por seguridad
        Invoke(nameof(Explotar), tiempoVida); // explota si no colisiona
    }

    public void IgnorarCollider(Collider2D colliderAExcluir)
    {
        if (balaCol != null && colliderAExcluir != null)
        {
            Physics2D.IgnoreCollision(balaCol, colliderAExcluir);
        }
        if (explotion != null && colliderAExcluir != null)
        {
            Physics2D.IgnoreCollision(explotion, colliderAExcluir);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje"))
        {
            VidaPj vida = other.GetComponent<VidaPj>();
            if (vida != null)
                vida.Tomar_Daño(daño);
        }
        //Explotar(); // siempre explota al colisionar con algo

        

       // Destroy(gameObject);
    }
     private void Explotar()
    {
        // Crear efecto visual de explosión
        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);

        // Instanciar balas en abanico
        if (balasBoom != null)
        {
            int cantidadBalas = 3;
            float[] angulos = { -45f, 0f, 45f }; // izquierda, arriba, derecha

            foreach (float angulo in angulos)
            {
                Quaternion rotacion = Quaternion.Euler(0, 0, angulo);
                Vector3 offset = rotacion * new Vector3(0, 0.3f, 0); // para evitar colisión inmediata

                GameObject nuevaBala = Instantiate(balasBoom, transform.position + offset, rotacion);
                
                // Movimiento de la bala
                Rigidbody2D rb = nuevaBala.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direccion = rotacion * Vector2.up;
                    rb.linearVelocity = direccion.normalized * velocidadDisparo;
                }

                // Ignorar colisión con el objeto que explotó
                Collider2D colNuevaBala = nuevaBala.GetComponent<Collider2D>();
                if (balaCol != null && colNuevaBala != null)
                    Physics2D.IgnoreCollision(balaCol, colNuevaBala);
            }
        }

        Destroy(gameObject);
    }
}
