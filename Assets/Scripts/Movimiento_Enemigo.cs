using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Enemigo : MonoBehaviour
{
    [SerializeField] private float velocidadEnemigo;
    [SerializeField] private float Distancia;
    [SerializeField] private bool Derecha;
    [SerializeField] private Transform SueloEnemigo;  
    [SerializeField]private Vector3 Caja;  
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(SueloEnemigo.position,Caja);
    }
    public void DestruirEnemigo()
    {
        Collider2D[] plataforma = Physics2D.OverlapBoxAll(SueloEnemigo.position, Caja, 0f,LayerMask.GetMask("Piso"));
        foreach (Collider2D col in plataforma)
        {
            if (col.CompareTag("Suelo"))
            {
               
            } else 
            {
                Destroy(gameObject);
            }
        }
    }
    void FixedUpdate() 
    {
        rb.velocity = new Vector2 (velocidadEnemigo *-1, rb.velocity.y);   
        DestruirEnemigo();
        
    }
}
