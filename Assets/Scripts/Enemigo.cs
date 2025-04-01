using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    

   
    public void Tomardaño(float daño)
    {
        vida -= daño;
        if (vida<= 0)
        {
            Muerte();
        }
    }
    private void Muerte ()
    {
        Destroy(gameObject);
    }
}
