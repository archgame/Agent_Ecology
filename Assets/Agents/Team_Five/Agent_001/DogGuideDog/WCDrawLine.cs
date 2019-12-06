using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WCDrawLine : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRender;
    private float counter;
    private float dist;

    public GameObject origin;
    public GameObject destination;

    public float lineDrawSpeed = 6f;

    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()

    {


        //lineRender.SetPosition(0, origin.position);
        lineRender.SetWidth(0.1f, 0.1f);



        //dist = Vector3.Distance(origin.position, destination.position);

        //if (counter < dist)
        //{
        //counter += .1f / lineDrawSpeed;
        //float x = Mathf.Lerp(0, dist, counter);
        Vector3 pointA = origin.transform.position;
        Vector3 pointB = destination.transform.position;
        Debug.Log("pointA " + origin.name + "," + pointA);
        Debug.Log("pointB " + destination.name + "," + pointB);
        //Debug.DrawLine(pointA, pointB, Color.magenta);
        //Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;
        //Vector3 pointAlongLine = pointA - pointB;
        //lineRender.SetPosition(1, pointAlongLine);
        lineRender.SetPositions(new Vector3[] { pointA, pointB });

        //}
    }
}
