using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WCDrawLine : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRender;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed = 6f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {

        lineRender = GetComponent<LineRenderer>();
        lineRender.SetPosition(0, origin.position);
        lineRender.SetWidth(0.1f, 0.1f);



        dist = Vector3.Distance(origin.position, destination.position);

        if (counter < dist)
        {
            counter += .1f / lineDrawSpeed;
            float x = Mathf.Lerp(0, dist, counter);
            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;

            Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

            lineRender.SetPosition(1, pointAlongLine);
        }
    }
}
