using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPotenciado : MonoBehaviour
{
    [SerializeField] private Transform ControladorDisparo;
    [SerializeField] private GameObject Bala;
    [SerializeField] private float velocidadBala;
    [SerializeField] private float couldown;
    private float lastShot = 0f;


    private Vector2 ultimaDireccion = Vector2.right;
    void Start()
    {
        GetComponent<DisparoPotenciado>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp2"))
        {
            GetComponent<DisparoPotenciado>().enabled = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PowerUp1"))
        {
            GetComponent<DisparoPotenciado>().enabled = false;
        }
    }
    void Update()
    {
        Vector2 direccion = Vector2.zero;

        // Detectar dirección de disparo
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) direccion += Vector2.up;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) direccion += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) direccion += Vector2.right;
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))  direccion = Vector2.right + Vector2.down;
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) direccion = Vector2.left + Vector2.down;
        
        if (direccion != Vector2.zero && direccion != Vector2.up && direccion != Vector2.down)
        {
            ultimaDireccion = direccion.normalized;
        }

        // Si se presiona el botón de disparo y hay una dirección válida
        if (Input.GetKey(KeyCode.Z) && Time.time >= lastShot + couldown)
        {
            Disparar(direccion != Vector2.zero ? direccion.normalized : ultimaDireccion);
            lastShot = Time.time;
        }
    }

    private void Disparar(Vector2 direccion)
    {
        // Dirección normalizada (ej: Vector2.right o Vector2.left según dónde mire el pj)
        Vector2[] direcciones = new Vector2[]
        {
            (direccion + Vector2.up).normalized,     // diagonal hacia arriba
            direccion.normalized,                    // recto
            (direccion + Vector2.down).normalized     // diagonal hacia abajo
        };

        foreach (Vector2 dir in direcciones)
        {
            GameObject bala = Instantiate(Bala, ControladorDisparo.position, Quaternion.identity);
            Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = dir * velocidadBala;
            }
        }
    }
}
