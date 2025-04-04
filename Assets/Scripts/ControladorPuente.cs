using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPuente : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    private bool enRango = false;
    [SerializeField] private GameObject jugador;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        jugador = GameObject.FindGameObjectWithTag("Personaje");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje") && !enRango) 
        {
            enRango = true;
            boxCollider2D.enabled = false;
            spriteRenderer.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
