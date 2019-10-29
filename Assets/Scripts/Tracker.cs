using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public bool run = false;
    public GameObject target;

    private GameObject[] taggedGameObjects;
    bool followGameObject = false;
    bool toggleViewType = false;

    public float smoothSpeed = 0.125f;
    private Vector3 newPosition = Vector3.zero;
    //float example = 0;

    float cameraY = 60f;
    float minY = 20;
    float maxY = 220;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        newPosition = Camera.main.transform.position;
        Camera.main.transform.forward = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        #region FOLLOW BY TAG
        //x key click example
        if (Input.GetKeyDown("q"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("SchoolChildren");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("w"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Skateboarder");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("e"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Bus");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("r"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Jaywalker");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("t"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Drone");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("y"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Delivery");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("u"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Elderly");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("i"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Robot");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("o"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Motorcycle");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("p"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("DogWalker");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("a"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Scooter");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("s"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("IceCreamTruck");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("d"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Wheelchair");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("f"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("RollerSkates");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("g"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Tram");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("h"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("Runner");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("j"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("OneWheel");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        if (Input.GetKeyDown("k"))
        {
            taggedGameObjects = GameObject.FindGameObjectsWithTag("TrashTruck");
            if (taggedGameObjects.Length > 0)
            {
                followGameObject = true;
            }
        }
        #endregion

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            Debug.Log("scroll: " + Input.mouseScrollDelta);
            cameraY = Mathf.Clamp(cameraY + Input.mouseScrollDelta.y*-2, minY, maxY);
        }

        //smoothly move camera
        if(followGameObject)
        {                    
            //change view type by clicking space
            if (Input.GetKeyDown("space"))
            {
                toggleViewType = !toggleViewType;
            }

            newPosition = taggedGameObjects[0].transform.position;

            if (toggleViewType)
            {
                newPosition = newPosition 
                    - (taggedGameObjects[0].transform.forward * taggedGameObjects[0].transform.localScale.z)
                    + new Vector3(0, taggedGameObjects[0].transform.localScale.y, 0);
            }
        }

        Vector3 cameraPosition = Camera.main.transform.position;

        if (!toggleViewType)
        {
            newPosition.y = cameraY;
        }
        Vector3 smoothedPosition = Vector3.Lerp(cameraPosition, newPosition, smoothSpeed);      
        Camera.main.transform.position = smoothedPosition;

        //look at gameobject
        //if(followGameObject)
        //{
            if (!toggleViewType)
            {
                //transform.LookAt(taggedGameObjects[0].transform, new Vector3(0, 1, 0));
                Vector3 towardsTop = new Vector3(0, 0, 1);
                Vector3 smoothedUp = Vector3.Lerp(Camera.main.transform.up, towardsTop, smoothSpeed);
                Camera.main.transform.up = smoothedUp;
                
                //Camera.main.transform.up = towardsTop;
                //Debug.DrawRay(Camera.main.transform.position, towardsTop * 10, Color.red);
            }
            else
            {
                transform.LookAt(taggedGameObjects[0].transform, Vector3.up);
            }
        //}

        Vector3 clickPosition = Vector3.zero;
        //Left mouse button click example
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Left Click");
            //Vector3 hit = GetClickHit("ground");
            //if(hit != Vector3.zero)
            //{
                //target.transform.position = hit;
            //}
        }

        //Right mouse button click example
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right Click");
            Vector3 hit = GetClickHit("ground");
            if (hit != Vector3.zero)
            {
                newPosition = hit;
                followGameObject = false;
                toggleViewType = false;
            }
        }

        //Middle mouse button click example
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Middle Click");
        }

    }

    Vector3 GetClickHit(string tag)
    {
        //creates a ray from the camera
        Vector3 screenPoint = Input.mousePosition;
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

}
