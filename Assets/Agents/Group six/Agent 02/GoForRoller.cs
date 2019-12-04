using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class GoForRoller : MonoBehaviour
{
    #region GLOBAL VARIABLES

    public GameObject[] rollers;
    //List<GameObject> distances = new List<GameObject>();
    public float Distance;
    NavMeshAgent killer;
    public List<float> distances = new List<float>();
    public GameObject[] preys;
    bool preySelected;
    

    #endregion




    // Start is called before the first frame update
    void Start()
    {
        //killer = gameObject.transform.parent.gameObject;
        killer = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

       
        if (!preySelected)
        {
            rollers = GameObject.FindGameObjectsWithTag("RollerSkates");
            foreach (GameObject roller in rollers)
            {
                if (roller.GetComponent<blah>().paralyzed == false)
                {
                    Distance = Vector3.Distance(gameObject.transform.position, roller.transform.position);
                    //Debug.Log(Distance + roller.name);
                    distances.Add(Distance);         //making a list of distances
                                                     //Debug.Log(distances);
                    float distancetoprey = distances.Min();
                    Debug.Log("bulshit " + distancetoprey);

                    if (Distance == distancetoprey) // defining prey as target

                    {
                        Debug.DrawLine(roller.transform.position, gameObject.transform.position, Color.blue);

                        killer.SetDestination(roller.transform.position);

                        preySelected = true;

                    }
                }
            }
        }

    }


        void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("RollerSkates"))
        {
            collision.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            preySelected = false;

        }

    }

    
}

