using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RollerSkater2 : MonoBehaviour
{

    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffleTargets = true;
    public string[] targetNames;

    public bool randomScale = false;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "RollerSkates";

        //scale the gameobject randomly
        if (randomScale)
        {
            float x = Random.Range(xmin, xmax);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(zmin, zmax);
            transform.localScale = new Vector3(x, y, z);
        }

        /*
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
        */

        //grab targets using names
        if (targets.Length == 0)
        {
            //get all game objects tagged with "Target"
            targets = GameObject.FindGameObjectsWithTag("target");

            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets) //search all "Target" game objects
            {
                //Debug.Log("go: " + go.name);
                foreach (string targetName in targetNames)
                {
                    //Debug.Log("targetName: " + targetName);
                    // "Target" contains: "Tar", "Targ", "get", ! "Trgt"
                    if (go.name.Contains(targetName)) //if GameObject has the same name as targetName, add to list
                    {
                        targetList.Add(go);
                    }
                }
            }
            targets = targetList.ToArray(); //Convert List to Array, because other code is still using array
        }

        //shuffle targets
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
            t++;
            if (t == targets.Length)
            {
                t = 0;
            }
            Debug.Log(this.name + " Change Target: " + t);
            target = targets[t].transform;
            agent.SetDestination(target.position); //each frame set the agent's destination to the target position
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
            objects[rnd] = objects[i];
            objects[i] = tempGO;

        }
        return objects;
    }
}
