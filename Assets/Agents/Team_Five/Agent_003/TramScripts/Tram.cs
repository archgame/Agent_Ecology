using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tram : MonoBehaviour
{
    #region GLOBAL VARIABLES
    GameObject target;
    NavMeshAgent agent;
    public string[] targetNames;
    public bool isRider = false;


    public Vector3 position;

    [Header("Target Info")]
    public float changeTargetDistance = 3;
    private int t;
    public bool shuffleTargets = false;
    public GameObject[] targets;

    [Header("Wait Times")]
    public float waitTimeStop = 0;
    public float waitTimeTarget = 0;

    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Tram";

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t];
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        if (target.transform.position != position)
        {
            position = target.transform.position;
            agent.SetDestination(position);

        }

        //original text if (!waiting) // (waiting == false) (1 == 0)
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

        } //if waiting
        else
        {
            //see agent's next destination
            Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

            float distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
            //change target once it is reached
            if (changeTargetDistance > distanceToTarget) //have we reached our target
            {
                //type of stop
                if (target.name.Contains("t_target"))
                {
                    waitTime = waitTimeTarget;
                }

                if (target.name.Contains("Stop"))
                {
                    waitTime = waitTimeStop;

                }

                t++;
                if (t == targets.Length)
                {
                    t = 0;
                }
                Debug.Log(this.name + " Change Target: " + t);
                target = targets[t];
                agent.SetDestination(target.transform.position); //each frame set the agent's destination to the target position

                waiting = true;
                agent.isStopped = true;

            } // changeTargetDistance test
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("exited");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            obstacles--; //obstacles = obstacles - 1; || obstacles -= 1;
        }
        if (obstacles == 0) //once there are zero obstacles, start the agent moving
        {
            agent.isStopped = false;
        }
    }



    private int obstacles = 0;

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
