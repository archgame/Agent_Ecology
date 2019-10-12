using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 10;
    int t;
    public bool shuffleTargets = true;

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
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

        float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
        //Debug.Log("Drone to target: " + distanceToTarget + " " + agent.transform.position.x + " " + agent.transform.position.y + " " + agent.transform.position.z);
        //Debug.Log("Drone to target: " + distanceToTarget + " " + target.transform.position.x + " " + target.transform.position.y + " " + target.transform.position.z);
        //Debug.Log("change T D: " + changeTargetDistance);
        if (changeTargetDistance > distanceToTarget)
        {
            //Debug.Log("I blieve i can fly");
            t++;
            if(t == targets.Length)
            {
                t = 0;
            }
            Debug.Log(this.name + " Change Target: " + t);
            target = targets[t].transform;
            //Debug.Log("Drone to target: " + distanceToTarget + " " + target.transform.position.x + " " + target.transform.position.y + " " + target.transform.position.z);
            agent.SetDestination(target.position); //each frame set the agent's destination to the target position
        }        
    }

    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGO;
        for(int i = 0; i < objects.Length; i++)
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
