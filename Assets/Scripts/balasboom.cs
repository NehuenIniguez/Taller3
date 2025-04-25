
using Unity.Mathematics;
using UnityEngine;

public class balasBoom : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] public float daño;
    [SerializeField] private float tiempoVida;
    public Collider2D balaCol;
     public Collider2D explotion;
    
    void Start()
    {
        balaCol = GetComponent<Collider2D>();
        explotion = GetComponent<Collider2D>();
        // Destruir después de X segundos por seguridad
      Invoke(nameof(Explotar), tiempoVida);
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
        Debug.Log(other.name);
        if (other.CompareTag("Personaje"))
        {
            VidaPj vida = other.GetComponent<VidaPj>();
            if (vida != null)
                vida.Tomar_Daño(daño);
        }

        // Instanciar la explosión
        if (explosion != null)
        {
            Debug.Log("boom");
            Instantiate(explosion, transform.position, Quaternion.identity);
            
        }

        //Destroy(gameObject);
    }
    public void Explotar()
    {
        // Instanciar la explosión
        if (explosion != null)
        {
            Debug.Log("boom");
            Instantiate(explosion, transform.position, Quaternion.identity);
            
        }
        Destroy(gameObject);
    }
}
