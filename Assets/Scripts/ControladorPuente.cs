using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPuente : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float tiempoDeEspera;
    private float coolDownPuente;
    private bool enRango = false;
    [SerializeField] private GameObject jugador;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        jugador = GameObject.FindGameObjectWithTag("Personaje");
        spriteRenderer = GetComponent<SpriteRenderer>();
        coolDownPuente = Time.deltaTime;
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje") && !enRango && Time.deltaTime>= coolDownPuente + tiempoDeEspera ) 
        {
            enRango = true;
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            coolDownPuente = Time.deltaTime;
        }
    }
}
