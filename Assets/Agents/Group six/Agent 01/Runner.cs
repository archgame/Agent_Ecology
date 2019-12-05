using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Runner : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    NavMeshAgent agent;

    [Header("Target info")]
    public string[] targetNames;
    public GameObject[] targets;

    private Vector3 position;
    public float changeTargetDistance = 3;
    int t;
    public bool shuffletargets = true;

    [Header("Wait Times")]
    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;

    //Min and Max Scale factor
    public bool randomScale = false;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    private int obstacles = 0;

    [Header("Day Night")]

    DayNightCycle timeScript; //from scrip daynight
    bool nightTime = true;

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

        GetTargets(new string[] { targetNames[0] });

        timeScript = Camera.main.GetComponent<DayNightCycle>(); //get script DayNight
        float now = timeScript.time;
        if (nightTime && now > 21600 && now < 72000) //daytime
        {
            nightTime = false;
        }
        else //nigh time
        {
            nightTime = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //time control from scrip
        float now = timeScript.time;
        if (nightTime && now > 21600 && now < 72000) //daytime
        {
            nightTime = false;
            targets = new GameObject[0];
            GetTargets(new string[] { targetNames[1] });
        }
        if (!nightTime)
        {
            if (now < 21600 || now > 72000) //nigh time
            {
                nightTime = true;
                targets = new GameObject[0];
                GetTargets(new string[] { targetNames[0] });
            }
        }


        if (agent.enabled)
        {
            if (target.transform.position != position)
            {
                position = target.transform.position;
                agent.SetDestination(position);
            }

            if (waiting) // saames as ;; if(waiting == false)
            {
                if (agent.enabled)
                    if (waited > waitTime)
                    {
                        waiting = false;
                        agent.isStopped = false;
                        waited = 0;
                        // Debug.Log(name + " moving");
                    }
                    else
                    {
                        waited += Time.deltaTime;
                    }

            } //if waiting
            else
            {
                Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);

                float distancetoTarget = Vector3.Distance(agent.transform.position, target.position);
                if (changeTargetDistance > distancetoTarget)
                {
                    t++;
                    if (t == targets.Length)
                    {
                        t = 0;
                    }
                    //Debug.Log(this.name + "change Target: " + t);
                    target = targets[t].transform;
                    agent.SetDestination(target.transform.position);

                    waiting = true;
                    agent.isStopped = true;
                    //Debug.Log(this.name + " waiting");

                } // cangue target.distance test

                //Debug.Log(gameObject.name + " : " + agent.hasPath);
                if (!agent.hasPath) //cath agent error when agent doesn't resume
                {
                    position = target.transform.position;
                    agent.SetDestination(position);

                }
            }
        }


    }

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

    GameObject[] Shuffle(GameObject[] objects) 
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

    private void GetTargets(string[] targetByNames)
    {

        //grab targets using tags
        if (targets == null || targets.Length == 0)
        {
            //get all names objects tagget with "Target"
            targets = GameObject.FindGameObjectsWithTag("Target");

            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets) //Seach all "Target" game objects
            {
                foreach (string targetName in targetByNames)
                {
                    if (go.name.Contains(targetName))
                    {
                        targetList.Add(go);
                    }
                }
            }
            targets = targetList.ToArray(); //convert List to our targes List
        }
        if (shuffletargets)
        {
            targets = Shuffle(targets);
        }

        //Debug.Log(this.name + hideFlags + " has " + targets.Length + "Target");

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
    }

}
