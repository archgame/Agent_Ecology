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
    public GameObject leader;
    public GameObject meetinPoint;
    public bool GO = false;
    public GameObject test;
    public Material alphamat;
    private float time;
    public GameObject[] alphas;

    #endregion




    // Start is called before the first frame update
    void Start()
    {


        preySelected = false;
        //killer = gameObject.transform.parent.gameObject;
        killer = gameObject.GetComponent<NavMeshAgent>();
        //killer.SetDestination(meetinPoint.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if (GO == false) //if the park is asking you to leave
        {
            killer.SetDestination(meetinPoint.transform.position);
        }



        if (alpha == true)  //going after prey
        {
            leader = gameObject;
            leader.GetComponent<MeshRenderer>().material = alphamat;
            leader.GetComponent<CapsuleCollider>().radius = 12;
            //Debug.Log("leader selected");

            if (preySelected)    //radar flashes to target
            {
                time += Time.deltaTime;
                if (time > 2) 
                {
                    time = 0;
                    killer.SetDestination(prey.transform.position);
                    Debug.DrawLine(prey.transform.position, gameObject.transform.position, Color.blue,1);
                }

            }

        }
       
        if (!preySelected && alpha == true && GO == true)  //choosing the prey
        {
            rollers = GameObject.FindGameObjectsWithTag("RollerSkates");
            float mindistance = 10000000;


            foreach (GameObject roller in rollers)
            {
                
                if (roller.GetComponent<RollerSkater>().paralyzed == false)
                {
                    Distance = Vector3.Distance(gameObject.transform.position, roller.transform.position);   //Debug.Log(Distance + roller.name);

                    //Debug.Log( Distance);

                    if (Distance < mindistance && roller.GetComponent<RollerSkater>().paralyzed == false)    // defining prey as target
                    {
                        //Debug.Log("prey selected");
                        mindistance = Distance;
                        prey = roller;            
                        preySelected = true;
                        

                    }
                }
            }
        }

        if (alpha == false && GO == true)   // follow your leader
        {

            //Debug.DrawLine(gameObject.transform.position, leader.transform.position, Color.green, 2);   //funny trail debug
            killer.SetDestination(leader.GetComponent<NavMeshAgent>().destination);

        }






    }


    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == ("RollerSkates") && collision.gameObject)
        {
            //collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            if (collision.gameObject.GetComponent<RollerSkater>() != null)
                collision.gameObject.GetComponent<RollerSkater>().paralyzed = true;

            preySelected = false;
            GO = false;

            Debug.DrawLine(collision.transform.position, gameObject.transform.position, Color.red, 3);


        }



    }

    void OnTriggerStay (Collider collision)
    {

    }
    
}

