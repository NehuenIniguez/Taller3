using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Movimiento_Pj : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 10;
    private Rigidbody2D  rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float movimiento = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(movimiento, 0, 0);

        if (Input.GetKeyDown(KeyCode.W))
        {
             rb.velocity = new Vector2(rb.velocity.x, jumpForce);;
        }
       
    }
}
