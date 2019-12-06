using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.UI;

public class RollerSkater : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;
    public string[] targetNames;
    

    public bool randomScale = false;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    //private float timePaintingMin = 10;
    //private float timePaintingMax = 25;

    public float DestinationTime = 0;
    public float paintingTime = 0;
    public float meetingWait = 0;
    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;

    public bool paralyzed = false;
    public float time;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "RollerSkates";

        //scale the gameobject randomly
        if (randomScale)
        {
            float x = Random.Range(xmin, xmax);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(zmin, zmax);
            transform.localScale = new Vector3(x, y, z);
        }
        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (paralyzed == true)
        {

            //Debug.DrawLine(transform.position, Vector3.up * 40, Color.red, 10f);
            agent.isStopped = true;
            time += Time.deltaTime;
            if (time > 10) //day in seconds
            {
                paralyzed = false;
                agent.isStopped = false;
                agent.enabled = true;
                agent.SetDestination(target.transform.position);
            }
        }

        else
        {

            Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

            if (waiting) // (waiting == false) (1 == 0)
            {
                if (waited > waitTime)
                {
                    waiting = false;
                    agent.isStopped = false;
                    waited = 0;
                }
                else
                {
                    waited += Time.deltaTime;
                }
            }
            else
            {
                float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
                if (changeTargetDistance > distanceToTarget)
                {
                    if (target.name.Contains("Intezaar"))
                    {
                        //Debug.Log("intezaar reached");
                        waitTime = meetingWait;
                    }
                    else
                    {
                        waitTime = 0;
                    }

                    if (target.name.Contains("Painting"))
                    {
                        waitTime = paintingTime;
                    }
                    /* else
                     {
                         waitTime = 0;
                     }*/

                    if (target.name.Contains("PaintNow"))
                    {
                        //Debug.Log("paintpaint");
                        waitTime = 12;
                    }
                    /*else
                    {
                        waitTime = 0;
                    }*/

                    /*if (target.name.Contains("Last"))
                    {
                        //gameObject.GetComponent<MeshRenderer>().enabled = false;
                        //gameObject.GetComponent<NavMeshAgent>().enabled = false;
                        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
                        //gameObject.GetComponent<Rigidbody>().useGravity = true;
                        //gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                        waitTime = 10000000;
                    }
                    else
                    {
                        waitTime = 0;
                    }*/

                    if (target.name.Contains("Last"))
                    {
                        //Debug.Log("paintpaint");
                        waitTime = DestinationTime;
                    }

                    /*if (target.name.Contains("Stop"))
                    {
                        Debug.Log("Tilting");
                        GetComponent<RotateOnTarget>().tilt = true;
                    }
                    else
                    {
                        Debug.Log("Tilting ended");
                        //GetComponent<RotateOnTarget>().tilt = false;
                    }*/

                    t++;
                    if (t == targets.Length)
                    {
                        t = 0;
                    }
                    //Debug.Log(this.name + " Change Target: " + t);
                    target = targets[t].transform;
                    agent.SetDestination(target.position); //each frame set the agent's destination to the target position
                    waiting = true;
                    agent.isStopped = true;
                }
            }
        }
    }

    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGO;
        for (int i = 0; i < objects.Length; i++)
        {
            //Debug.Log("i: " + i);
            int rnd = Random.Range(0, objects.Length);
            tempGO = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempGO;

        }
        return objects;
    }
}