using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Bala : MonoBehaviour
{
   
    [SerializeField] private float daño;
    [SerializeField] private float tiempoVida;

    void Start()
    {
        Destroy(gameObject,tiempoVida);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemigo>().Tomardaño(daño);
            Destroy(gameObject);
        }
    }
}
