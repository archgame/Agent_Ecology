using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleAgentPracticeW3 : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public int changeTargetDistance = 1;
    private int t;
    public bool ShuffleTargets = true;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;
    public float waitTime = 0;
    private 


    // Start is called before the first frame update
    void Start()
    {
        //float x = Random.Range(xmin, xmax);
        //float y = Random.Range(ymin, ymax);
        //float z = Random.Range(zmin, zmax);
        //transform.localScale = new Vector3(x, y, z);
      
        
        //grab targets using tags
        if (targets == null || targets.Length == 0)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
        }
        targets = Shuffle(targets);
        // does Shuffle a type? Variable?
        //Debug.Log(this.name + "has" + targets.Length + "Targets");
        agent = GetComponent<NavMeshAgent>();
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
        //agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);
        
        float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
        //Debug.Log("ditanceToTarget" + distanceToTarget);
        if (changeTargetDistance > distanceToTarget)

        {
            t++;
            if (t == targets.Length)
            {
                t = 0;
                //Debug.Log("finishfullrun");
            }

        }
        //Debug.Log(this.name + "change target" + " "+ t);
        target = targets[t].transform;
        agent.SetDestination(target.position);
    }

    GameObject[] Shuffle(GameObject[] objects)
    // GameObject[] Shuffle(GameObject[] objects) WHYYYYYY????
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