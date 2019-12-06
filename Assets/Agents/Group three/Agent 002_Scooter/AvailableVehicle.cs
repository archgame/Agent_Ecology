using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AvailableVehicle : MonoBehaviour
{
    NavMeshAgent agent;

    GameObject[] Scooters;

    public GameObject[] BusStops;
    public GameObject[] AvailableScooters;


    // Update is called once per frame
    void Update()
    {
        agent = GetComponent<NavMeshAgent>();

        //List all Scooters and find those without a child named Rider

        //找到所有scooter
        List<GameObject> ScooterList = new List<GameObject>();
        Scooters = GameObject.FindGameObjectsWithTag("Scooter");
        //找到所有avaliable scooter
        foreach (GameObject Scooter in Scooters)
        {
            if (Scooter.GetComponent<ScooterGO>().ScooterAvaliable)
            {
                ScooterList.Add(Scooter);
            }
        }
        AvailableScooters = ScooterList.ToArray();


        //找到所有bus stop
        List<GameObject> BusStopList = new List<GameObject>();

        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject go in allTargets)
        {
            if (go.name.Contains("bus"))
            {
                BusStopList.Add(go);
            }
        }
        BusStops = BusStopList.ToArray();


    }


}
