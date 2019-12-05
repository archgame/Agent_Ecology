using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Robot : MonoBehaviour
{
    #region GLOBAL VARIABLES
    public GameObject target;
    NavMeshAgent agent;
    public bool isRider = false;


    [Header("Target Info")]
    public string[] targetNames;
    [HideInInspector]
    public Vector3 position;
    public float changeTargetDistance = 1;
    private int t;
    public bool shuffleTargets = true;
    public GameObject[] targets;

    [Header("Wait Times")]
    public float waitTimeShortMin = 0;
    public float waitTimeShortMax = 0;
    public float waitTimeLongMin = 0;
    public float waitTimeLongMax = 0;

    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;

    [Header("Agent Size")]
    public bool randomScale = false;
    public float xmin = 0.9f;
    public float xmax = 1.1f;
    public float ymin = 0.9f;
    public float ymax = 1.1f;
    public float zmin = 0.9f;
    public float zmax = 1.1f;
    #endregion
    public string m_Scene;
    public GameObject goods;
    private bool ifGoods;

    public GameObject[] shopTargetList;
    public GameObject[] officeTargetList;
    public GameObject[] chargingTargetList;
    public bool ifPickup;
    public int targetNumber;

    public bool ifCreate;

    public GameObject Red;
    public GameObject Green;

    // Start is called before the first frame update
    void Start()
    {
        

        //scale the gameobject randomly
        if (randomScale)
        {
            float x = Random.Range(xmin, xmax);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(zmin, zmax);
            transform.localScale = new Vector3(x, y, z);
        }

        //grab targets using tags
        if (targets.Length == 0)
        {
            //get all game objects tagged with "Target"
            List<GameObject> shopList = new List<GameObject>();
            List<GameObject> officeList = new List<GameObject>();
            List<GameObject> chargingList = new List<GameObject>();

            

            GameObject[] foodtTrucks = GameObject.FindGameObjectsWithTag("IceCreamTruck");
            foreach (GameObject truck in foodtTrucks) //search all "Target" game objects
            {
                if (truck.name.Contains("Food Truck Cube")) //if GameObject has the same name as targetName, add to list
                {
                    shopList.Add(truck);
                }

            }


            targets = GameObject.FindGameObjectsWithTag("target");
            foreach (GameObject go in targets) //search all "Target" game objects
            {

                if (go.name.Contains("Shop"))
                {
                    shopList.Add(go);
                }
                if (go.name.Contains("Office"))
                {
                    officeList.Add(go);
                }
                if (go.name.Contains("Charging"))
                {
                    chargingList.Add(go);
                }



            }
            shopTargetList = shopList.ToArray();
            officeTargetList = officeList.ToArray();
            chargingTargetList = chargingList.ToArray();
            targets = shopList.ToArray(); //Convert List to Array, because other code is still using array
        }

        //shuffle targets
        if (shuffleTargets)
        {
            targets = Shuffle(targets);
        }
        //Debug.Log(this.name + " has " + targets.Length + "Targets");

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
       
        ifPickup = false;
        ifCreate = false;
        targetNumber = 0;
        target = shopTargetList[Random.Range(0, shopTargetList.Length)];
        agent.SetDestination(target.transform.position);



    }

    // Update is called once per frame
    void Update()
    {
        if (target.name.Contains("Office"))
        {

            if (ifCreate)
            {
            }
            else
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
                cube.transform.parent = transform;
                cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                ifCreate = true;
            }

            float distanceToOffice = Vector3.Distance(agent.transform.position, target.transform.position);
            //change target once it is reached
            if (changeTargetDistance > distanceToOffice) //have we reached our target
            {
                if (ifCreate)
                {
                    transform.DetachChildren();
                    ifCreate = false;
                }
            }
        }
        if (target.name.Contains("Charging"))
        {
            Red.GetComponent<MeshRenderer>().enabled = true;
            Green.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            Red.GetComponent<MeshRenderer>().enabled = false;
            Green.GetComponent<MeshRenderer>().enabled = true;
        }
    
        /*if (target.name.Contains("Food Truck Cube"))
        {
            float distanceToOffice = Vector3.Distance(agent.transform.position, target.transform.position);
            //change target once it is reached
            if (changeTargetDistance > distanceToOffice) //have we reached our target
            {
                if (ifCreate)
                {
                    transform.DetachChildren();
                    ifCreate = false;
                }
            }
          
                
        }*/
        /*if (target.name.Contains("Charging"))
        {

            float distanceToOffice = Vector3.Distance(agent.transform.position, target.transform.position);
            //change target once it is reached
            if (changeTargetDistance > distanceToOffice) //have we reached our target
            {
                if (ifCreate)
                {
                    transform.DetachChildren();
                    ifCreate = false;
                }
            }
        }*/
        /*if (target.name.Contains("Shop"))
        {
            float distanceToOffice = Vector3.Distance(agent.transform.position, target.transform.position);
            //change target once it is reached
            if (changeTargetDistance > distanceToOffice) //have we reached our target
            {
                if (ifCreate)
                {
                    transform.DetachChildren();
                    ifCreate = false;
                }

               
            }
        }*/
      



        if (target.name.Contains("Food Truck Cube")) //if GameObject has the same name as Food Truck Cube, change TargetDistance
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.yellow);
            changeTargetDistance = 5f;

        }
        else
        {
            changeTargetDistance = 1f;
        }
        



        if (agent.enabled)
        {
            if (target.transform.position != position)
            {
                position = target.transform.position;
                agent.SetDestination(position);
            }

            //original text if (!waiting) // (waiting == false) (1 == 0)
            if (waiting) // (waiting == false) (1 == 0)
            {
                if (waited > waitTime)
                {
                    waiting = false;
                    agent.isStopped = false;
                    waited = 0;
                    PickUp[] pickups = gameObject.GetComponentsInChildren<PickUp>();
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
               

                float distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
                //change target once it is reached
                if (changeTargetDistance > distanceToTarget) //have we reached our target
                {
                    //type of stop
                    if (target.name.Contains("Charging"))
                    {
                        //Debug.Log("long wait");
                        waitTime = Random.Range(waitTimeLongMin, waitTimeLongMax);
                    }
                    if (target.name.Contains("Shop"))
                    {
                        //Debug.Log("long wait");
                        waitTime = Random.Range(waitTimeShortMin, waitTimeShortMax);
                    }
                    if (target.name.Contains("Office"))
                    {
                        //Debug.Log("short");
                        waitTime = Random.Range(waitTimeShortMin, waitTimeShortMax);
                    }


                    if (targetNumber < 2)
                    {
                        if (ifPickup)
                        {
                            targetNumber++;
                            target = shopTargetList[Random.Range(0, shopTargetList.Length)];
                            ifPickup = false;
                        }

                        else
                        {
                            target = officeTargetList[Random.Range(0, officeTargetList.Length)];
                            ifPickup = true;
                        }

                      
                        
                    }
                    else
                    {
                        targetNumber = 0;
                        target=chargingTargetList[Random.Range(0, chargingTargetList.Length)];
                    }

                    

               


                
                    //Debug.Log(this.name + " Change Target: " + t);
                  
                    agent.SetDestination(target.transform.position); //each frame set the agent's destination to the target position
                    waiting = true;
                    agent.isStopped = true;

                } // changeTargetDistance test

                //Debug.Log(gameObject.name + ":" + agent.hasPath);
                if (!agent.hasPath)// catch agent error when agent doesn't resume
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
   

        if (collision.gameObject.name.Contains("NoWalk"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }

        if (collision.gameObject.name.Contains("Green"))
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
        }



    }

    void OnTriggerExit(Collider collision)
    {
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

    public int obstacles = 0;

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