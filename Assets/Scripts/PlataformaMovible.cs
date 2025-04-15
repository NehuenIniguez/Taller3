using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlataformaMovible : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    private bool going;
    private float speed = 10f;
    void Start()
    {
        transform.position = point1.position;
    }

    // Update is called once per frame
    void Update()
    {
     UnityEngine.Vector3 wantedPosition = UnityEngine.Vector3.zero;

     if(going)
     {
        wantedPosition = point2.position;
     }else
     {
        wantedPosition = point1.position;
     }

     UnityEngine.Vector3 direction = wantedPosition - transform.position;
     transform.position += direction.normalized * speed * Time.deltaTime;

     if(direction.magnitude <1)
     {
        going = !going;
     }
    }
}
