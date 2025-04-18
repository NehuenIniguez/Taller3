using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloqueEnemigo : MonoBehaviour
{
    [SerializeField] private float vida = 5f;

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            GetComponentInParent<BrazoManager>().BloqueDestruido(); // Avisa al brazo
            Destroy(gameObject); // Se destruye a sí mismo
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BalaPj"))
        {
            float daño = other.GetComponent<Movimiento_Bala>().daño;
            TomarDaño(daño);
            Destroy(other.gameObject);
        }
    }
}
