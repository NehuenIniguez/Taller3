using UnityEngine;

public class Block2 : MonoBehaviour
{
    public Transform cameraTransform;

    public float yOffset = -8f; // Ajustá según el alto de tu cámara

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        // Nos posicionamos respecto al eje Y de la cámara
        transform.position = new Vector3(transform.position.x, cameraTransform.position.y + yOffset, transform.position.z);
    }
}
