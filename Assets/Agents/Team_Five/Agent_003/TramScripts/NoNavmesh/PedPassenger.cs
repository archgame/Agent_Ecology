using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedPassenger : MonoBehaviour
{
    #region GLOBAL VARIABLES
    GameObject target;
    NavMeshAgent agent;



    public Vector3 position;

    [Header("Target Info")]
    public float changeTargetDistance = 2;
    private int t;
    public GameObject[] targets;

    [Header("Wait Times")]
    public float waitTimeStop = 0;


    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;


    #endregion

    // Start is called before the first frame update
    void Start()
    {

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
                if (target.name.Contains("Station"))
                {
                    waitTime = waitTimeStop;
                    gameObject.GetComponent<PedPassenger>().enabled = false;
                    gameObject.GetComponent<PUpDoff>().enabled = true;

                }

                if (target.name.Contains("Target"))
                {
                    waitTime = 0;
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


}