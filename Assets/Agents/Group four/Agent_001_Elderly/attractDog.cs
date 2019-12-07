using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class attractDog : MonoBehaviour
{
    public string tagName;


    void OnTriggerStay(Collider col)
    {
        //guard statement
        if (col.gameObject.tag != tagName)
        { return; }
        //Debug.Log("Stay: " + col.gameObject.name);

        NavMeshAgent agent = col.gameObject.GetComponent<NavMeshAgent>();
        if (agent == null)
        { return; }

        float speed = agent.velocity.magnitude * Time.deltaTime;
        //Vector3 direction = col.gameObject.transform.position - transform.position;
        Vector3 direction =transform.position - col.gameObject.transform.position ;

        Vector3 velocity = direction * speed * 0.5f;
        float angle = Vector3.Angle(velocity, agent.velocity);
        if (angle < 90)  //angle < 90
        {
            Debug.DrawRay(col.gameObject.transform.position, velocity, Color.black);
            //Debug.Log("1111");
            agent.velocity += velocity;
        }


    }
}
