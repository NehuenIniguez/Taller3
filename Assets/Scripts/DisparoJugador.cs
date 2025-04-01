using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
   [SerializeField] private Transform ControladorDisparo;
   [SerializeField] private GameObject Bala;
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Disparo();
        }
    }
    private void Disparo() 
    {
        Instantiate(Bala,ControladorDisparo.position, ControladorDisparo.rotation);
    }
}
