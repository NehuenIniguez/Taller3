using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlataforma : MonoBehaviour
{
    private PlatformEffector2D pE2D;
    void Start()
    {
        pE2D = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down") && Input.GetKeyDown(KeyCode.X))
        {
            pE2D.rotationalOffset = 180f;
        }
    }
}
