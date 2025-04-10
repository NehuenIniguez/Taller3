using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiempoMuerte : MonoBehaviour
{
    [SerializeField] private float tiempoVida;
    public float daño;

    void Start()
    {
        Destroy(gameObject,tiempoVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Personaje"))
        {
            collision.GetComponent<VidaPj>().Tomar_Daño(daño);
            Destroy(gameObject);
        }
    }
}
