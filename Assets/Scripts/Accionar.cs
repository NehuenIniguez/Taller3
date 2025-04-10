using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accionar : MonoBehaviour
{
    [SerializeField] private GameObject cañon;
    [SerializeField] private GameObject Cañone;
    [SerializeField] private float VidaMaxima;
    [SerializeField] public GameObject gO;
    void Start()
    {
        cañon = GameObject.Find("Cañon1");
        Cañone = GameObject.Find("Cañon2");
        gO = GetComponent<Movimiento_Bala>().gameObject;
    }
    void Update()
    {
        if (cañon == null && Cañone == null)
        {
           Tomar_Daño(gO.GetComponent<Movimiento_Bala>().daño);
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
