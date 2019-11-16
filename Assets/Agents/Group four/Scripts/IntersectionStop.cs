using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IntersectionStop : MonoBehaviour
{
   



    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            GameObject passenger = collision.gameObject;


            passenger.GetComponent<NavMeshAgent>().speed = 0;
            Debug.Log("exit triggered: " + passenger.name);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("SmallVehicle"))
        {
            GameObject passenger = collision.gameObject;
            passenger.GetComponent<NavMeshAgent>().speed = 0;
            Debug.Log("exit triggered: " + passenger.name);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("LargeVehicle"))
        {
            GameObject passenger = collision.gameObject;
            passenger.GetComponent<NavMeshAgent>().speed = 0;
            Debug.Log("exit triggered: " + passenger.name);
        }
    }

    void OnTriggerExit(Collider collision)
    {


        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            GameObject passenger = collision.gameObject;
            passenger.GetComponent<NavMeshAgent>().speed = 1.5f;
            Debug.Log("exit triggered: " + passenger.name);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("SmallVehicle"))
        {
            GameObject passenger = collision.gameObject;
            passenger.GetComponent<NavMeshAgent>().speed = 6;
            Debug.Log("exit triggered: " + passenger.name);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("LargeVehicle"))
        {
            GameObject passenger = collision.gameObject;
            passenger.GetComponent<NavMeshAgent>().speed = 15;
            Debug.Log("exit triggered: " + passenger.name);
        }
    }


    /*void OnTriggerStay(Collider collision)
    {
        GameObject passenger = collision.gameObject;
        passenger.GetComponent<NavMeshAgent>().isStopped = true;
    }*/
}
