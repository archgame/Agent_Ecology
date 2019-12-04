using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateOnTarget : MonoBehaviour
{
    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    public bool tilt = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tilt == false)
        {
            return;
        }

        if (tilt == true)
        { 
        // Smoothly tilts a transform towards a target rotation.
        //float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        //float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
        

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(60, 0, 60);
        transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);


        // Dampen towards the target rotation
        //transform.position = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Jump"))
        {

            //Debug.Log("Tilting");
            tilt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tilt = false;
        //Debug.Log("Tilting false");
    }
}
