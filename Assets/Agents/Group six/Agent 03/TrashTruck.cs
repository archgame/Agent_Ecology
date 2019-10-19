using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrashTruck : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    //public string[] targetNames;

    public float changeTargetDistance = 3;
    private int t;
    public bool shuffleTargets = true;
    public GameObject[] targets;

    public float waitTime = 0;
    private bool waiting = false; // to know id they are waiting 
    private float waited = 0; // to know how have they been waiting

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
        if (targets == null || targets.Length == 0)
        {
            targets = GameObject.FindGameObjectsWithTag("target");
        }
        // shuffle targets
        if (shuffleTargets)
        {
            targets = Shuffle(targets);
        }
        //Debug.Log(this.name + " has " + targets.Length + "Targets");

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
        

        //target = targets[0];
        //agent.SetDestination(target.transform.position);
        /*
        //grab targets using tags
        if (targets.Length == 0)  //(targets == null || targets.Length == 0)
        {
            // get all game objects tagged with "targets"
            targets = GameObject.FindGameObjectsWithTag("target");

            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets) // search all "tagetName" game objects
            {
                Debug.Log("go: " + go.name);
                foreach (string targetName in targetNames) 
                {
                    Debug.Log("targetName: " + targetNames);
                    if (go.name.Contains("targetNames")) //if GameObject has the same name as targetNAme, add to list
                    {
                        targetList.Add(go);
                    }
                }
            }
            targets = targetList.ToArray();
        }
        // shuffle targets
        if (shuffleTargets)
        {
            targets = Shuffle(targets);
        }
        //Debug.Log(this.name + " has " + targets.Length + "Targets");

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
        */
    }


    // Update is called once per frame
    void Update()
    {
        if (waiting) // if (waiting == false)
       
        {
            // see targets next destination
            Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

            float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
            //changeed target one it is reached
            if (changeTargetDistance > distanceToTarget)
            {
                t++;
                if (t == targets.Length)
                {
                    t = 0;
                }
                Debug.Log(this.name + " Change Target: " + t);
                target = targets[t].transform;
                agent.SetDestination(target.position); //each frame set the agent's destination to the target position
                waiting = true;
                agent.isStopped = true;
            } // change target distace test
        } // if waiting equate false
        else
        {
            if (waited > waitTime)
            {
                waiting = false;
                agent.isStopped = false;
                waited = 0;
            }
            else
            {
                waited += Time.deltaTime; // operation that add how much time has it waited
            }
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("LargeVehicle"))
        {
            agent.isStopped = true;
            obstacles++; //obstacles + 1 || or
        }
            
    }
    void OnTriggerExit(Collider collision)
    {
        Debug.Log("exited");
        if (collision.gameObject.layer == LayerMask.NameToLayer("LargeVehicle"))
        {
            obstacles--;
        }
        if (obstacles == 0)
        {
            agent.isStopped = false;
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
