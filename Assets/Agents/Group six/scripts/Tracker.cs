using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public bool run = false;
    public GameObject Target;
    public float smoothspeed = 0.125f;
    private Vector3 newPosition = Vector3.zero;
    // float example=0;
    float cameraY = 60f;
    float minY = 10;
    float maxY = 200;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        newPosition = Camera.main.transform.position;
        //Debug.Log("addition example: " + CoolAddition(2, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.mouseScrollDelta != Vector2.zero)
        {
            Debug.Log("scroll; " + Input.mouseScrollDelta);
            cameraY = Mathf.Clamp(cameraY + Input.mouseScrollDelta.y*-5, minY, maxY);
        }
        // smooth move camera
        Vector3 cameraPosition = Camera.main.transform.position;
        newPosition.y = cameraY;
        Vector3 smoothedPosition = Vector3.Lerp(cameraPosition, newPosition, smoothspeed);
        Camera.main.transform.position = smoothedPosition;

        Vector3 clickPosition = Vector3.zero;

        //Left mouse botton click
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left click");
            Vector3 hit = GetClickHit("ground");
            if (hit !=Vector3.zero)
            {
                Target.transform.position = hit;
            }
            //Debug.Log("screenPoint: " + screenPoint);
           // Ray ray = Camera.main.ScreenPointToRay(screenPoint);
            ///Debug.DrawRay(ray.origin, ray.direction * 100, Color.red,1);

        }
        //Right click mouse botton click
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right click");

            Vector3 hit = GetClickHit("ground");
            if (hit !=Vector3.zero)
            {
                newPosition = hit;
            }
          
        }
        Vector3 GetClickHit(string tag)
        {
            //create a ray from the camera

            Vector3 screenPoint = Input.mousePosition;
           // Debug.Log("screenPoint: " + screenPoint);
            Ray ray = Camera.main.ScreenPointToRay(screenPoint);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
            foreach (RaycastHit hit in hits)
            {
                Debug.Log("layer: " + LayerMask.NameToLayer(tag));
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer(tag))
                {
                    return hit.point;
                }
            }
            return Vector3.zero;
        }
        //Space click
    }
}
