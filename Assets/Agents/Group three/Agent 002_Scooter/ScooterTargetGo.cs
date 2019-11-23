using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ScooterTargetGo : MonoBehaviour
{
    #region Global Variable
    public bool NTM;


    GameObject target;
    NavMeshAgent agent;
    //[HideInInspector]

    [Header("")]

    public string[] targetNames;

    private Vector3 position;

    public float changeTargetDistance = 3;
    private int t;
    public bool shuffleTargets = true;
    public GameObject[] targets;
    public bool isStopped;
    public GameObject parent;


    Transform Rider;


    #endregion

// Start is called before the first frame update
    void Start()
    {
        //grab targets using tags
        if (targets.Length == 0)
        {
            //get all game objects tagged with "Target"
            targets = GameObject.FindGameObjectsWithTag("target");

            List<GameObject> targetList = new List<GameObject>();           
            foreach(GameObject go in targets) //search all "Target" game objects
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
        target = targets[t];
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        NTM = agent.GetComponent<NavMeshAgent>().isStopped;

        //gameObject.AddComponent<Rigidbody>();

        /*if (agent.speed != 0)
        {
            if (agent.hasPath)
            {
                Vector3 toSteeringTarget = agent.steeringTarget - transform.position;
                float turnAngle = Vector3.Angle(transform.forward, toSteeringTarget);
                agent.acceleration = turnAngle * agent.speed * 0.01f;
            }
            NavMeshHit navHit;
            agent.SamplePathPosition(-1, 0.0f, out navHit);
            //Debug.Log("mask: " + navHit.mask);
            int bikelaneArea = 1 << NavMesh.GetAreaFromName("Bikelane");
            //Debug.Log("Bikelane " + bikelaneArea);
            if (bikelaneArea == navHit.mask)
            {
                agent.speed = Random.Range(10, 15);
                //agent.acceleration = Random.Range(12, 15);
                //Debug.Log("Change Speed");
            }
            else
            {
                agent.speed = 5;
                agent.acceleration = 5;
            }
        }*/
        agent.speed = Random.Range(9, 12);
        isStopped = agent.isStopped;
        //see agent's next destination
        Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

        float distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
        //Debug.Log(distanceToTarget);
        //change target once it is reached
        if (changeTargetDistance > distanceToTarget) //have we reached our target
        {
            Rider = transform.Find("Rider");
            if (transform.childCount > 5) 
            {
                Rider.GetComponent<NavMeshAgent>().enabled = true;
                Rider.GetComponent<RiderTargetGo>().enabled = true;
                Rider.parent = parent.transform;
                //Rigidbody rb = GetComponent<Rigidbody>();
                //Destroy(rb);
            }

            t++;
            if (t == targets.Length)
            {
                t = 0;
            }
            target = targets[t];
            agent.SetDestination(target.transform.position); 

        } // changeTargetDistance test
        else
        {
            if (transform.childCount < 6)
            {
                agent.isStopped = true;
                agent.speed = 0;
                //Rigidbody rb = GetComponent<Rigidbody>();
                //Destroy(rb);
            }
            else
            {
                //agent.isStopped = false;
            }
        }
    }


    
    GameObject[] Shuffle(GameObject[] objects)
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

    public int obstacles = 0;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(obstacles);

        //Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }

        if (collision.gameObject.name.Contains("RedLight"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
        if (collision.gameObject.name.Contains("GreenLight"))
        {
            obstacles--; //count as obstacle removal
            if(obstacles<0)
            {
                obstacles = 0;
            }
            if (obstacles == 0) //once there are zero obstacles, start the agent moving
            {
                agent.isStopped = false;
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log(obstacles);

        //Debug.Log("exited");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            obstacles--; //obstacles = obstacles - 1; || obstacles -= 1;
        }
        if (obstacles == 0) //once there are zero obstacles, start the agent moving
        {
            agent.isStopped = false;
        }
    }
}
