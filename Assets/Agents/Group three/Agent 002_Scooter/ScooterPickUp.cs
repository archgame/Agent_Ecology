using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScooterPickUp : MonoBehaviour
{
    GameObject RIDER;
    NavMeshAgent CONTAINER;
    ScooterTargetGo ScooterGo;
    public GameObject SCOOTER;

    float currentRotY;
    float startRotY;
    float endRotY;



    //public List<GameObject> SCOOTERS = new List<GameObject>();
    public List<int> passengerStops = new List<int>();
    private bool isStopped = false;
    public int boardAtStop = 0;

   

    public bool a = false;

    // Start is called before the first frame update
    void Start()
    {
        RIDER = gameObject.transform.parent.gameObject;
        //agent = parent.GetComponent<NavMeshAgent>();
        CONTAINER = gameObject.GetComponentInParent<NavMeshAgent>();
        ScooterGo = gameObject.GetComponentInParent<ScooterTargetGo>();


    }

    // Update is called once per frame
    void Update()
    {
        //first stop frame
        //if (ScooterGo.daozhan == true && isStopped == false)
        if (ScooterGo.daozhan == true)
        {
            // PEOPLE.GetComponentInChildren<MeshRenderer>().enabled = false;
            // PEOPLE.GetComponentInChildren<Transform>().Rotate(0, 0, 90);
            // a = true;

            //adding each rider on the list
            /*foreach (GameObject SCOOTER in SCOOTERS)
            {
                SCOOTER.transform.SetParent(PEOPLE.transform);
                //BOARD.GetComponent<NavMeshAgent>().enabled = false;
                //BOARD.GetComponent<MeshRenderer>().enabled = false ;
                //BOARD.GetComponent<Collider>().enabled = false;
            }*/

            //SCOOTER.transform.parent(.transform);
            gameObject.transform.DetachChildren();



            /*currentRotY = 0;
            startRotY = 0;
            endRotY = 45;
            SCOOTER.transform.rotation = Quaternion.Euler(0, 90, 0);

            currentRotY++;
            currentRotY = Mathf.Clamp(currentRotY, startRotY, endRotY);
            SCOOTER.transform.rotation = Quaternion.Euler(0, currentRotY, 0);*/


            isStopped = true;

            #region ??
            //deboarding
            /*List<int> removeIndexes = new List<int>();
            for (int i = 0; i < passengerStops.Count; i++)
            {
                passengerStops[i]++;
                //*
                if (passengerStops[i] == 1)
                {

                    GameObject boards = BOARDS[i];
                    Debug.Log("agent deboards: " + boards.name);
                    NavMeshAgent b = boards.GetComponent<NavMeshAgent>();
                    b.enabled = true;
                    b.isStopped = false;
                    //agent.speed = 18;
                    //passenger.GetComponent<MeshRenderer>().enabled = true;
                    boards.GetComponent<Collider>().enabled = true;
                    boards.transform.SetParent(null);
                    removeIndexes.Add(i);
                    
                }
                
            }

            removeIndexes.Reverse();
            foreach (int i in removeIndexes)
            {
                BOARDS.RemoveAt(i);
                passengerStops.RemoveAt(i);
            }*/
            #endregion
        }

        //reste isStopped variable for next stop
        if (!CONTAINER.isStopped)
        {
            isStopped = false;
        }


        //speed
        if (gameObject.transform.childCount >0 )
        {
            CONTAINER.speed = 12;
        }
        else
        {
            CONTAINER.speed = 3.5f;

        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Scooter") 
        {
            //Debug.Log("collision: " + collision.name);
            GameObject newscooter = collision.gameObject;
            //newscooter.transform.parent = RIDER.transform;

            newscooter.transform.SetParent(gameObject.transform);
            //make sure the collision is not already a child of the bus
            //if (agent.isStopped)
            //{
            /*if (scooter.GetComponent<ScooterTargetGo>().isRider == false)
            {
                scooter.GetComponent<ScooterTargetGo>().isRider = true;
                //Debug.Log("passenger added");
                //passenger.transform.SetParent(parent.transform);
                //passenger.GetComponent<NavMeshAgent>().enabled = false;
                //passenger.GetComponent<MeshRenderer>().enabled = false;
                //passenger.GetComponent<Collider>().enabled = false;

                BOARDS.Add(passenger);
                passengerStops.Add(0);
                     }
            //}*/

        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Scooter")
        {

            /*GameObject passenger = collision.gameObject;
            Debug.Log("exit triggered: " + passenger.name);
            passenger.GetComponent<ScooterTargetGo>().isRider = false;
            passenger.GetComponent<NavMeshAgent>().isStopped = false;*/
            ///passenger.GetComponent<NavMeshAgent>().Resume();
            //passenger.GetComponent<ScooterTargetGo>().position = 
            //passenger.GetComponent<ScooterTargetGo>().targets[0].transform.position;


            GameObject newscooter = collision.gameObject;
            //gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("shabi");
        }
    }

}
