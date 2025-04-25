using UnityEngine;

public class muerte : MonoBehaviour
{
    float tiempo=0.5f;
    void Start()
    {
        Destroy(gameObject,tiempo);
    }

}
