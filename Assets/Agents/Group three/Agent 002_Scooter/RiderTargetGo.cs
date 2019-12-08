using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RiderTargetGo : MonoBehaviour
{
    #region Global Variable
    public bool NTM;
    GameObject target;
    NavMeshAgent agent;

    private Vector3 position;
    public GameObject[] Scooters;
    public GameObject[] AvailableScooters;
    public GameObject GetScooter;
    public GameObject baba;
    public GameObject parent;
    public GameObject[] Riders;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.AddComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        baba = transform.parent.gameObject;
        //List all Scooters and find those without a child named Rider
        List<GameObject> ScooterList = new List<GameObject>();
        List<GameObject> shuffledList = new List<GameObject>();


        Scooters = GameObject.FindGameObjectsWithTag("Scooter");
        foreach (GameObject Scooter in Scooters)
        {
            if (Scooter.GetComponent<ScooterTargetGo>().avaliable)
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
        NTM = agent.GetComponent<NavMeshAgent>().isStopped;
        Riders = new GameObject[AvailableScooters.Length];
        for(int i=0; i < AvailableScooters.Length;i++)
        {
            if (Riders[i] == null)
            {
                Riders[i] = gameObject;
                GetScooter = AvailableScooters[i];
                AvailableScooters[i].GetComponent<NavMeshAgent>().speed = Random.Range(9, 12);
                
                agent.SetDestination(GetScooter.transform.position);
                float dist = Vector3.Distance(agent.transform.position, GetScooter.transform.position);
                if (1f > dist)
                {
                    //Rigidbody rb = GetComponent<Rigidbody>();
                    //Destroy(rb);
                    GetScooter.GetComponent<NavMeshAgent>().isStopped = false;
                    GetScooter.GetComponent<ScooterTargetGo>().avaliable = false;
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
    public int obstacles = 0;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Contains("GreenLight"))
        {
            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
        if (collision.gameObject.name.Contains("RedLight"))
        {
            obstacles--; //count as obstacle removal

        }
    }

    void OnTriggerExit(Collider collision)
    {
       // Debug.Log(obstacles);
        if (obstacles < 0)
        {
            obstacles = 0;
        }
        if (obstacles == 0) //once there are zero obstacles, start the agent moving
        {
            if(agent == null) { return; }
            agent.isStopped = false;
        }
    }
}
