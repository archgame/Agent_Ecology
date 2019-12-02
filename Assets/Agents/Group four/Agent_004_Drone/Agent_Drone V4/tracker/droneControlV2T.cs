using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneControlV2T : MonoBehaviour
{

    public GameObject drone;
    public float speed = 1.0f;
    public GameObject[] targets;
    

    private int t;

    public int maximumAddHeight = 15;
    public int maximumReduceHeight = 5;
    private float addHeight = 0;
    //private float h = 0.01f;

    Transform target;
    public float changeTargetDistance = 1;
    public bool ShuffleTargets = true;

    [HideInInspector]
    public Vector3 hangingpoint;
    

    //private bool hover = false;

    public bool finishTour = false;

    //public Vector3 prePoition;
    //public Vector3 rotateTo;
    //public Vector3 newDir;

    

    void Start()
    {

        if (targets == null || targets.Length == 0)
        {
                   
            targets = GameObject.FindGameObjectsWithTag("Motorcycle");
                  
        }

        targets = Shuffle(targets);
    }

    void Update()
    {
        if (gameObject.GetComponent<droneControlV1T>().getheight == true)
        {
            gameObject.GetComponent<droneControlV1T>().keyHit = false;
            gameObject.GetComponent<droneControlV1T>().enabled = false;



            float h = Random.Range(-0.00001f, 0.00001f);
            addHeight += h;
            //print("H"+h);
            hangingpoint = GetComponent<droneControlV1T>().drone.transform.position;

            
            


            float step = speed * Time.deltaTime;
            target = targets[t].transform;


            if (drone.transform.position.y >= maximumAddHeight)
            {
                addHeight = -0.001f;
                //print("XXXXX" + drone.transform.position.y);
            }

            if (drone.transform.position.y <= maximumReduceHeight)
            {
                addHeight = 0.001f;
                //print("ZZZZZ");
            }

            Vector3 localtargetpoint = new Vector3(target.transform.position.x,
                hangingpoint.y + addHeight,
                target.transform.position.z);
            Vector3 localdronepoint = new Vector3(drone.transform.position.x,
                drone.transform.position.y,
                drone.transform.position.z);

            

            drone.transform.position = Vector3.MoveTowards(localdronepoint, localtargetpoint, step);


            

           

            

            


        }



   

    }
    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGo;
        for (int i = 0; i < objects.Length; i++)
        {
            int rnd = Random.Range(0, objects.Length);
            tempGo = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempGo;
        }
        return objects;
    }

}