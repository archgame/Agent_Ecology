using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrashTruck : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    [Header("Target info")]
    public string[] targetNames;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;

    /*
   [Header("Wait times")]
   //Wait time at target
   public float waitTimeShortMin = 0;
   public float waitTimeShortMax = 0;
   public float waitTimeLongMin = 0;
   public float waitTimeLongMax = 0;
   */

    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;

    //Min and Max Scale factor
    public bool randomScale = false;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    private int obstacles = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Random sclae on start
        if (randomScale == true)
        {
            float x = Random.Range(xmin, xmax);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(zmin, zmax);
            transform.localScale = new Vector3(x, y, z);
        }


        //grab targets using tags
        if (targets == null || targets.Length == 0)
        {
            //get all names objects tagget with "Target"
            targets = GameObject.FindGameObjectsWithTag("target");

            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets) //Seach all "Target" game objects
            {
                foreach (string targetName in targetNames)
                {
                    if (go.name.Contains(targetName))
                    {
                        targetList.Add(go);
                    }
                }
            }
            targets = targetList.ToArray(); //convert List to our targes List
        }
        if (shuffleTargets)
        {
            targets = Shuffle(targets);
        }

        //Debug.Log(this.name + hideFlags + " has " + targets.Length + "Target");

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
        //target = targets[0];
        //agent.SetDestination(target.transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (waiting) // saames as ;; if(waiting == false)
        {
            if (agent.enabled)
                if (waited > waitTime)
                {
                    waiting = false;
                    agent.isStopped = false;
                    waited = 0;
                    Debug.Log(name + " moving");
                }
                else
                {
                    waited += Time.deltaTime;
                }

        } //if waiting
        else
        {
            Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

            float distancetoTarget = Vector3.Distance(agent.transform.position, target.position);
            if (changeTargetDistance > distancetoTarget)
            {
                /*
                if(target.name.Contains("short"))
                {
                    waitTime = Random.Range(waitTimeShortMin, waitTimeShortMax);
                }
                if(target.name.Contains("long"))
                {
                    waitTime = Random.Range(waitTimeLongMin, waitTimeLongMax);
                }
                */

                t++;
                if (t == targets.Length)
                {
                    t = 0;
                }
                //Debug.Log(this.name + "change Target: " + t);
                target = targets[t].transform;
                agent.SetDestination(target.transform.position);

                waiting = true;
                agent.isStopped = true;
                Debug.Log(this.name + " waiting");

            } // cangue target.distance test
        }

    }

    
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision: " + collision.gameObject.name);
        //if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        if (collision.gameObject.name.Contains("RedLight"))
        {
            agent.isStopped = true;
            obstacles++; //obstacles + 1 || or
            Debug.Log("Light");
        }

        if (collision.gameObject.name.Contains("GreenLight"))
        {
            agent.isStopped = false;
            obstacles++; //obstacles + 1 || or
            Debug.Log("Light");
        }
    }

    /*
    void OnTriggerExit(Collider collision)
    {
        Debug.Log("LightOff");
        if (collision.gameObject.name.Contains("RedLight"))
        {
            obstacles--;
        }
        if (obstacles == 0)
        {
            agent.isStopped = false;
            Debug.Log("LightOff");
        }
    }

    /*
    void OnTriggerEnter(Collider alleycollision)
    {
        if (alleycollision.gameObject.name.Contains("alley"))
        {
            Debug.Log("Slowdown");
            agent.speed = 6;
        }

    }

    void OnTriggerExit(Collider alleycollision)
    {
        if (alleycollision.gameObject.name.Contains("alley"))
        {
            Debug.Log("fast");
            agent.speed = 11;
        }
    }
    */


    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject temGO;
        for (int i = 0; i < objects.Length; i++)
        {
            Debug.Log("i:" + i);
            int rnd = Random.Range(0, objects.Length);
            temGO = objects[rnd];
            objects[i] = temGO;
        }
        return objects;
    }


}
