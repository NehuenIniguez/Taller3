using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    [SerializeField] private Transform ControlDisparo;
    [SerializeField] private GameObject bala;
    [SerializeField] private float Velocidadbala;
    [SerializeField] private GameObject jugador;
    [SerializeField] private float tiempoEntreDisparos = 1f;
    private float tiempoUltimoDisparo;
    private bool enRango = false;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Personaje");
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje") && !enRango) 
        {
            enRango = true;
            InvokeRepeating(nameof(Disparo), 0f, tiempoEntreDisparos);
            Debug.Log("Entro el pj");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Personaje") && enRango)
        {
            enRango = false;
            CancelInvoke(nameof(Disparo));
        }
    }
   
    public void Disparo()
    {
        if (jugador == null) return;
        Vector2 direccion = (jugador.transform.position - transform.position).normalized;
        GameObject proyectil = Instantiate(bala, ControlDisparo.position, Quaternion.identity);
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direccion * Velocidadbala;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ControlDisparo.position, 10f);
    }
}
