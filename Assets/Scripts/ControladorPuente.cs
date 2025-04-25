using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ControladorPuente : MonoBehaviour
{private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float delayDesaparicion = 0.5f;
    [SerializeField] public GameObject puenteAnterior;
    [SerializeField] private GameObject explosion;

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
        Explotar();
    }
    public void Explotar()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}

