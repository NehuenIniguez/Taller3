using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiempoMuerte : MonoBehaviour
{
    [SerializeField] private float tiempoVida;

    void Start()
    {
        Destroy(gameObject,tiempoVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
