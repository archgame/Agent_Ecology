using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skateboard : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;

    //float time = 4f;
    //float countTime = 0;

    //Rigidbody rb;
    //NavMeshAgent na;

    // Start is called before the first frame update
    void Start()
    {
        //grab targets using tags
        if (targets == null || targets.Length == 0)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
        }
        if (shuffleTargets)
        {
            targets = Shuffle(targets);
        }
        //Debug.Log(this.name + " has " + targets.Length + "Targets");

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);

        //get rigidbody
        //rb = GetComponent<Rigidbody>();
        //na = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump
        /*
        if(countTime > time)
        {
            na.enabled = false;
            rb.isKinematic = false;
            rb.useGravity = true;

            rb.AddForce(new Vector3(0, 0, 0.2f), ForceMode.Impulse);

            //rb.AddRelativeForce(new Vector3(0, 5, 5), ForceMode.Impulse);
            
        }
        else
        {
            countTime += Time.deltaTime;
        }
        */
        //Debug.Log("countTime" + countTime);
        //Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

        float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
        if (changeTargetDistance > distanceToTarget)
        {
            t++;
            if (t == targets.Length)
            {
                t = 0;
            }
           // Debug.Log(this.name + " Change Target: " + t);
            target = targets[t].transform;
            agent.SetDestination(target.position); //each frame set the agent's destination to the target position
        }
    }

    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGO;
        for (int i = 0; i < objects.Length; i++)
        {
            Debug.Log("i: " + i);
            int rnd = Random.Range(0, objects.Length);
            tempGO = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempGO;

        }
        return objects;
    }
}