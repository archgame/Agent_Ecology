using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltSharp : MonoBehaviour
{
    private float turnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.0)
        { //if the right arrow is pressed
            transform.Rotate(0, turnSpeed * Time.deltaTime, -10 * Time.deltaTime, Space.World); //and then turn the plane
        }
        if (Input.GetAxis("Horizontal") < 0.0)
        { //if the left arrow is pressed
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 10 * Time.deltaTime, Space.World); //The X-rotation turns the plane - the Z-rotation tilts it
        }
        if (Input.GetAxis("Vertical") > 0.0)
        { //if the up arrow is pressed
            transform.Rotate(10* Time.deltaTime, 0, 0);
        }
        if (Input.GetAxis("Vertical") < 0.0)
        { //if the down arrow is pressed
            transform.Rotate(-10 * Time.deltaTime, 0, 0);
        }
    }
}
