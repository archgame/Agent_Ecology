using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrafficCollider : MonoBehaviour
{
    GameObject parent;
    NavMeshAgent agent;
    public float waitTime = 2;
    private bool waiting = true;
    public float waited = 0;

    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        agent = gameObject.GetComponentInParent<NavMeshAgent>();

        waited += Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.name.Contains("RedLight"))
        {
           agent.isStopped = true;          

        }

        if (collision.name.Contains("GreenLight"))
        {
            agent.isStopped = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }

       
    }

    private void Update()
    {               
        if (gameObject.GetComponent<Collider>().enabled == false)
        {
            waited += Time.deltaTime;

            if (waited > waitTime)
            {
                gameObject.GetComponent<Collider>().enabled = true;
                waited = 0;
            }
           
            
        }
    }




}
