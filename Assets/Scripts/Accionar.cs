using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Accionar : MonoBehaviour
{
    [SerializeField] private GameObject cañon;
    [SerializeField] private GameObject Cañone;
    [SerializeField] private float VidaMaxima;
    
    public Collider2D colliderDeDaño; // BoxCollider2D
    public Collider2D colliderDetector; // CircleCollider2D
    private bool vulnerable = false;
    private AudioSource audioSource;
    [SerializeField] private AudioClip Explosion;
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private float tiempoEspera;

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
        cañon = GameObject.Find("Cañon1");
        Cañone = GameObject.Find("Cañon2");
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!vulnerable && cañon == null && Cañone == null)
        {
           vulnerable=true;
        }
    }
    public void Tomar_Daño (float daño)
    {
        if(!vulnerable) return;
        VidaMaxima -= daño;
        if (VidaMaxima <= 0)
        {
            Muerte();
        }
    }
        private void Muerte()
    {
        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        Explotar(); // Primero explotás
        audioSource.PlayOneShot(Explosion);
        yield return new WaitForSecondsRealtime(tiempoEspera); // Esperás
        SceneManager.LoadScene("PasoLevel2"); // Cambiás de escena
        Destroy(gameObject); // (opcional) podés destruirlo después, o ni hace falta si ya cambiás de escena
    }
    private void Explotar()
    {
          // Instanciar la explosión
        if (ExplosionPrefab != null)
        {
            Debug.Log("boom");
            
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            
        }
    }
    
}
