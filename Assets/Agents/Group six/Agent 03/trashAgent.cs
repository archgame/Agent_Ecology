using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class trashAgent : MonoBehaviour

{
    Transform target;
    NavMeshAgent agent;

    [Header("Target info")]
    public string[] targetNames;
    public GameObject[] targets;
    public float distanceZone = 3;
    int t;
    

// Start is called before the first frame update
void Start()
    {
        if (targets == null || targets.Length == 0)
        {
            //get all names objects tagget with "Target"
            targets = GameObject.FindGameObjectsWithTag("target");

            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets) //Seach all "Target" game objects
            {
                foreach (string targetName in targetNames)
                {
                    if (go.name.Contains(targetName))
                    {
                        targetList.Add(go);
                    }
                }
            }
            targets = targetList.ToArray(); //convert List to our targes List
        }

        agent = GetComponent<NavMeshAgent>(); //set the agent variable to this game object's navmesh
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
        //target = targets[0];
        //agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
}
