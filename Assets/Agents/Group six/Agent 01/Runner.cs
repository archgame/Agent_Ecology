using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Runner : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffletargets = true;
   
    //Star is called before the first frame update
    void Start()
    {
        //grab targets using tags
        if (targets == null || targets.Length == 0)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
        }
            if (shuffletargets)
        {
            targets = shuffle(targets);
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
        Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

        float distancetoTarget = Vector3.Distance(agent.transform.position, target.position);
        if (changeTargetDistance > distancetoTarget)
        {
            t++;
            if(t == targets.Length)
            {
                t = 0;
            }
            Debug.Log(this.name + "change Target: " + t);
            target = targets[t].transform;
            agent.SetDestination(target.transform.position);
        }

       // agent.SetDestination(target.transform.position);
    }
    // AnimatorUpdateMode one per frame
    
    GameObject[] shuffle(GameObject[] objects) 
    {
        GameObject tempGO;
        for (int i=0; i <objects.Length; i++)
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
