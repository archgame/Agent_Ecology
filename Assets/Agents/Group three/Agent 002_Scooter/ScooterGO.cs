using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScooterGO : MonoBehaviour
{
    NavMeshAgent agent;
    int t;
    Transform target;
    GameObject Rider;

    public GameObject[] targets;
    public string[] targetNames;
    public float ReachTargetDistance;
    public GameObject Parent;
    public bool ScooterAvaliable = false;
    public bool ScooterPicked;
    public bool ScooterIsStopped;



    // Start is called before the first frame update
    void Start()
    {
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
                    if (go.name.Contains(targetName)) //if GameObject has the same name as targetName, add to list
                    {
                        targetList.Add(go); 
                    }
                }
            }
            targets = targetList.ToArray(); //Convert List to Array, because other code is still using array
        }
        targets = Shuffle(targets);

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
    }

    // Update is called once per frame
    void Update()
    {
        ScooterIsStopped = agent.isStopped;

        if (gameObject.transform.Find("Rider")!=null) //有叫做Rider的儿子
        {
            agent.isStopped = false;
            ScooterAvaliable = false;


            //自然转弯，不同lane不同速度
            if (agent.hasPath)
            {
                Vector3 toSteeringTarget = agent.steeringTarget - transform.position;
                float turnAngle = Vector3.Angle(transform.forward, toSteeringTarget);
                agent.acceleration = turnAngle * agent.speed * 10000f;
            }
            NavMeshHit navHit;
            agent.SamplePathPosition(-1, 0.0f, out navHit);
            //Debug.Log("mask: " + navHit.mask);
            int bikelaneArea = 1 << NavMesh.GetAreaFromName("Bikelane");
            //Debug.Log("Bikelane " + bikelaneArea);
            if (bikelaneArea == navHit.mask)
            {
                agent.speed = 10;
                agent.acceleration = Random.Range(12, 15);
                //Debug.Log("Change Speed");
            }
            else
            {
                agent.speed = 5;
                agent.acceleration = 5;
            }

            Rider = gameObject.transform.Find("Rider").gameObject;
            agent.SetDestination(target.position);
            //Debug.DrawLine(agent.transform.position,target.transform.position,Color.black);
            float distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);
            if (ReachTargetDistance > distanceToTarget) //have we reached our target
            {
                t++;
                if (t == targets.Length)
                {
                    t = 0;
                }//准备好scooter下次去的target
                Rider.GetComponent<NavMeshAgent>().enabled = true;
                Rider.transform.parent = null;
            }

        }
        else//没有Rider
        {
            agent.isStopped = true;
            ScooterAvaliable = true;
            //ScooterAvaliable = Switch(ScooterAvaliable,10);
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
