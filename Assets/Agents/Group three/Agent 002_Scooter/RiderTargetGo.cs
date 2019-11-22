using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RiderTargetGo : MonoBehaviour
{
    #region Global Variable
    GameObject target;
    NavMeshAgent agent;

    private Vector3 position;
    public GameObject[] AvailableScooters;
    public GameObject GetScooter;
    public GameObject baba;
    public GameObject parent;
    public GameObject[] Riders;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        baba = transform.parent.gameObject;
        //List all Scooters and find those without a child named Rider
        List<GameObject> ScooterList = new List<GameObject>();
        List<GameObject> shuffledList = new List<GameObject>();


        GameObject[] Scooters = GameObject.FindGameObjectsWithTag("Scooter");
        foreach (GameObject Scooter in Scooters)
        {
            if (Scooter.GetComponent<NavMeshAgent>().speed == 0)
            {
                ScooterList.Add(Scooter);
                //shuffledList = ScooterList.OrderBy(x => Random.value).ToList();
            }
        }
        AvailableScooters = ScooterList.ToArray();
        //AvailableScooters = Shuffle(AvailableScooters);

    }

    // Update is called once per frame
    void Update()
    {
        Riders = new GameObject[AvailableScooters.Length];
        for(int i=0; i < AvailableScooters.Length;i++)
        {
            if (Riders[i] == null)
            {
                Riders[i] = gameObject;
                GetScooter = AvailableScooters[i];
                AvailableScooters[i].GetComponent<NavMeshAgent>().speed = Random.Range(9, 12);
                /*NavMeshAgent agent = AvailableScooters[i].GetComponent<NavMeshAgent>();
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
                if (agent.hasPath)
                {
                    Vector3 toSteeringTarget = agent.steeringTarget - transform.position;
                    float turnAngle = Vector3.Angle(transform.forward, toSteeringTarget);
                    agent.acceleration = turnAngle * agent.speed * 0.01f;
                }*/

                //AvailableScooters[i].GetComponent<NavMeshAgent>().isStopped = true;
                agent.SetDestination(GetScooter.transform.position);
                float dist = Vector3.Distance(agent.transform.position, GetScooter.transform.position);
                if (0.5f > dist)
                {
                    GetScooter.GetComponent<NavMeshAgent>().isStopped = false;
                    transform.parent = GetScooter.transform;
                    GetComponent<NavMeshAgent>().enabled = false;
                    GetComponent<RiderTargetGo>().enabled = false;
                    AvailableScooters = Shuffle(AvailableScooters);
                    break;
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
