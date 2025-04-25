using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrazoManager : MonoBehaviour
{
    private int bloquesRestantes;
    private int detectorMuerte;
    [SerializeField] private GameObject explosion;

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
            Explotar();
            Destroy(gameObject); // Opcional: destruir el GameObject del brazo
        }
    }
    public void Explotar()
    {
         // Instanciar la explosión
        if (explosion != null)
        {
            Debug.Log("boom");
            Instantiate(explosion, transform.position, Quaternion.identity);
            
        }
    }
}
