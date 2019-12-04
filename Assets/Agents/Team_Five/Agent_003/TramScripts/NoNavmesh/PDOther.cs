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

        if (tram.GetComponent<Go>().MyPath[i].Load == true)
        {
            if (tram.GetComponent<Go>().waited > 2)
            {
                Debug.Log("got waiting");
                //gameObject.GetComponent<NavMeshAgent>().areaMask;

                gameObject.transform.position = Vector3.MoveTowards(transform.position, Bustarget.transform.position, PassengerSpeed * Time.deltaTime);
                Debug.Log("went to bus other");
                
            }

            if (tram.GetComponent<Go>().ReadyToGo == true)
            {
                Debug.Log("parent on other");
                gameObject.transform.localScale = new Vector3(x, y, z);
                gameObject.transform.SetParent(Bustarget.transform);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;
                

            }

        }


        if (tram.GetComponent<Go>().MyPath[i + ns].Load == true)
        {
            if (tram.GetComponent<Go>().waited > 2)
            {
                Debug.Log("reached drop off other");
                gameObject.transform.SetParent(null);
                gameObject.transform.localScale = new Vector3(x, y, z);
                gameObject.GetComponent<PedPassengerOther>().enabled = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<Collider>().enabled = true;
                gameObject.transform.position = Vector3.MoveTowards(transform.position, Stoptarget.transform.position, PassengerSpeed * Time.deltaTime);
                gameObject.GetComponent<PDOther>().enabled = false;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
               
            }


        }


    }



}