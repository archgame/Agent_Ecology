using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Transform.rotation example.

// Rotate a GameObject using a Quaternion.
// Tilt the cube using the arrow keys. When the arrow keys are released
// the cube will be rotated back to the center using Slerp.


public class RotateBasicW : MonoBehaviour
{

    float smooth = 5.0f;
    float tiltAngle = 60.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

    }
}
