using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebotePiedra : MonoBehaviour
{
 private Rigidbody2D rb;
    private Collider2D colliderPiedra;

    [SerializeField] private float tiempoDeVida = 10f;
    private bool yaReboto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderPiedra = GetComponent<Collider2D>();

        Destroy(gameObject, tiempoDeVida);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!yaReboto && other.collider.CompareTag("Suelo"))
        {
            yaReboto = true;

            // Después del primer rebote: le sacamos el bounciness
            //colliderPiedra.sharedMaterial = null;

            // (opcional) Podés cambiar también el gravityScale para que caiga más rápido
            //rb.gravityScale = 2f;
            colliderPiedra.isTrigger = true;
        }
        if (other.collider.CompareTag("Personaje"))
        {
            other.collider.GetComponent<VidaPj>().Tomar_Daño(1f);
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
    
        if (yaReboto && other.CompareTag("Suelo"))
        {
            Debug.Log("Hola");
            yaReboto = false;
            // Desactivamos el collider para que no rebote más
            colliderPiedra.isTrigger = false;
          //colliderPiedra.sharedMaterial 
        }
    }

}
