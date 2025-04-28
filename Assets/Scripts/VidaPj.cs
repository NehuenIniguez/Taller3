using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaPj : MonoBehaviour
{
    [Header("Manejo de Vida")]
    [SerializeField] public int vidasMaximas = 2;
    public int vidasActuales;
    [SerializeField] private Image[] medallasUI; // ← arrastrá las dos medallas desde el Canvas
    [SerializeField] private GameObject gameOverUI; // ← arrastrá aquí el objeto con el sprite de "Game Over"
    [SerializeField] private float tiempoEspera = 1f;

    [Header("animacion y pavadas asi")]
    [SerializeField] private Transform puntoReaparicion; // Asignás arriba en el aire en el editor
    [SerializeField] private float tiempoDesaparecido = 0.5f;
    [SerializeField] private GameObject cuerpoVisual; // El GameObject que tiene el sprite y animaciones
    private Movimiento_Pj movimientoScript; // Para desactivar movimiento
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoMuerte;
    void Start()
    {
        vidasActuales = vidasMaximas;
        ActualizarMedallas();
        movimientoScript = GetComponent<Movimiento_Pj>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        cuerpoVisual = GetComponent<SpriteRenderer>().gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    public void Tomar_Daño(float daño)
    {
        vidasActuales--;
        ActualizarMedallas();
        audioSource.PlayOneShot(sonidoMuerte);
        if (vidasActuales <= 0)
        {
            Muerte();
        } else
        {
            
            StartCoroutine(Reaparecer());
            animator.SetBool("Muerte", true);
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
        //Time.timeScale = 0f;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            StartCoroutine(CambioEscena());
        }
    }
    
    IEnumerator CambioEscena()
    {
        yield return new WaitForSecondsRealtime(tiempoEspera);
        SceneManager.LoadScene("GameOver");
    }
    IEnumerator Reaparecer()
    {
        // Retroceso
        
        rb.linearVelocity = new Vector2(movimientoScript.transform.localScale.x > 0 ? -5f : 5f, 5f); // empuja hacia atrás y arriba
         
        
        
        // Desactivar controles
        movimientoScript.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Inmune");
        //col.enabled = false;
        //cuerpoVisual.SetActive(false);

        yield return new WaitForSeconds(tiempoDesaparecido);

        // Reaparecer
        gameObject.layer = LayerMask.NameToLayer("personaje");
        transform.position = puntoReaparicion.position;
        rb.linearVelocity = Vector2.zero; // Evitar seguir con impulso
        movimientoScript.enabled = true;
        col.enabled = true;
        cuerpoVisual.SetActive(true);
        animator.SetBool("Muerte", false);

        // Acá podés resetear power-ups también
    }
}
