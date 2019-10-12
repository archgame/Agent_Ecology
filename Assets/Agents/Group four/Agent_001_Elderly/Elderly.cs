using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elderly : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null || targets.Length == 0)
            targets = Shuffle(targets);
        // Debug.Log(this.name + "has " + targets.Length + " Target");

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);


    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

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
            agent.SetDestination(target.position);//each frame set the agent's desitination to the target
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
            objects[rnd] = targets[i];
            objects[i] = tempGO;
        }
        return objects;
    }
}
