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

    //Min and Max Scale factor
    public bool randomScale = false;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    private int obstacles = 0;

    //Star is called before the first frame update
    void Start()
    {
        // Random sclae on start
        if (randomScale == true)
        {
            float x = Random.Range(xmin, xmax);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(zmin, zmax);
            transform.localScale = new Vector3(x, y, z);
        }

        //grab targets using tags
        if (targets == null || targets.Length == 0)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
        }
        //shuffle targets
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
            //Debug.Log(this.name + "change Target: " + t);
            target = targets[t].transform;
            agent.SetDestination(target.transform.position);
        }

       // agent.SetDestination(target.transform.position);
    }
    // AnimatorUpdateMode one per frame

    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("LargeVehicle"))
        {
            agent.isStopped = true;
            obstacles++; //obstacles + 1 || or
        }

    }
    void OnTriggerExit(Collider collision)
    {
        //Debug.Log("exited");
        if (collision.gameObject.layer == LayerMask.NameToLayer("LargeVehicle"))
        {
            obstacles--;
        }
        if (obstacles == 0)
        {
            agent.isStopped = false;
        }
    }

    GameObject[] shuffle(GameObject[] objects) 
    {
        GameObject tempGO;
        for (int i=0; i <objects.Length; i++)
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
