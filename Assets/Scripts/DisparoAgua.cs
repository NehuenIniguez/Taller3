using System;
using UnityEngine;

public class DisparoAgua : MonoBehaviour
{
    [SerializeField] private Transform ControlDisparo;
    [SerializeField] private GameObject bala;
    [SerializeField] private float Velocidadbala;
    [SerializeField] private float tiempoEntreDisparos = 1f;
    private float tiempoUltimoDisparo;
    private void Update() {
        Disparo();
    }
    public void Disparo()
    {
        if (Time.time - tiempoUltimoDisparo >= tiempoEntreDisparos)
        {
            GameObject proyectil = Instantiate(bala, ControlDisparo.position, Quaternion.identity);
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = ControlDisparo.up * Velocidadbala;
            }
            tiempoUltimoDisparo = Time.time;
        }
    }
}
