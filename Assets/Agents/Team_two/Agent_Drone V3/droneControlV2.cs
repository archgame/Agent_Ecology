using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneControlV2 : MonoBehaviour
{

    public GameObject drone;
    public float speed = 1.0f;
    public GameObject[] targets;
    private int t;

    public int maximumAddHeight = 15;
    public int maximumReduceHeight = 5;
    private float addHeight = 0;
    private float h = 0.01f;

    Transform target;
    public float changeTargetDistance = 1;

    public Vector3 hangingpoint;
    //private Vector3 localtargetpoint;
    //private Vector3 localdronepoint;

    private bool hover = false;

    public bool finishTour = false;

    public Vector3 prePoition;
    public Vector3 rotateTo;
    public Vector3 newDir;

    //internal Vector3 localdronepoint;

    void Start()
    {
        
        

    }

    void Update()
    {
        if (gameObject.GetComponent<droneControlV1>().getheight == true)
        {
            gameObject.GetComponent<droneControlV1>().keyHit = false;
            gameObject.GetComponent<droneControlV1>().enabled = false;

            //hangingpoint = GetComponent<droneControlV1>().drone.transform.position;
            
            if (Input.GetKeyDown(KeyCode.B))
            {
                addHeight =+ h;
            }

            if ((hangingpoint.y + addHeight) >= maximumAddHeight)
            {
                addHeight = 0;
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                addHeight =- h;
            }

            if ((hangingpoint.y + addHeight) <= maximumReduceHeight)
            {
                addHeight = 0;
            }
         

            float step = speed * Time.deltaTime;
            target = targets[t].transform;
            hangingpoint = GetComponent<droneControlV1>().drone.transform.position;
            //Debug.Log("hp" + hangingpoint);
            //Vector3 hangingPoint = Vector3 localpoint;

            

            Vector3 localtargetpoint = new Vector3(target.transform.position.x,
                hangingpoint.y + addHeight,
                target.transform.position.z);
            Vector3 localdronepoint = new Vector3(drone.transform.position.x,
                hangingpoint.y + addHeight,
                drone.transform.position.z);
            


            if (hover == false)
            {
                drone.transform.position = Vector3.MoveTowards(localdronepoint, localtargetpoint, step);
                
                //transform.position += transform.forward * Time.deltaTime * step;

                //drone.transform.position = Vector3.Lerp(localdronepoint,
                //localtargetpoint + drone.transform.forward,
                //Time.deltaTime * 10);

                //Vector3 targetDir = localtargetpoint - localdronepoint;
                //Vector3 newDir = Vector3.RotateTowards(drone.transform.forward, targetDir, step, 0.0f);
                //drone.transform.rotation = Quaternion.LookRotation(newDir);
            }

            if (Vector3.Distance(localdronepoint, localtargetpoint) < changeTargetDistance && t <= targets.Length - 1 )
            {
                t++;
                if (t == targets.Length)
                {
                    t = 0;
                }
                target = targets[t].transform;
                if (hover == false)
                {
                    drone.transform.position = Vector3.MoveTowards(localdronepoint, localtargetpoint, step);

                    //drone.transform.position = Vector3.Lerp(localdronepoint,
                    //localtargetpoint + drone.transform.forward,
                    //Time.deltaTime * 10); ;

                    //transform.position += transform.forward * Time.deltaTime * step;
                }
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                hover = !hover;      
            }

            if (Input.GetKeyDown(KeyCode.C))
            {               
                finishTour = true;
                gameObject.GetComponent<droneControlV3>().enabled = true;
                gameObject.GetComponent<droneControlV1>().getheight = false;
                
            }


        }



        //RotateDrone();
        
    }

    void RotateDrone()
    {
        float step = speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, prePoition) >0.5f )
        {
            rotateTo = transform.position - prePoition;
        }

        Vector3 newDir = Vector3.RotateTowards(transform.forward, -rotateTo, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        Debug.Log("newDir" + newDir);

    }
}