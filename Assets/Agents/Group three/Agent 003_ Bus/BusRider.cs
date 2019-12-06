using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BusRider : MonoBehaviour
{
    NavMeshAgent agent;
    bool babaisStopped;

    public GameObject target;
    public GameObject[] targets;
    public bool board;

    string mode;

    // Update is called once per frame
    public string bus()
    {
        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh

        //找到available
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject go in allTargets)
        {
            if (go.name.Contains("Available"))
            {
                targets = go.GetComponent<AvailableVehicle>().BusStops;
            }
        }

        target = FindNearest(targets);

        //防止路上不小心经过bus stop
        //没有爸爸才可以board

        float distanceToTarget = Vector3.Distance(agent.transform.position, target.transform.position);

        if (3f > distanceToTarget && transform.parent==null) //have we reached our target
        {
            board = true;
            mode = "Bus";
        }
        else
        {
            agent.SetDestination(target.transform.position);
            board = false;
        }
        return mode;
    }

    GameObject FindNearest(GameObject[] targets)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject target in targets)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = target;
                distance = curDistance;
            }
        }
        return closest;
    }


}
