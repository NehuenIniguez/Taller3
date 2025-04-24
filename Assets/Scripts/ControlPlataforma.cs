using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlataforma : MonoBehaviour
{
    private PlatformEffector2D pE2D;
    private bool puedePasar = false;
    

    void Start()
    {
        pE2D = GetComponent<PlatformEffector2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Personaje"))
        {
            puedePasar = true;
        }
    }
    void Update()
    {
        float ye = Input.GetAxisRaw("Vertical");
        if (Input.GetKey("down") && Input.GetKeyDown(KeyCode.X) && puedePasar || ye < 0 && Input.GetButton("Fire2") && puedePasar)
        {
            pE2D.rotationalOffset = 180f;
            puedePasar = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        pE2D.rotationalOffset = 0;
        puedePasar = true;
    }
}
