using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo1 : MonoBehaviour
{
    [SerializeField] private Transform ControlDisparo;
    [SerializeField] private GameObject bala;
    [SerializeField] private float Velocidadbala;
    [SerializeField] private GameObject jugador;
    [SerializeField] private float tiempoEntreDisparos = 1f;
    private float tiempoUltimoDisparo;
    private bool enRango = false;
    //public Animator animator;
    //private AudioSource audioSource;
    //[SerializeField] private AudioClip sonidoDisparo;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Personaje");
        //animator = GetComponent<Animator>();
       // audioSource = GetComponent<AudioSource>();
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
         // --- Animación según dirección ---
    /*if (Mathf.Abs(direccion.x) > Mathf.Abs(direccion.y))
    {
        
        // Movimiento más horizontal
        if (direccion.x > 0)
            //animator.Play("DisparoDerecha");
           
        else
            //animator.Play("DisparoIzquierda");
            // Movimiento más horizontal
        if (direccion.x < 0 && direccion.y > 0)
            //animator.Play("DisparoArribaIzquierda");
           
        else if (direccion.x < 0 && direccion.y < 0)
            //animator.Play("DisparoAbajoIzquierda");
    }
    else
    {
        // Movimiento más vertical
        if (direccion.y > 0)
            //animator.Play("DisparoArriba");
        else
            //animator.Play("DisparoAbajo");
             // Movimiento más vertical
        if (direccion.y > 0 && direccion.x > 0)
            //animator.Play("DisparoArribaDerecha");
        else if (direccion.y <0 && direccion.x > 0)
            //animator.Play("DisparoAbajoDerecha");
    }*/
    // --- Fin animación ---
        GameObject proyectil = Instantiate(bala, ControlDisparo.position, Quaternion.identity);
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        //audioSource.PlayOneShot(sonidoDisparo);
        if (rb != null)
        {
            rb.linearVelocity = direccion * Velocidadbala;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ControlDisparo.position, 10f);
    }
}
