using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public bool run = false;
    public GameObject target;

    public GameObject[] taggedGameObjects;
    private bool followGameObject = false;
    private bool toggleViewType = false;
    private int taggedIndex = 0;

    public float smoothSpeed = 0.125f;
    private Vector3 newPosition = Vector3.zero;
    //float example = 0;

    private float cameraY = 60f;
    private float cameraOffset = 1;
    private float minY = 20;
    private float maxY = 220;
    private float minOffset = 1;
    private float maxOffset = 20;

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
            SetGlobalGameObject("SchoolChildren");
        }
        if (Input.GetKeyDown("w"))
        {
            SetGlobalGameObject("Skateboarder");
        }
        if (Input.GetKeyDown("s"))
        {
            SetGlobalGameObject("Bus");
        }
        if (Input.GetKeyDown("r"))
        {
            SetGlobalGameObject("Jaywalker");
        }
        if (Input.GetKeyDown("t"))
        {
            SetGlobalGameObject("Drone");
        }
        if (Input.GetKeyDown("y"))
        {
            SetGlobalGameObject("Delivery");
        }
        if (Input.GetKeyDown("u"))
        {
            SetGlobalGameObject("Elderly");
        }
        if (Input.GetKeyDown("i"))
        {
            SetGlobalGameObject("Robot");
        }
        if (Input.GetKeyDown("o"))
        {
            SetGlobalGameObject("Motorcycle");
        }
        if (Input.GetKeyDown("p"))
        {
            SetGlobalGameObject("DogWalker");
        }
        if (Input.GetKeyDown("a"))
        {
            SetGlobalGameObject("Riders");
        }
        if (Input.GetKeyDown("e"))
        {
            SetGlobalGameObject("IceCreamTruck");
        }
        if (Input.GetKeyDown("d"))
        {
            SetGlobalGameObject("Wheelchair");
        }
        if (Input.GetKeyDown("f"))
        {
            SetGlobalGameObject("RollerSkates");
        }
        if (Input.GetKeyDown("g"))
        {
            SetGlobalGameObject("Tram");
        }
        if (Input.GetKeyDown("h"))
        {
            SetGlobalGameObject("Runner");
        }
        if (Input.GetKeyDown("j"))
        {
            SetGlobalGameObject("OneWheel");
        }
        if (Input.GetKeyDown("k"))
        {
            SetGlobalGameObject("TrashTruck");
        }
        #endregion

        //scroll for camera distance
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            if (!toggleViewType)
            {
                Debug.Log("scroll: " + Input.mouseScrollDelta);
                cameraY = Mathf.Clamp(cameraY + Input.mouseScrollDelta.y * -2, minY, maxY);
            }
            else
            {
                cameraOffset = Mathf.Clamp(cameraOffset + Input.mouseScrollDelta.y * -0.1f, minOffset, maxOffset);
                Debug.Log("cameraOffset: " + cameraOffset);
            }
        }

        //smoothly move camera
        if(followGameObject)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                NextFollowTarget(-1);
                Debug.Log("Left Arrow" + taggedIndex);              
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                NextFollowTarget(1);
                Debug.Log("Right Arrow:" + taggedIndex);
            }

            //change view type by clicking space
            if (Input.GetKeyDown("space"))
            {
                ChangeViewType();
            }

            newPosition = taggedGameObjects[taggedIndex].transform.position;

            if (toggleViewType)
            {
                Vector3 camPos = taggedGameObjects[taggedIndex].transform.forward * cameraOffset;
                //Vector3 camOffset = new Vector3(0, taggedGameObjects[taggedIndex].transform.localScale.y* cameraOffset, -cameraOffset);
                Vector3 camOffset = new Vector3(0, cameraOffset, 0);
                newPosition = newPosition - camPos + camOffset;
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
        if (!toggleViewType)
        {
            Vector3 towardsTop = new Vector3(0, 0, 1);
            Vector3 smoothedUp = Vector3.Lerp(Camera.main.transform.up, towardsTop, smoothSpeed);
            Camera.main.transform.up = smoothedUp;            
        }
        else
        {
            transform.LookAt(taggedGameObjects[taggedIndex].transform.position, Vector3.up);
        }

        Vector3 clickPosition = Vector3.zero;
        //Left mouse button click example
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Left Click");
            Vector3 hit = GetClickHit("ground");
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

    private GameObject[] SetGameObject(string tag)
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
        if (array.Length > 0)
        {
            followGameObject = true;
            taggedIndex = 0;
        }
        else
        {
            followGameObject = false;
            taggedIndex = 0;
        }
        return array;
    }

    public void SetGlobalGameObject(string tag)
    {
        taggedGameObjects = SetGameObject(tag);
    }

    public void NextFollowTarget(int update)
    {
        if (followGameObject)
        {
            taggedIndex = taggedIndex + update;

            if (taggedIndex < 0)
            {
                taggedIndex = taggedGameObjects.Length - 1;
            }
            if (taggedIndex >= taggedGameObjects.Length)
            {
                taggedIndex = 0;
            }
        }
    }

    public void ChangeViewType()
    {
        if (followGameObject)
        {
            toggleViewType = !toggleViewType;
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
