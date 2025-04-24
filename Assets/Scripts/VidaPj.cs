using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPj : MonoBehaviour
{
   [SerializeField] private int vidasMaximas = 2;
    private int vidasActuales;

    [SerializeField] private Image[] medallasUI; // ← arrastrá las dos medallas desde el Canvas
    [SerializeField] private GameObject gameOverUI; // ← arrastrá aquí el objeto con el sprite de "Game Over"

    void Start()
    {
        vidasActuales = vidasMaximas;
        ActualizarMedallas();
    }

    public void Tomar_Daño(float daño)
    {
        vidasActuales--;
        ActualizarMedallas();

        if (vidasActuales <= 0)
        {
            Muerte();
        }
    }

    private void ActualizarMedallas()
    {
        for (int i = 0; i < medallasUI.Length; i++)
        {
            medallasUI[i].enabled = i < vidasActuales;
        }
    }

    private void Muerte()
    {
        //Destroy(gameObject);
        //SceneManager.LoadScene("GameOver");
        Time.timeScale = 0f;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);
    }
}
