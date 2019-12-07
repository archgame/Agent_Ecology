using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotPickup : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] peoples;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    int randEnemy;
    public int t;
    int n;
    public int maxnumber;

    public GameObject[] targets;
    public GameObject[] drones;
    public GameObject[] peopleList;
    public GameObject PeopleStartpoint;
    
    public float changeTargetDistance;
    public float PeopleTargetDistance;
    public float[] distanceDronetoTargetList;
    public float[] distancePeopletoTargetList;
    public float startpointdistance;

    //public int dronesnumber;
    //public int peopleListnumber;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
        t = 0;
        /*GameObject[] waitTargets;
        waitTargets = GameObject.FindGameObjectsWithTag("Target");
        //Shuffle(waitTargets);
        List<GameObject> a = new List<GameObject>();
        for (int i = 0; i< targets.Length; i++)
        {
            a.Add(targets[i]);
        }
        for (int i = 0; i < waitTargets.Length; i++)
        {
            a.Add(waitTargets[i]);
        }
        targets = a.ToArray();
        */      
    }
    // Update is called once per frame
    void Update()
    {
        

        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        n = maxnumber - 2;
        if (t > n)
        {
            stop = true;
        }
        else
        {
            stop = false;
        }
        
      
        //drones = GameObject.FindGameObjectsWithTag("Robot");
        //peopleList = GameObject.FindGameObjectsWithTag("People");
        // check number
        //dronesnumber = drones.Length;
        //peopleListnumber = distancePeopletoTargetList.Length;


        for (int i = 0; i < drones.Length; i++)
        {
            distanceDronetoTargetList[i] = Vector3.Distance(drones[i].GetComponent<NavMeshAgent>().transform.position, targets[i].GetComponent<Transform>().position);      
        }
        for (int i = 0; i < peopleList.Length; i++)
        {       
            distancePeopletoTargetList[i] = Vector3.Distance(peopleList[i].GetComponent<NavMeshAgent>().transform.position, targets[i].GetComponent<Transform>().position);
        }

        for (int i = 0; i < 5; i++)
        {
            
            
            if (distanceDronetoTargetList[i] < changeTargetDistance)
            {
                //drones[i].GetComponent<NavMeshAgent>().isStopped = true;
                peopleList[i].GetComponent<NavMeshAgent>().SetDestination(targets[i].GetComponent<Transform>().position);   //poeple to target

            }
            else
            {
                drones[i].GetComponent<NavMeshAgent>().SetDestination(targets[i].GetComponent<Transform>().position);   //drone to target
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));

                peopleList[i].GetComponent<NavMeshAgent>().SetDestination(PeopleStartpoint.GetComponent<NavMeshAgent>().transform.position+ spawnPosition);   //people to startpoint
            }
            if (distancePeopletoTargetList[i] < PeopleTargetDistance)
            {
                //Debug.Log("1111");
                int rnd = Random.Range(5, 9);
                int rnd1 = Random.Range(10, 14);
                GameObject tempDrone = drones[rnd];
                drones[rnd] = drones[i];
                drones[i] = tempDrone;

                GameObject tempPeople = peopleList[rnd];
                peopleList[rnd] = peopleList[i];
                peopleList[i] = tempPeople;

                GameObject tempDrone1 = drones[rnd1];
                drones[rnd1] = drones[rnd];
                drones[rnd] = tempDrone1;

                GameObject tempPeople1 = peopleList[rnd1];
                peopleList[rnd1] = peopleList[rnd];
                peopleList[rnd] = tempPeople1;
                //drones[i].GetComponent<NavMeshAgent>().isStopped = false;

            }

        }


        for (int i = 5; i < 15; i++)
        {

            drones[i].GetComponent<NavMeshAgent>().SetDestination(targets[i].GetComponent<Transform>().position);   //drone to target
            peopleList[i].GetComponent<NavMeshAgent>().SetDestination(PeopleStartpoint.GetComponent<NavMeshAgent>().transform.position);
            /*if (distanceDronetoTargetList[i] < changeTargetDistance)
            {
                drones[i].GetComponent<NavMeshAgent>().isStopped = true;
            }*/
        }
        /*for (int i = 7; i < drones.Length; i++)
        {
            drones[i].GetComponent<NavMeshAgent>().SetDestination(target.transform.position);   //drone to target
        }*/









    }
    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {

            randEnemy = Random.Range(0, 2);
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
           

            randEnemy = Random.Range(0, 2);
            Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));
            Instantiate(peoples[randEnemy], spawnPosition1 + PeopleStartpoint.transform.TransformPoint(0, 0, 0), PeopleStartpoint.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
            t = t + 1;
            Debug.Log("t=" + t);
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