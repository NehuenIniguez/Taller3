using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento_Pj : MonoBehaviour
{
    [Header("Movimiento lateral y salto")]
    public float speed = 100f;
    public float jumpForce = 10f;
    private bool miraDerecha = true;
    private bool salto = false;
    private Rigidbody2D  rb;
    
    [Header("Agacharse")]
    [SerializeField] private BoxCollider2D cajaColision; 
    private Vector2 tamañoOriginal;
    private Vector2 offsetOriginal;
    private bool agacharse = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tamañoOriginal = cajaColision.size;
        offsetOriginal = cajaColision.offset;
    }
    void Update()
    {
        float movimiento = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //transform.Translate(movimiento, 0, 0);
        if (!agacharse )
        {
            rb.velocity = new Vector2(movimiento * speed, rb.velocity.y);
            Pararse();
        }
        if ((movimiento > 0 && !miraDerecha) || (movimiento < 0 && miraDerecha))
        {
            Girar();
        }

        if (Input.GetKeyDown(KeyCode.X) && !salto)
        {
            salto = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetKeyDown("down") && !agacharse)
        {
            Agacharse();
        }
        if (Input.GetKeyUp("down") && agacharse)
        {
            Pararse();
        }
    }
    private void Girar()
    {
        miraDerecha = !miraDerecha;
        transform.eulerAngles = new Vector3(0,miraDerecha ? 0:180, 0);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
       {
            salto = false;
       }
    }
    public void Agacharse()
    {
        agacharse = true;
        cajaColision.size = new Vector2(tamañoOriginal.x, tamañoOriginal.y / 2f); // Reducir altura
        cajaColision.offset = new Vector2(offsetOriginal.x, offsetOriginal.y - (tamañoOriginal.y / 4f)); // Ajustar posición
    }
    public void Pararse ()
    {
        agacharse = false;
        cajaColision.size = tamañoOriginal;
        cajaColision.offset = offsetOriginal;
    }
}
