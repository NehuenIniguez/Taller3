using UnityEngine;

public class muerte : MonoBehaviour
{
    [SerializeField] private float tiempo = 0.5f;
    void Start()
    {
        Destroy(gameObject,tiempo);
    }

}
