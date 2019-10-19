using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public string[] targetNames;

    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;
    public GameObject[] targets;

    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;

    public bool randomScale = false;
    public float xmin = 0.65f;
    public float xmax = 0.95f;
    public float ymin = 0.25f;
    public float ymax = 0.55f;
    public float zmin = 0.15f;
    public float zmax = 0.45f;

    private int obstacles = 0;


    // Start is called before the first frame update
    void Start()
    {
        // scale the gameobject
        if (randomScale)
        {
            float x = Random.Range(xmin, xmax);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(zmin, zmax);
            transform.localScale = new Vector3(x, y, z);
        }


        if (targets.Length == 0)
        {
            //get all game objects tagged with "target"
            targets = GameObject.FindGameObjectsWithTag("Target");


            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets)  // search all "target" game objects
            {
                foreach (string targetName in targetNames)
                {
                    Debug.Log("go: " + go.name);
                    //"target" containssplit 
                    if (go.name.Contains(targetName)) // if GameObject has the same name as targetName, add to list or contain this name
                    {
                        targetList.Add(go);
                    }
                }

            }
            targets = targetList.ToArray(); // Convert list to Array, because the code is still using array
        }


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
        if (!waiting) //(waiting == false)  // if (true) will run {}
        {
            //see agent's next destination
            Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

            float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);

            //change target once it is reached
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

                waiting = true;
                agent.isStopped = true;

            } // changeTargetDistance test
        } //if(!waiting)
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
                waited += Time.deltaTime;
            }
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles +1; || obstacles += 1;

        }

    }

    void OnTriggerExit(Collider collision)
    {

        Debug.Log("existed");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            obstacles--;//obstacles = obstcles-1 || obastacles -=1;
        }
        if (obstacles == 0) // once there are zero obstacles, start the agent moving
        {
            agent.isStopped = false;
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
