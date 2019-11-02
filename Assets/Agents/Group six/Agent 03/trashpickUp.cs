using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class trashpickUp : MonoBehaviour
{
    //GameObject parent;
   // NavMeshAgent agent;
   // public List<GameObject> riders = new List<GameObject>();
   // public List<int> passengerStops = new List<int>();
    //private bool isStopped = false;


    // Start is called before the first frame update
   
   // void Start()
   // {
        //parent = gameObject.transform.parent.gameObject;
       // agent = parent.GetComponent<NavMeshAgent>();
   // }

    // Update is called once per frame

    /*
   void Update()
   {

       /*
       //fisrt stop frame
       if (agent.isStopped == true && isStopped == false)
       {
           isStopped = true;

           List<int> removeIndexes = new List<int>();
           for (int i = 0; i < passengerStops.Count; i ++)
           {
               passengerStops[i]++;
               if (passengerStops[i] == 2) 
               {
                   Debug.Log("agent deboards");
                   GameObject passenger = riders[i];
                   NavMeshAgent a = passenger.GetComponent<NavMeshAgent>();
                   a.enabled = true;
                   a.isStopped = true;
                   passenger.GetComponent<MeshRenderer>().enabled = true;
                   passenger.GetComponent<Collider>().enabled = true;
                   removeIndexes.Add(i);
               }
           }
           removeIndexes.Reverse();
           foreach(int i in removeIndexes)
           {
               riders.RemoveAt(i);
               passengerStops.RemoveAt(i);
           }

       }

       if(!agent.isStopped)
       {
           isStopped = false;

   }
   

    }
    */



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Contains("trashCans"))
        {
            GameObject passenger = collision.gameObject;
            Destroy(passenger, 1);

            /*
             * 
            Debug.Log("collision " + collision.name);
            GameObject passenger = collision.gameObject;
            //make sure the collision is not aready a child of the bus
            //if (passenger.transform.parent == null) //||
            //passenger.transform.parent.gameObject != transform.parent.gameObject) 

            Debug.Log("passenger added");
            passenger.transform.SetParent(parent.transform);
            passenger.GetComponent<MeshRenderer>().enabled = false;
            passenger.GetComponent<Collider>().enabled = false;
            passenger.GetComponent<NavMeshAgent>().enabled = false;
            /*
            if (passenger.transform.parent.gameObject != transform.parent.gameObject)
            {

                Debug.Log("passenger added");
                passenger.transform.SetParent(parent.transform);
                passenger.GetComponent<MeshRenderer>().enabled = false;
                passenger.GetComponent<Collider>().enabled = false;
                passenger.GetComponent<NavMeshAgent>().enabled = false;

               // riders.Add(passenger);
               // passengerStops.Add(0);
            }
            */

        }
       
    }

    /*
    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            Debug.Log("exited triggered");
            GameObject passenger = collision.gameObject;
            passenger.transform.SetParent(null);

        }
    }
    */
}
