using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarTest : MonoBehaviour
{
    #region GLOBAL VARIABLES
    GameObject target;
    NavMeshAgent agent;
    public bool isRider = false;


    [Header("Target Info")]
    public string[] targetNames;
    [HideInInspector]
    public Vector3 position;
    public float changeTargetDistance = 3;
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
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;
    #endregion

    [Header("Raycast")]
    public Transform raycastAnchor;
    public float raycastLength = 5;
    public int raySpacing = 2;
    public int raysNumber = 6;

    [Header("Vehicle")]
    public float minTopSpeed;
    public float maxTopSpeed;

    public bool hasToStop = false;
    public bool hasToGo = false;

    //VehiclePhysics carController;
    //NavMeshAgent agent;
    //int curWp = 0;
    [HideInInspector]
    public int curSeg = 0;
    float initialTopSpeed;


    // Start is called before the first frame update
    void Start()
    {
        ////////////////////
        //carController = this.GetComponent<VehiclePhysics>();

        initialTopSpeed = GetComponent<NavMeshAgent>().speed;
        //carController.Topspeed = initialTopSpeed;
        /////////////////////////
        gameObject.tag = "CarTest";

        //grab targets using tags
        if (targets.Length == 0)
        {
            //get all game objects tagged with "Target"
            targets = GameObject.FindGameObjectsWithTag("CarTarget");

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
                //see agent's next destination
                Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);
                Debug.DrawLine(transform.position, agent.pathEndPosition, Color.cyan);
                Debug.DrawRay(agent.pathEndPosition, Vector3.up * 40, Color.red);
                Debug.DrawRay(target.transform.position, Vector3.up * 40, Color.yellow);

                float distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
                //change target once it is reached
                if (changeTargetDistance > distanceToTarget) //have we reached our target
                {
                    PickUp[] pickups = gameObject.GetComponentsInChildren<PickUp>();
                    if(pickups.Length > 0)
                    {
                        int riderCount = pickups[0].peopleAtStop;
                        Debug.Log("riderCount: " + riderCount);
                        if (riderCount > 0)
                        {                          
                            waitTime = waitTimeShortMax * riderCount;
                        }else
                        {
                            waitTime = waitTimeShortMin;
                        }
                    }
                    else
                    {
                        waitTime = waitTimeShortMin;
                    }
                    Debug.Log("waitTime: " + waitTime);

                    t++;
                    if (t == targets.Length)
                    {
                        t = 0;
                    }
                    //Debug.Log(this.name + " Change Target: " + t);
                    target = targets[t];
                    agent.SetDestination(target.transform.position); //each frame set the agent's destination to the target position
                    waiting = true;
                    agent.isStopped = true;

                } // changeTargetDistance test

                Debug.Log(gameObject.name + " : " + agent.hasPath);
                if (!agent.hasPath) //cath agent error when agent doesn't resume
                {
                    position = target.transform.position;
                    agent.SetDestination(position);
                }
            }
        }
        ///////////////
        float topSpeed = GetCarSpeed();
        MoveVehicle(topSpeed);
        //////////////////
    }

    float GetCarSpeed()
    {
        //If car has to stop, set speed to 0
        //if (hasToStop)
        //    return 0;

        Vector3 anchor = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);
        if (raycastAnchor != null)
            anchor = raycastAnchor.position;

        //Check if we are going to collide with a car in front
        CarTest otherCarAI = null;
        float topSpeed = initialTopSpeed;
        float initRay = (raysNumber / 2f) * raySpacing;

        for (float a = -initRay; a <= initRay; a += raySpacing)
        {
            float hitDist;
            CastRay(anchor, a, this.transform.forward, raycastLength, out otherCarAI, out hitDist);

            //If rays collide with a car, adapt the top speed to be the same as the one of the front vehicle
            if (otherCarAI != null && otherCarAI.agent != null && agent.speed > otherCarAI.agent.speed)
            {
                //Check if the car is on the same lane or not. If not the same lane, then we do not adapt the vehicle speed to the one in front
                //(it just means that the rays are hitting a car on the opposite lane...which shouldn't influence the car's speed)
                if (IsOnSameLane(otherCarAI.transform))
                    return topSpeed;

                //If the hit distance is too close, "emergency slow down" the car so they don't collide
                //else if (hitDist < 2f)
                //    return topSpeed = 0f;

                //Otherwise adapt the car speed to the one in front
                topSpeed = otherCarAI.agent.speed;
                break;
            }
        }

        //If no collision detected then keep the car top speed
        return topSpeed;
    }


    void MoveVehicle(float _topSpeed)
    {
        /*
        //Wheel steering value
        float steering = Mathf.Clamp(this.transform.InverseTransformDirection(agent.desiredVelocity).x, -1f, 1f);

        //If car is turning then decrease it's maximum
        float topSpeed = _topSpeed;
        if (steering > 0.2f || steering < -0.2f && carController.Topspeed > 15) topSpeed = initialTopSpeed / 2f;
        carController.Topspeed = topSpeed;

        //Move the car
        carController.Move(steering, 1f, 0f);
        */
        agent.speed = _topSpeed;
    }


    void CastRay(Vector3 anchor, float angle, Vector3 dir, float length, out CarTest outCarAI, out float outHitDistance)
    {

        outCarAI = null;
        outHitDistance = -1f;

        //Draw raycast
        Debug.DrawRay(anchor, Quaternion.Euler(0, angle, 0) * dir * length, new Color(1, 0, 0, 0.5f));

        //Detect hit only on the autonomous vehicle layer
        int layer = LayerMask.NameToLayer("SmallVehicle");
        Debug.Log("layer" + layer);
        RaycastHit hit;
        if (Physics.Raycast(anchor, Quaternion.Euler(0, angle, 0) * dir, out hit, length, layer))
        {
            outCarAI = hit.collider.GetComponentInParent<CarTest>();
            outHitDistance = hit.distance;
        }
    }


    bool IsOnSameLane(Transform otherCar)
    {
        Vector3 diff = this.transform.forward - otherCar.transform.forward;
        if (Mathf.Abs(diff.x) < 0.3f && Mathf.Abs(diff.z) < 0.3f) return true;
        else return false;
    }
}
