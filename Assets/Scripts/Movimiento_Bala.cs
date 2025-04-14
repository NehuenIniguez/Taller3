using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Bala : MonoBehaviour
{
   
    [SerializeField] public float daño;
    [SerializeField] private float tiempoVida;
    private Collider2D balaCol;


    void Start()
    {
        Collider2D balaCol = GetComponent<Collider2D>();
      
        Destroy(gameObject,tiempoVida);
    }
     public void IgnorarCollider(Collider2D colliderAExcluir)
    {
        if (balaCol != null && colliderAExcluir != null)
        {
            Physics2D.IgnoreCollision(balaCol, colliderAExcluir);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemigo"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null && enemigo.colliderDeDaño == other)
            {
                enemigo.Tomardaño(daño);
                Destroy(gameObject);
            } else if (enemigo == null)
            {
                enemigo = other.GetComponentInParent<Enemigo>();
            }
        }
    }
}
