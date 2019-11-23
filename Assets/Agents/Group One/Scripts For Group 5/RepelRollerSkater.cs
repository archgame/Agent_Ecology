using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RepelRollerSkater : MonoBehaviour
{ 
    void OnTriggerStay(Collider col)
    {
        //guard statement
        if(col.gameObject.tag != "RollerSkates") { return; }
        Debug.Log("Stay: " + col.gameObject.name);
    
        NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
        if(agent == null) { return;}

        float speed = agent.velocity.magnitude * Time.deltaTime;
        Vector3 direction = col.gameObject.transform.position - transform.position;

        Vector3 velocity = direction * speed * 0.4f;
        float angle = Vector3.Angle(velocity, agent.velocity);
        if (angle > 90)
        {
            Debug.DrawRay(col.gameObject.transform.position, velocity, Color.green);
            agent.velocity += velocity;

        }
    }
}
