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
        }
       
        if (!preySelected && alpha == true && GO == true)
        {
            rollers = GameObject.FindGameObjectsWithTag("RollerSkates");
            foreach (GameObject roller in rollers)
            {
                //if (1==1)
                if (roller.GetComponent<RollerSkater>().paralyzed == false)
                {
                    Distance = Vector3.Distance(gameObject.transform.position, roller.transform.position);   //Debug.Log(Distance + roller.name);

                    distances.Add(Distance);            //making a list of distances

                    float distancetoprey = distances.Min();
                    Debug.Log("bulshit " + distancetoprey);

                    if (Distance == distancetoprey)    // defining prey as target

                    {

                        Debug.DrawLine(roller.transform.position, gameObject.transform.position, Color.blue);
                        killer.SetDestination(roller.transform.position);
                        preySelected = true;

                    }
                }
            }
        }
        if (GO == true && !alpha)
        {
            killer.SetDestination(leader.transform.position);

        }

    }


    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == ("RollerSkates"))
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            preySelected = false;

        }


        if (collision.gameObject.tag == ("Robot"))
        {
            //afterPark = true;
            //collision.gameObject.GetComponent<Transform>().rotation.x = 90;


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

