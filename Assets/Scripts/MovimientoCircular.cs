using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCircular : MonoBehaviour
{ 
   public Transform centro;
    public float velocidad = 180f;
    public float radio = 2f;
    public bool semicircular = true;

    public float duracionMovimiento = 2f; // tiempo que se mueve
    public float duracionEspera = 1f;     // tiempo detenido

    private float angulo = 0f;
    private float direccion = 1f;
    private float temporizador = 0f;
    private bool movimientoActivo = true;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (movimientoActivo)
        {
            angulo += direccion * velocidad * Time.deltaTime;

            if (semicircular)
            {
                if (angulo > 180f)
                {
                    angulo = 180f;
                    direccion = -1f;
                }
                else if (angulo < 0f)
                {
                    angulo = 0f;
                    direccion = 1f;
                }
            }

            float radianes = angulo * Mathf.Deg2Rad;
            Vector3 posicion = new Vector3(
                centro.position.x + Mathf.Cos(radianes) * radio,
                centro.position.y + Mathf.Sin(radianes) * radio,
                transform.position.z
            );

            transform.position = posicion;

            if (temporizador >= duracionMovimiento)
            {
                movimientoActivo = false;
                temporizador = 0f;
            }
        }
        else
        {
            if (temporizador >= duracionEspera)
            {
                movimientoActivo = true;
                temporizador = 0f;
                direccion *= -1f;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centro.position, radio);
    }
}
