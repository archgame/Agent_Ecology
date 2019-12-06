using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class GoForRoller : MonoBehaviour
{
    #region GLOBAL VARIABLES

    public GameObject[] rollers;
    public float Distance;
    NavMeshAgent killer;
    public List<float> distances = new List<float>();
    public GameObject[] preys;
    public GameObject prey;
    bool preySelected;
    public bool alpha;
    GameObject leader;
    public GameObject meetinPoint;
    public bool GO = false;

    #endregion




    // Start is called before the first frame update
    void Start()
    {


        preySelected = false;
        //killer = gameObject.transform.parent.gameObject;
        killer = gameObject.GetComponent<NavMeshAgent>();
        killer.SetDestination(meetinPoint.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (alpha == true)
        {
            leader = gameObject;
            Debug.Log("leader selected");
            if (preySelected)
            {
                killer.SetDestination(prey.transform.position);
                Debug.DrawLine(prey.transform.position, gameObject.transform.position, Color.blue);
            }

        }
       
        if (!preySelected && alpha == true && GO == true)
        {
            rollers = GameObject.FindGameObjectsWithTag("RollerSkates");
            float mindistance = 10000000;


            foreach (GameObject roller in rollers)
            {
                
                if (roller.GetComponent<RollerSkater>().paralyzed == false)
                {
                    Distance = Vector3.Distance(gameObject.transform.position, roller.transform.position);   //Debug.Log(Distance + roller.name);

                    Debug.Log("bulshit " + Distance);

                    if (Distance < mindistance)    // defining prey as target
                    {
                        Debug.Log("prey selected");
                        mindistance = Distance;
                        prey = roller;            
                        preySelected = true;

                    }
                }
            }
        }

        if (GO == true && !alpha)
        {
            Debug.DrawLine(leader.transform.position, gameObject.transform.position, Color.red);
            killer.SetDestination(leader.transform.position);
        }


    }


    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == ("RollerSkates"))
        {
            //collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            collision.gameObject.GetComponent<RollerSkater>().paralyzed = true;
            preySelected = false;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(meetinPoint.transform.position);
            //GO = false;

        }


    }

    void OnTriggerStay (Collider collision)
    {
        /*if (collision.gameObject.GetComponent<gangUp>().strike == true)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }
        */
    }
    
}

