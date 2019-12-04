using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public GameObject goods;
public class forCargo : MonoBehaviour
{

    private bool destroy = false;
    void Start()
    {

        

    }
    void OnTriggerEnter(Collider goods)
    {
        
        if (goods.gameObject.CompareTag("disCharge"))
        {
            //print("targettarget");
            GetComponent<Collider>().enabled = false;

            


        }

    }
    void OnTriggerExit(Collider goods)
    {
    
        if (goods.gameObject.CompareTag("Drone"))
        {

            //print("333333");
            destroy = true;

        }

    }

    private void Update()
    {
        if (transform.parent == null && destroy == true)
        {

            //print("killllll");
            Destroy(gameObject, 2f);
        }
    }



}
