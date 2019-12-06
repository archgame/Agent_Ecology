using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CircleBus : MonoBehaviour
{
    #region GLOBAL VARIABLES

    GameObject target;
    NavMeshAgent agent;
    //public bool isRider = false;
    public bool isTheBus = false;

    [Header("Target Info")]
    public string[] targetNames;
    [HideInInspector]
    public Vector3 position;
    public float changeTargetDistance = 3;
    private int t;
    public GameObject[] targets;

    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;


    public float TurningMultiplier = 1;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

        if (isTheBus)
            gameObject.tag = "Bus";

        //grab targets using tags
        if (targets.Length == 0)
        {
            //get all game objects tagged with "Target"
            targets = GameObject.FindGameObjectsWithTag("Target");

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


        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t];
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (target.transform.position != position)
            {
                position = target.transform.position;
                agent.SetDestination(position);
            }

            float distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);


            if (agent.hasPath)
            {
                Vector3 toSteeringTarget = agent.steeringTarget - transform.position;
                float turnAngle = Vector3.Angle(transform.forward, toSteeringTarget);
                agent.acceleration = turnAngle * agent.speed * TurningMultiplier;
            }
            //original text if (!waiting) // (waiting == false) (1 == 0)
            if (waiting) // (waiting == false) (1 == 0)
            {
                if (waited > waitTime)
                {
                    waiting = false;
                    agent.isStopped = false;
                    waited = 0;
                    BusPickUp[] pickups = gameObject.GetComponentsInChildren<BusPickUp>();
                    if (pickups.Length > 0)
                    {
                        pickups[0].peopleAtStop = 0;
                    }
                }
                else
                {
                    waited += Time.deltaTime;
                }

            } //if waiting
            else
            {
                //change target once it is reached
                if (changeTargetDistance > distanceToTarget) //have we reached our target
                {
                    t++;
                    if(t==targets.Length)
                    {
                        t = 0;
                    }
                    target = targets[t];
                    agent.SetDestination(target.transform.position); //each frame set the agent's destination to the target position
                    waiting = true;
                    agent.isStopped = true;

                } // changeTargetDistance test
            }



        }
    }


    public int obstacles = 0;

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            agent.isStopped = true;
            obstacles++;
        }

        #region
        /*if (collision.gameObject.name.Contains("RedLight"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
        if (collision.gameObject.name.Contains("GreenLight"))
        {
            obstacles--; //count as obstacle removal
            if (obstacles < 0)
            {
                obstacles = 0;
            }
            if (obstacles == 0) //once there are zero obstacles, start the agent moving
            {
                agent.isStopped = false;
            }
        }*/
        #endregion
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            obstacles--; //obstacles = obstacles - 1; || obstacles -= 1;
        }
        if (obstacles == 0) //once there are zero obstacles, start the agent moving
        {
            agent.isStopped = false;
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


}
