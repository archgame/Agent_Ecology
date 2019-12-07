using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Atractor : MonoBehaviour
{
    //Trigget Controllers
    public bool stay = false;
    private bool colliderToggle = true;


    //Time to turn Off Collider
    private float petTime = 0;
    private float timetoPet = 8;

    //Time to turn ON Collider
    private float ColliderOffTime = 0;
    private float ColliderTime = 15;

    private Runner parentRunner;

    //Collison list
    public GameObject[] collTarget = new GameObject[1];
    private GameObject dogTarget;


    private void Start()
    {
        colliderToggle = true;
        parentRunner = gameObject.GetComponentInParent<Runner>();
    }

    void OnTriggerStay (Collider col)
    {

        //guard statement
        if (col.gameObject.tag != "Dog") { return; } ///
        // code goes here
        //Debug.Log("Stay" + col.gameObject.name);
        stay = true;
        colliderToggle = true;
        // List<GameObject> colList = new List<GameObject>();

        dogTarget = col.gameObject;
        collTarget[0] = dogTarget;
        parentRunner.SetTarget(dogTarget);
    }
    void Update()
    {
        
        if (stay)
        {
            if(petTime > timetoPet)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                stay = false;
                colliderToggle = false;
                petTime = 0;
                //Debug.Log("Collider OFF");
               
            }
            else
            {
                petTime += Time.deltaTime;
            }
           
        }
        if(!colliderToggle)
        {
            if(ColliderOffTime > ColliderTime)
            {
                GetComponent<CapsuleCollider>().enabled = true;
                colliderToggle = true;
                ColliderOffTime = 0;
                //Debug.Log("Collider ON");
            }
            else
            {
                ColliderOffTime += Time.deltaTime;
            }
        }
    }

}