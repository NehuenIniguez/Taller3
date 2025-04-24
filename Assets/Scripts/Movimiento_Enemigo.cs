using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento_Enemigo : MonoBehaviour
{
     [SerializeField] private float velocidadEnemigo = 2f;
    [SerializeField] private Transform sueloEnemigo;  
    [SerializeField] private Vector3 caja = new Vector3(0.5f, 0.1f, 0f);  
    [SerializeField] private LayerMask capaSuelo;

    private Rigidbody2D rb;
    private bool mirandoDerecha = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(sueloEnemigo.position, caja);
    }

    void FixedUpdate()
    {
        // Mover en la dirección actual
        float direccion = mirandoDerecha ? 1 : -1;
        rb.linearVelocity = new Vector2(direccion * velocidadEnemigo, rb.linearVelocity.y);

        // Detectar si hay suelo delante
        Collider2D sueloDetectado = Physics2D.OverlapBox(sueloEnemigo.position, caja, 0f, capaSuelo);
        if (sueloDetectado == null)
        {
            // No hay suelo → girar
            Girar();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bloqueo"))
        {
            Debug.Log("Hola");
        }
    }
    void Girar()
    {
        mirandoDerecha = !mirandoDerecha;

        // Invertir la escala en X para voltear el sprite
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    
}
