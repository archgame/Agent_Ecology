using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class YellowLight : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
    void OnTriggerStay(Collider col)
    {
        //guard statement
        if (col.gameObject.layer != LayerMask.NameToLayer("Pedestrian") || col.gameObject.layer != LayerMask.NameToLayer("Car")) { return; } // Check to see if schoolchild
        Debug.Log("Stay: " + col.gameObject.name);

        NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>(); //do we need this? No agent data comes from game object not collider
        if (agent == null) { return;}

        float speed = agent.velocity.magnitude * Time.deltaTime;
        Vector3 direction = col.gameObject.transform.position - transform.position; //flip this

        Vector3 velocity = direction * speed * 0.4f;
        float angle = Vector3.Angle(velocity, agent.velocity);
        if (angle > 90)
        {
            Debug.DrawRay(col.gameObject.transform.position, velocity, Color.green);
            agent.velocity += velocity;

        }
    }
}
