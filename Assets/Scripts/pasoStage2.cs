using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pasoStage2 : MonoBehaviour
{
 [SerializeField] private float tiempoEspera = 3f; // Tiempo antes de cambiar de escena
    void Start()
    {
        StartCoroutine(CambiarEscenaDespuesDeTiempo());
    }

    IEnumerator CambiarEscenaDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoEspera);
        SceneManager.LoadScene("Level2");
    }
}
