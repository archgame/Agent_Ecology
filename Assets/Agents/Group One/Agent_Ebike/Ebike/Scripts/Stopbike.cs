using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stopbike : MonoBehaviour
{
    #region GLOBAL VARIABLES
    GameObject target;
    public GameObject[] targets;
    NavMeshAgent agent;
    public GameObject[] spots;
    public GameObject[] bikes;
    string SpotName = "ParkingSpot";
    [Header("Target Info")]
    public string[] targetNames;
    private int t = 0;
    //public float changeTargeDistance = 3;
    public bool shuffleTargets = true;
    public bool parking = false;
    public bool detachchild = false;
    public Transform myChildObject;
    GameObject LastTarget;
    public float TurningMultiplier = 1;
    public bool aligned = false;
    public Vector3 position;
    public Transform leaveaway;
    public Transform brainobject;
    GameObject lastTarget;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
      if (targets.Length == 0)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets)
            {
                foreach(string targetName in targetNames)
                {
                    if(go.name.Contains(targetName))
                    {
                        targetList.Add(go);
                    }
                }
            }
            targets = targetList.ToArray();
        }
        if(shuffleTargets)
        {
            targets = Shuffle(targets);
        }
        t = 0;
        target = targets[t];
       agent.SetDestination(target.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            if (target.name.Contains("BikeStop"))
            {
                GameObject[] spots = target.GetComponent<BikeStop>().spots;
                GameObject[] bikes = target.GetComponent<BikeStop>().bikes;
                List<GameObject> pickbikes = new List<GameObject>();
                for (int i = 0; i < bikes.Length; i++)
                {
                    if (bikes[i] == null)
                    {
                        bikes[i] = gameObject;
                        target = spots[i];
                        position = target.transform.position;
                        agent.SetDestination(position);
                      //  bikes[i].tag = "stop bike";

                       // pickbikes.Add(bikes[i]);
                      //  Debug.Log(pickbikes.Count);
                        break;

                    }
                   
                   
                }
            }


            else
            {
                float distanceToTarget = Vector3.Distance(agent.transform.position, position);
                if (1 > distanceToTarget)
                {
                    parking = true;
                    lastTarget = target;
                   
                   
                    agent.SetDestination(target.transform.position);
                    //Debug.Log("儿子出去了");
                    GetComponentInChildren<NavMeshAgent>().enabled = true;
                    GetComponentInChildren<human>().enabled = true;
                    /* if (detachchild == true)
                     {
                         myChildObject.parent = null;
                     }*/

                    myChildObject.SetParent(brainobject);

                    agent.isStopped = true;
                    agent.tag = "stop bike";
                    agent.GetComponent<Stopbike>().enabled = false;
                }
            }
          /*  if (agent.isStopped && agent.CompareTag("Riders"))
            {

                agent.isStopped = false;
                agent.SetDestination(leaveaway.position);
            }*/


            if (agent.hasPath)
            {
                Debug.DrawLine(transform.position, agent.steeringTarget, Color.black);
                Vector3 toSteeringTarget = agent.steeringTarget - transform.position;
                float turnAngle = Vector3.Angle(transform.forward, toSteeringTarget);
                agent.acceleration = turnAngle * agent.speed * TurningMultiplier;

            }  
        }

        if (parking)
        {
            Vector3 parkingPosition = lastTarget.transform.position + lastTarget.transform.right * 0.4f;
            Vector3 levelPosition = new Vector3(parkingPosition.x, transform.position.y, parkingPosition.z);
            float dist = Vector3.Distance(levelPosition, transform.position);
            if (transform.position != levelPosition)
            {
                //  Debug.Log("parking...");
                transform.position = Vector3.Lerp(
                    transform.position,
                    levelPosition,
                    0.04f); //smoothly moving bicycle to parking spot
                transform.forward = Vector3.Lerp(
                    transform.right,
                    -lastTarget.transform.right,
                    0.04f); //smoothly moving bicycle to parking spot
                if (dist < 0.1f)
                {
                    parking = false;
                    aligned = true;
                }
            }
        }
        if (aligned)
        {
            Vector3 parkingPosition = LastTarget.transform.position;
            Vector3 levelPosition = new Vector3(parkingPosition.x, transform.position.y, parkingPosition.z);

            float dist = Vector3.Distance(levelPosition, transform.position);
            if (transform.position != levelPosition)
            {
                transform.position = Vector3.Lerp(transform.position, levelPosition, 0.02f);
                transform.forward = Vector3.Lerp(transform.forward, -LastTarget.transform.right, 0.04f);
                if (dist < 0.1f)
                {
                    aligned = false;

                }
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
}
