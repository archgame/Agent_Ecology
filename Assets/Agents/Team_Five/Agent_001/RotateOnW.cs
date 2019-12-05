using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Rotate a GameObject using a Quaternion.
// Tilt the cube using the arrow keys. When the arrow keys are released
// the cube will be rotated back to the center using Slerp.


public class RotateOnW : MonoBehaviour
{

    float totalTime = 0.5f;
    float tiltAngle = 25f;

    public float rotationChange;
    public float tolerance;
    float rotationLast;
    public float timer;
    public float returnTimer;
    public Vector3 target;
    float counterLast;
    float lastTiltAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotationLast = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<WheelChair>().counter != counterLast)
        {
            timer = 0;
        }

        //rotationChange = Mathf.Round((transform.localEulerAngles.y - rotationLast) * 10000f) / 10000f;

        //to turn left
        if (GetComponent<WheelChair>().counter % 6 == 1)
        {
            timer = timer + Time.deltaTime;
            //returnTimer = 0;
            target = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y, tiltAngle);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, target, timer / totalTime);
            lastTiltAngle = tiltAngle;
        }


        //to turn right
        else if (GetComponent<WheelChair>().counter % 6 == 4)
        {
            timer = timer + Time.deltaTime;
            //returnTimer = 0;
            target = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -tiltAngle);
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, target, timer / totalTime);
            lastTiltAngle = -tiltAngle;
        }

        //if there is no turn
        else
        {
            timer = timer + Time.deltaTime;
            //returnTimer = 0;
            target = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, lastTiltAngle);
            Debug.Log("z" + transform.localEulerAngles.z);
            transform.localEulerAngles = Vector3.Lerp(target, transform.localEulerAngles, timer / totalTime);
        }
        /*{
            // Smoothly tilts a transform towards a target rotation.
            float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }*/

        counterLast = GetComponent<WheelChair>().counter;
        //rotationLast = transform.localEulerAngles.y;
    }
}
