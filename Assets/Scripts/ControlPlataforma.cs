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
        if (Input.GetKey("down") && Input.GetKeyDown(KeyCode.X) && puedePasar || Input.GetKey("down") && Input.GetButton("Fire1") && puedePasar)
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
