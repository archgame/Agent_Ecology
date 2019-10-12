using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IceCreamTruck : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;
    public bool _patrolWaiting;
    public float _totalWaitTime = 3f;
    bool _travelling = true;
    bool _waiting;
    float _waitTimer;

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
        if (changeTargetDistance > distanceToTarget)
        {
            //Check if we're close to the destination.
            if (_travelling && agent.remainingDistance <= 1.0f)
            {
                _travelling = false;

                if (_patrolWaiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }
            }
            if (_waiting)
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;
                    

                    t++;
                    if (t == targets.Length)
                    {
                        t = 0;
                    }
                    Debug.Log(this.name + " Change Target: " + t);
                    target = targets[t].transform;
                    agent.SetDestination(target.position); //each frame set the agent's destination to the target position
                    _travelling = true;
                }
            }
            
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
