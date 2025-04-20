using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebotePiedra : MonoBehaviour
{
    private bool yaReboto = false;
    private PlatformEffector2D effector;
    private Rigidbody2D rb;  
    [SerializeField] private float tiempoDeVida = 10f;


    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        rb = GetComponent<Rigidbody2D>();
        
        Destroy(gameObject, tiempoDeVida);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!yaReboto && collision.collider.CompareTag("Suelo"))
        {
            yaReboto = true;
            Invoke("DesactivarEffector", 0.1f); // Pequeña demora para evitar bugs
        }
    }

    void DesactivarEffector()
    {
        effector.rotationalOffset = 0f;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Suelo"))
        {
            yaReboto = false;
            effector.rotationalOffset =180f;
        }
    }
}
