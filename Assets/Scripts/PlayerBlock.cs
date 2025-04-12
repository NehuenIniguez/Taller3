using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public Transform cameraTransform;

    public float xOffset = -8f; // Ajustá según el ancho de tu cámara

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Nos posicionamos a la izquierda de la cámara
        transform.position = new Vector3(cameraTransform.position.x + xOffset, transform.position.y, transform.position.z);
    }
 
}
