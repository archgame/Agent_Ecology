using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PUpDoff : MonoBehaviour
{
    public GameObject tram;
    public List<int> passengerStops = new List<int>();
    public float PassengerSpeed = 0;
    public GameObject Tramtarget;
    public GameObject Stoptarget;





    // Start is called before the first frame update
    void Start()
    {


    }


    void Update()
    {


        if (tram.GetComponent<TramGo>().MyPath[0].Reached == true)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, Tramtarget.transform.position, PassengerSpeed * Time.deltaTime);
            Debug.Log("went to bus");

            if (tram.GetComponent<TramGo>().ReadyToGo == true)
            {
                gameObject.transform.SetParent(Tramtarget.transform);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;

            }

        }


        if (tram.GetComponent<TramGo>().MyPath[1].Reached == true)
        {
            gameObject.transform.SetParent(null);
            gameObject.GetComponent<PedPassenger>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.transform.position = Vector3.MoveTowards(transform.position, Stoptarget.transform.position, PassengerSpeed * Time.deltaTime);
            gameObject.GetComponent<PUpDoff>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;

        }



    }


}