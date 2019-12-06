using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class ScooterRider : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject[] ScooterDestination;
    public GameObject[] UnpickedScooters;
    public GameObject closestScooter;
    string mode;



    // Update is called once per frame
    public string scooter()
    {
        agent = GetComponent<NavMeshAgent>();
        //找到available
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag("Target");
        foreach (GameObject go in allTargets)
        {
            if (go.name.Contains("Available"))
            {
                ScooterDestination = go.GetComponent<AvailableVehicle>().AvailableScooters;
            }
        }

        List<GameObject> ScooterList = new List<GameObject>();

        for (int i = 0; i < ScooterDestination.Length; i++)
        {
            if (!ScooterDestination[i].GetComponent<ScooterGO>().ScooterPicked)//如果可选scooter没有被选中
            {
                ScooterList.Add(ScooterDestination[i]);
            }
        }
        UnpickedScooters = ScooterList.ToArray();

        //找到最近的scooter
        closestScooter = FindNearest(UnpickedScooters);

        Debug.DrawLine(transform.position, closestScooter.transform.position, Color.red);
        float dist = Vector3.Distance(transform.position, closestScooter.transform.position);
        if (dist > 1f)
        {
            agent.SetDestination(closestScooter.transform.position);
        }
        else
        {
            transform.parent = closestScooter.transform;
            Transform cube = closestScooter.transform.Find("Cube");
            Vector3 StandingPoint = new Vector3(cube.position.x, transform.position.y + 0.2f, cube.position.z);
            transform.position = Vector3.MoveTowards(transform.position, StandingPoint, 1f);
            GetComponent<NavMeshAgent>().enabled = false;
            mode = "Scooter";
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
