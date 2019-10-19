using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningLight : MonoBehaviour
{
    Light frontLight;
    // Start is called before the first frame update
    void Start()
    {
        frontLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            frontLight.enabled = !frontLight.enabled;
        }
    }
}
