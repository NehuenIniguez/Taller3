using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPuertas : MonoBehaviour
{
   [SerializeField] private float VidaMaxima;
   
    public Collider2D colliderDeDaño; // BoxCollider2D
    public Collider2D colliderDetector; // CircleCollider2D

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
   public void Tomar_Daño (float daño)
   {
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
