using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPj : MonoBehaviour
{
   [SerializeField] private float VidaMaxima;
   
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
