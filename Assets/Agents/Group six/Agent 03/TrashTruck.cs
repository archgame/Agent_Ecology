using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrashTruck : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;

    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;

    //Min and Max Scale factor
    public bool randomScale = false;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    private int obstacles = 0;

    // Start is called before the first frame update
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
        if (targets == null || targets.Length == 0)   //(DELETE IF NOT CHANGUIN  TARGET)
        {
            targets = GameObject.FindGameObjectsWithTag("target");
        }
        
        if (shuffleTargets)
        {
            targets = Shuffle(targets);
        }
        
        Debug.Log(this.name + " has " + targets.Length + "Targets"); //this name means the name of the object

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
            t++;
            if (t == targets.Length) // Reseat target list onece Gameobject goes to all
            {
                t = 0;
            }
            Debug.Log(this.name + "change Target to: " + t);
            target = targets[t].transform;
            agent.SetDestination(target.position);
        }
    }
    
    /*
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            agent.isStopped = true;
            obstacles++; //obstacles + 1 || or
        }
            
    }
    void OnTriggerExit(Collider collision)
    {
        //Debug.Log("exited");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            obstacles--;
        }
        if (obstacles == 0)
        {
            agent.isStopped = false;
        }
    }

    */
    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject temGO;
        for (int i = 0; i < objects.Length; i++) 
        {
            Debug.Log("i:" + i);
            int rnd = Random.Range(0, objects.Length);
            temGO = objects[rnd];
            objects[i] = temGO;
        }
        return objects;
    }

  
}
