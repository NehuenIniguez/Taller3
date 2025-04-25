using UnityEngine;

public class BarrilPowerUp : MonoBehaviour
{
   [Header("Movimiento")]
    [SerializeField] private float amplitudY = 1.5f;
    [SerializeField] private float velocidadY = 3f;
    [SerializeField] private float velocidadX = 2f;
    [SerializeField] private float distanciaX = 5f;

    [Header("Explosión y Power-Up")]
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject powerUp;

    [Header("Vida")]
    [SerializeField] private float vida = 1f;

    private Vector3 posicionInicial;
    private float tiempoInicio;

    void Start()
    {
        posicionInicial = transform.position;
        tiempoInicio = Time.time;
    }

    void Update()
    {
        // Movimiento horizontal constante
        float desplazamientoX = Mathf.PingPong((Time.time - tiempoInicio) * velocidadX, distanciaX);
        
        // Movimiento vertical tipo seno (sube y baja mientras se mueve en X)
        float offsetY = Mathf.Sin((Time.time - tiempoInicio) * velocidadY) * amplitudY;

        transform.position = posicionInicial + new Vector3(desplazamientoX, offsetY, 0f);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Explota();
        }
    }

    private void Explota()
    {
        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);

        if (powerUp != null)
            Instantiate(powerUp, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalaPj"))
        {
            TomarDaño(1f);
            Destroy(other.gameObject);
        }
    }
}
