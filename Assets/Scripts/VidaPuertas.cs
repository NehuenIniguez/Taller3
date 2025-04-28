using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPuertas : MonoBehaviour
{
   [SerializeField] private float VidaMaxima;
   
    public Collider2D colliderDeDaño; // BoxCollider2D
    public Collider2D colliderDetector; // CircleCollider2D
    private AudioSource audioSource;
    [SerializeField] private AudioClip DañoPuerta;
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
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void Tomar_Daño (float daño)
   {
      VidaMaxima -= daño;
      audioSource.PlayOneShot(DañoPuerta);
      if (VidaMaxima <= 0)
      {
            Muerte();
      }
   }
   private void Muerte()
   {
       
        Destroy(gameObject);
        
        Explotar();
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

