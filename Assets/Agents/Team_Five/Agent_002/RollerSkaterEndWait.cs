using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RollerSkaterEndWait : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public float changeTargetDistance = 3;
    int t;
   //private bool shuffleTargets = false;
   //private string[] targetNames;

    public bool randomScale = false;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    public float meetingWait = 0;
    public float waitTime = 0;
    public bool waiting = false;
    private float waited = 0;

    public GameObject earPhones;


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
            if (waiting) // (waiting == false) (1 == 0)
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

            if (target.name.Contains("Intezaar"))
            {
                Debug.Log("intezaar reached");
                agent.GetComponent<RollerSkater>().enabled = true;
                agent.GetComponent<RollerSkaterEndWait>().enabled = false;
            }

            t++;
            if (t == targets.Length)
            {
                t = 0;
            }
            Debug.Log(this.name + " Change Target: " + t);
            target = targets[t].transform;
            agent.SetDestination(target.position);
            waiting = true;
            agent.isStopped = true;//each frame set the agent's destination to the target position
        }


        /*
        if (target.name.Contains("Intezaar"))
        {
            //waitTime = meetingWait;

            if (changeTargetDistance > distanceToTarget)
            {
                Debug.Log("intezaar reached");
                gameObject.GetComponent<RollerSkater>().enabled = true;
                gameObject.GetComponent<RollerSkaterEndWait>().enabled = false;
            }
           
        }
        */


        if (waiting) // (waiting == false) (1 == 0)
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

    /*
     * GameObject[] Shuffle(GameObject[] objects)
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
    */
}
