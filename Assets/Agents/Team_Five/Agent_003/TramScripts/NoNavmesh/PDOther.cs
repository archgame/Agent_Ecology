using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PDOther : MonoBehaviour
{
    public GameObject tram;

    [Header("Speed")]
    public float PassengerSpeed = 0;

    [Header("Targets")]
    public GameObject Bustarget;
    public GameObject Stoptarget;

    [Header("Stations")]
    public int i;
    public int ns;

    [Header("Scale")]
    public float x;
    public float y;
    public float z;






    // Start is called before the first frame update
    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {
        
        
        if(tram.GetComponent<Go>() == null) { return; }
        if (tram.GetComponent<Go>().MyPath[i].Load == true)
        {
            if (tram.GetComponent<Go>().waited > 2)
            {

                gameObject.transform.position = Vector3.MoveTowards(transform.position, Bustarget.transform.position, PassengerSpeed * Time.deltaTime);
                //Debug.Log("went to tram");                                      

                if (tram.GetComponent<Go>().waited > 4)
                {
                //Debug.Log("parent on");
                gameObject.transform.SetParent(Bustarget.transform);
                gameObject.transform.localScale = new Vector3(x, y, z);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;


                }

            }
        }


        if (tram.GetComponent<Go>().MyPath[i + ns].Load == true)
        {
            
            
                //Debug.Log("reached drop off");
                gameObject.transform.SetParent(null);
                gameObject.transform.localScale = new Vector3(x, y, z);
                gameObject.GetComponent<PedPassengerOther>().enabled = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<Collider>().enabled = true;
                //gameObject.transform.position = Vector3.MoveTowards(transform.position, Stoptarget.transform.position, PassengerSpeed * Time.deltaTime);
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                gameObject.GetComponent<PedPassengerOther>().agent.isStopped = false;
                gameObject.GetComponent<PedPassengerOther>().agent.SetDestination(Stoptarget.transform.position);

                gameObject.GetComponent<PedPassengerOther>().waited = gameObject.GetComponent<PedPassengerOther>().waitTimeStop;
                gameObject.GetComponent<PedPassengerOther>().waitTime = 0;
            

            tram.GetComponent<Go>().MyPath[i].Load = false; // new delete?

            if (i == 0)
            {
                i = tram.GetComponent<Go>().MyPath.Length - 1;
                ns = -(tram.GetComponent<Go>().MyPath.Length - 2);
            }
            else
            {
                i = i;
            }

            if (gameObject.GetComponent<PedPassengerOther>().x == 0)
            {
                gameObject.GetComponent<PedPassengerOther>().x = tram.GetComponent<Go>().MyPath.Length - 1;
            }
            else
            {
                gameObject.GetComponent<PedPassengerOther>().x = gameObject.GetComponent<PedPassengerOther>().x;
            }
            gameObject.GetComponent<PDOther>().enabled = false;


        }


    }



}


       