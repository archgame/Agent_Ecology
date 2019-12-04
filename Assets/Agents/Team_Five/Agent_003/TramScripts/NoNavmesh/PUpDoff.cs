using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PUpDoff : MonoBehaviour
{
    public GameObject tram;
    public List<int> passengerStops = new List<int>();
    public float PassengerSpeed = 0;
    public GameObject Bustarget;
    public GameObject Stoptarget;






    // Start is called before the first frame update
    void Start()
    {


    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Target") && gameObject.layer == LayerMask.NameToLayer("Passenger"))
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, Bustarget.transform.position, PassengerSpeed * Time.deltaTime);
            Debug.Log("went to bus");

            if (bus.GetComponent<BusGoToDropOff>().ReadyToGo == true)
            {
                Debug.Log("bus departing");
                gameObject.transform.SetParent(Bustarget.transform);
                gameObject.GetComponent<MeshRenderer>().enabled = false;                
                gameObject.GetComponent<Collider>().enabled = false;

            }
        }
    }
    */

    // Update is called once per frame
    void Update()
    {


        if (tram.GetComponent<Go>().MyPath[0].Reached == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, Bustarget.transform.position, PassengerSpeed * Time.deltaTime);
            Debug.Log("went to bus");

            if (tram.GetComponent<Go>().ReadyToGo == true)
            {
                gameObject.transform.SetParent(Bustarget.transform);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;

            }

        }


        if (tram.GetComponent<Go>().MyPath[1].Reached == true)
        {
            gameObject.transform.SetParent(null);
            gameObject.GetComponent<Pedestrian>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, Stoptarget.transform.position, PassengerSpeed * Time.deltaTime);
            gameObject.GetComponent<PUpDoff>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;


            /*float distanceToTarget = Vector3.Distance(gameObject.transform.position, Stoptarget.transform.position);

            if (distanceToTarget < 1)
            {
                Debug.Log("back to black");
                gameObject.GetComponent<PickUpPassenger>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                gameObject.GetComponent<Pedestrian>().enabled = true;  
                
            }
            */


        }









    }

    /* private void OnTriggerEnter(Collider bus)

     {


             if (bus.GetComponent<BusGoToDropOff>().MyPath.Reached == true)
             {
                 Debug.Log("detected collision");
                 gameObject.transform.position = Vector3.MoveTowards(transform.position, bus.transform.position, PassengerSpeed * Time.deltaTime);
                 Debug.Log("went to bus");

                 //other.transform.parent = gameObject.transform;
                 //other.GetComponent<MeshRenderer>().enabled = false;
                 //other.GetComponent<Collider>().enabled = false;
                 //other.GetComponent<NavMeshAgent>().enabled = false;
             }




     }


     private void OnTriggerExit(Collider other)
     {

         //other.transform.SetParent(null);
         other.transform.parent = null;

     }


      private void OnTriggerEnter(Collider collider)
      {
          if (collider.gameObject.tag==("Bus"))
          {
              Destroy(gameObject);
          }

      }
      */
}