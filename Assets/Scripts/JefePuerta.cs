using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefePuerta : MonoBehaviour
{
    [SerializeField] private float vida = 20f;
    private int brazosDestruidos = 0;
    private bool vulnerable = false;

    public Collider2D colliderDeDaño;
    public Collider2D colliderDeteccion;

    void Start()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            if (col is BoxCollider2D) colliderDeDaño = col;
            if (col is CircleCollider2D) colliderDeteccion = col;
        }
    }

    public void BrazoDestruido()
    {
        brazosDestruidos++;
        if (brazosDestruidos >= 2)
        {
            vulnerable = true;
            colliderDeteccion.enabled = false;
            Debug.Log("¡Puerta vulnerable!");
        }
    }

    public void Tomar_Daño(float daño)
    {
        if (!vulnerable) return;
         

        vida -= daño;
        if (vida <= 0)
        {
            Debug.Log("Puerta destruida");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalaPj"))
        {
            Movimiento_Bala scriptBala = other.GetComponent<Movimiento_Bala>();
            

            if (scriptBala != null)
            {
                float daño = scriptBala.daño;
                Tomar_Daño(daño);
                Destroy(other.gameObject);
            }
        }
    }
}
