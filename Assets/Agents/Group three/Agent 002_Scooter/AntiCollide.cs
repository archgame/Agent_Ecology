using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class AntiCollide : MonoBehaviour
{
    private int obstacles = 0;


    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("SmallVehicle"))
        {
            GetComponentInParent<NavMeshAgent>().speed = 0f;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        //Debug.Log("exited");
        if (collision.gameObject.layer == LayerMask.NameToLayer("SmallVehicle"))
        {
            obstacles--; //obstacles = obstacles - 1; || obstacles -= 1;
        }
        if (obstacles == 0) //once there are zero obstacles, start the agent moving
        {
            GetComponentInParent<NavMeshAgent>().speed = Random.Range(9, 12);
        }
    }

}
