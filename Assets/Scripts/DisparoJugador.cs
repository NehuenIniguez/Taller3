using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform ControladorDisparo;
    [SerializeField] private GameObject Bala;
    [SerializeField] private float velocidadBala;
    public Animator animator;
    private Vector2 ultimaDireccion = Vector2.right;

    void Start()
    {
        GetComponent<DisparoPotencido>().enabled = true;
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp1") || other.CompareTag("PowerUp2"))
        {
           GetComponent<DisparoJugador>().enabled = false;
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 direccion = Vector2.zero;

        // Detectar dirección de disparo
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || vertical > 0) direccion += Vector2.up;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || horizontal < 0) direccion += Vector2.left;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || horizontal > 0) direccion += Vector2.right;
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) || vertical < 0 && horizontal > 0)  direccion = Vector2.right + Vector2.down;
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) || vertical < 0 && horizontal < 0) direccion = Vector2.left + Vector2.down;
        
        if (direccion != Vector2.zero && direccion != Vector2.up && direccion != Vector2.down)
        {
            ultimaDireccion = direccion.normalized;
        }

        // Si se presiona el botón de disparo y hay una dirección válida
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("joystick button 2") )
        {
           
            Disparar(direccion != Vector2.zero ? direccion.normalized : ultimaDireccion);
            

        }
       
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("joystick button 2"))
        {
            animator.SetBool("Dispara",true);
        }
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp("joystick button 2"))
        {
            animator.SetBool("Dispara", false);
        }
    }

    private void Disparar(Vector2 direccion)
    {
        GameObject bala = Instantiate(Bala, ControladorDisparo.position, Quaternion.identity);
        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direccion * velocidadBala;
        }
    }
}
