using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accionar : MonoBehaviour
{
    [SerializeField] private GameObject cañon;
    [SerializeField] private GameObject Cañone;
    [SerializeField] private float VidaMaxima;
    
    public Collider2D colliderDeDaño; // BoxCollider2D
    public Collider2D colliderDetector; // CircleCollider2D
    private bool vulnerable = false;

    void Awake()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            if (col is BoxCollider2D)
                colliderDeDaño = col;
            else if (col is CircleCollider2D)
                colliderDetector = col;
        }
    }
    void Start()
    {
        cañon = GameObject.Find("Cañon1");
        Cañone = GameObject.Find("Cañon2");
        
    }
    void Update()
    {
        if (!vulnerable && cañon == null && Cañone == null)
        {
           vulnerable=true;
        }
    }
    public void Tomar_Daño (float daño)
    {
        if(!vulnerable) return;
        VidaMaxima -= daño;
        if (VidaMaxima <= 0)
        {
            Muerte();
        }
    }
    private void Muerte()
    {
        Destroy(gameObject);
    }
}
