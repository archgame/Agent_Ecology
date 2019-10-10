using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tram : MonoBehaviour
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
        //grab targets using tags
        if (targets == null || targets.Length == 0)
            if (shuffleTargets)
            {
                targets = Shuffle(targets);
            }
        //Debug.Log(this.name + "has " + targets.Length + "Target");

        agent = GetComponent<NavMeshAgent>();
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeTargetDistance > Vector3.Distance(agent.transform.position, target.position))
        {
            t++;
            if (t == targets.Length)
            {
                t = 0;
            }
            Debug.Log(this.name + "Change Target: " + t);
            target = targets[t].transform;
            agent.SetDestination(target.position); // each frame set the agent's destination to the
        }

        if (Input.GetKeyDown("s"))
        { 
            Debug.Log("Tram Stop");
            agent.isStopped = true;
        }

        if (Input.GetKeyDown("a"))
        {
            Debug.Log("Tram Start");
            agent.isStopped = false;
        }
    }

    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGO;
        for (int i = 0; i < targets.Length; i++)
        {
            Debug.Log("i: " + i);
            int rnd = Random.Range(0, targets.Length);
            tempGO = targets[rnd];
            targets[rnd] = targets[i];
            targets[i] = tempGO;
        }
        return objects;


    }

   
}
