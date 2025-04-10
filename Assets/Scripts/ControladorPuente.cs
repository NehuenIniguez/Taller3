using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPuente : MonoBehaviour
{private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float delayDesaparicion = 0.5f;
    [HideInInspector] public GameObject puenteAnterior;

    private bool yaDesaparecio = false;

    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (puenteAnterior != null)
        {
            if (!puenteAnterior.activeInHierarchy && !yaDesaparecio)
            {
                StartCoroutine(DesaparecerConDelay());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Personaje") && puenteAnterior == null && !yaDesaparecio)
        {
            StartCoroutine(DesaparecerConDelay());
        }
    }

    IEnumerator DesaparecerConDelay()
    {
        yaDesaparecio = true;
        yield return new WaitForSeconds(delayDesaparicion);
        boxCollider2D.enabled = false;
        spriteRenderer.enabled = false;
        gameObject.SetActive(false); // Si querés que desaparezca completamente
    }

}

