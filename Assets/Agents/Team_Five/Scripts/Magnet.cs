using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Magnet : MonoBehaviour
{
    void onTriggerStay (Collider col)
    {
        if (col.gameObject.tag == "Agent") // only register if Agent
        {
            //code goes here
        }

        //guard statement
        if (col.gameObject.tag != "Agent"){ return; }

        NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
        if (agent==null) { return; }


        //magnitude is the length of the vector
        float speed = agent.velocity.magnitude * Time.deltaTime;
        Vector3 direction = col.gameObject.transform.position - transform.position;
        Debug.DrawRay(col.gameObject.transform.position, direction, Color.green);

        Vector3 velocity = direction * speed * 0.5f;
        float angle = Vector3.Angle(velocity, agent.velocity);



        if (angle > 90)
        {

            Debug.DrawRay(col.gameObject.transform.position, direction, Color.green);
            agent.velocity += velocity;
        }
       


    }
}
