﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickThemUp3 : MonoBehaviour
{

    GameObject parent;
    NavMeshAgent agent;
    public List<GameObject> riders = new List<GameObject>();
    public List<int> passengerStops = new List<int>();
    private bool isStopped = false;
    public int peopleAtStop = 0;


    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        agent = gameObject.GetComponentInParent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        //first stop frame
        if (agent.isStopped == true && isStopped == false)
        {
            //adding each rider on the list
            foreach (GameObject rider in riders)
            {
                rider.transform.SetParent(parent.transform);
                rider.GetComponent<NavMeshAgent>().enabled = false;
                rider.GetComponent<MeshRenderer>().enabled = false;
                rider.GetComponent<Collider>().enabled = false;
            }

            isStopped = true;

            //deboarding
            List<int> removeIndexes = new List<int>();
            for (int i = 0; i < passengerStops.Count; i++)
            {
                passengerStops[i]++;
                //*
                if (passengerStops[i] == 72)
                {
                    GameObject passenger = riders[i];
                    Debug.Log("agent deboards: " + passenger.name);
                    NavMeshAgent a = passenger.GetComponent<NavMeshAgent>();
                    a.enabled = true;
                    a.isStopped = false;
                    passenger.GetComponent<MeshRenderer>().enabled = true;
                    passenger.GetComponent<Collider>().enabled = true;
                    passenger.transform.SetParent(null);
                    removeIndexes.Add(i);
                    //passenger.transform.position = parent.transform.position;
                    //Debug.DrawRay(parent.transform.position, Vector3.up*100, Color.magenta,2);
                }
                //*/
            }

            removeIndexes.Reverse();
            foreach (int i in removeIndexes)
            {
                riders.RemoveAt(i);
                passengerStops.RemoveAt(i);
            }
        }

        //reste isStopped variable for next stop
        if (!agent.isStopped)
        {
            isStopped = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            peopleAtStop++;
            //Debug.Log("collision: " + collision.name);
            GameObject passenger = collision.gameObject;
            //make sure the collision is not already a child of the bus
            //if (agent.isStopped)
            //{
            riders.Add(passenger);
            passengerStops.Add(0);
            if (passenger.GetComponent<Tram>().isRider == false)
            {
                passenger.GetComponent<Tram>().isRider = true;
                //Debug.Log("passenger added");
                //passenger.transform.SetParent(parent.transform);
                //passenger.GetComponent<NavMeshAgent>().enabled = false;
                //passenger.GetComponent<MeshRenderer>().enabled = false;
                //passenger.GetComponent<Collider>().enabled = false;

                riders.Add(passenger);
                passengerStops.Add(0);

            }
            //}

        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {

            GameObject passenger = collision.gameObject;
            Debug.Log("exit triggered: " + passenger.name);
            passenger.GetComponent<Tram>().isRider = false;
            passenger.GetComponent<NavMeshAgent>().isStopped = false;
            //passenger.GetComponent<NavMeshAgent>().Resume();
            //passenger.GetComponent<PranaySample>().position =
            //passenger.GetComponent<PranaySample>().targets[0].transform.position;
        }
    }



}