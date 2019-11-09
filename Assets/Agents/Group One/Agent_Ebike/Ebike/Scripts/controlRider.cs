using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class controlRider : MonoBehaviour
{
    human humanScript;
    QueueL1 queue;
    another rideAway;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        humanScript = GetComponent<human>();
        queue = GetComponent<QueueL1>();
        rideAway = GetComponent<another>();

        humanScript.enabled = false;
        queue.enabled = false;
        rideAway.enabled = false;
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(humanScript.findFood == true)
        {
            humanScript.enabled = false;
            queue.enabled = true;
        }

        if(queue.finished == true)
        {
            gameObject.tag = "Riders";
            queue.enabled = false;
            rideAway.enabled = true;
        }
    }

    /*
    void Leave()
    {
        agent.SetDestination(leaveToTarget.transform.position);
        Debug.Log("1111");
        if(Vector3.Distance(transform.position, leaveToTarget.transform.position) < 3)
        {
            agent.isStopped = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }

    }
    */
}
