using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    public Collider2D colliderDeDaño; // BoxCollider2D
    public Collider2D colliderDetector; // CircleCollider2D
    [SerializeField] private GameObject explosion;

    void Awake()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            if (col is BoxCollider2D)
                colliderDeDaño = col;
            else if (col is CircleCollider2D)
                colliderDetector = col;
        }
        
    }

    public void Tomardaño(float daño)
    {
        vida -= daño;
        if (vida<= 0)
        {
            Debug.Log("le dieron");
            Muerte();
        }
    }
    private void Muerte ()
    {
        Debug.Log("nt");
       Explotar();
       Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        VidaPj vidaPj = other.gameObject.GetComponent<VidaPj>();
        if (other.gameObject.CompareTag("Personaje"))
        {
            
            vidaPj.Tomar_Daño(1f); // Cambia el valor de daño según sea necesario
            
        }
    }
    public void Explotar()
    {
         // Instanciar la explosión
        if (explosion != null)
        {
            Debug.Log("boom");
            Instantiate(explosion, transform.position, Quaternion.identity);
            
        }
        //Destroy(gameObject);
    }
}
