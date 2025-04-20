using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorPiedra : MonoBehaviour
{
    [SerializeField] private GameObject prefabPiedra;
    [SerializeField] private Transform puntosDeSpawn;
    [SerializeField] private float tiempoEntrePiedras = 2f;

    void Start()
    {
        InvokeRepeating("GenerarPiedra", 0f, tiempoEntrePiedras);
    }

    void GenerarPiedra()
    {
        int index = Random.Range(0, puntosDeSpawn.childCount);
        Instantiate(prefabPiedra, puntosDeSpawn.position, Quaternion.identity);
    }
}
