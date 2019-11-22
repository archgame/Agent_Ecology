using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class human : MonoBehaviour
{

    //public bool worked;
    public Transform destination;
    NavMeshAgent agent;
    public bool findFood = false;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().enabled = true;
      //  GetComponent<human>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       // worked =GetComponent<Bicycle>().worked;
       // Debug.Log("dsb");
      //  agent.SetDestination(destination.transform.position);


        // GetComponent<Bicycle>().enabled = false;


        float distanceToTarget = Vector3.Distance(agent.transform.position, destination.transform.position);
            if (2 > distanceToTarget )
            {
            //   agent.isStopped = true;
            findFood = true;
        }
            else
            {
                agent.SetDestination(destination.transform.position);
            }
        
    }
}
