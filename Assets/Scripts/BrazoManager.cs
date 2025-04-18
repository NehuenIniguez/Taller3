using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoManager : MonoBehaviour
{
    private int bloquesRestantes;
    private int detectorMuerte;

    private void Start()
    {
        bloquesRestantes = transform.childCount;
        detectorMuerte = transform.childCount;
    }

    public void BloqueDestruido()
    {
        bloquesRestantes--;

        if (bloquesRestantes != detectorMuerte)
        {
            Debug.Log("Brazo destruido");
            FindObjectOfType<JefePuerta>().BrazoDestruido();
            Destroy(gameObject); // Opcional: destruir el GameObject del brazo
        }
    }
}
