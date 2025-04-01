using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform ControladorDisparo;
    [SerializeField] private GameObject Bala;
    [SerializeField] private float velocidadBala;

    void Update()
    {
        Vector2 direccion = Vector2.zero;

        // Detectar dirección de disparo
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) direccion += Vector2.up;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) direccion += Vector2.down;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) direccion += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) direccion += Vector2.right;

        // Si se presiona el botón de disparo y hay una dirección válida
        if (Input.GetKeyDown(KeyCode.Z) && direccion != Vector2.zero)
        {
            Disparar(direccion.normalized);
        }
    }

    private void Disparar(Vector2 direccion)
    {
        GameObject bala = Instantiate(Bala, ControladorDisparo.position, Quaternion.identity);
        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direccion * velocidadBala;
        }
    }
}
