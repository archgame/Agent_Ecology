using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Magnet : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        /*
        if(col.gameObject.tag == "Agent")
        {
            Debug.Log("Stay" + col.gameObject.name);
        }
        */
        
        //guard statement
        if(col.gameObject.tag != "Runner") { return; }
        // code goes here
        Debug.Log("Stay" + col.gameObject.name);

        NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
        if(agent == null) { return; }
        float speed = agent.velocity.magnitude * Time.deltaTime;
        Vector3 direction = col.gameObject.transform.position - transform.position;
        Vector3 velocity = direction * speed*0.75f;

        float angle = Vector3.Angle(velocity, agent.velocity);
        if( angle < 90)
        {
            Debug.DrawRay(col.gameObject.transform.position, direction, Color.green);
            agent.velocity += velocity;
        }
        
    }
}
