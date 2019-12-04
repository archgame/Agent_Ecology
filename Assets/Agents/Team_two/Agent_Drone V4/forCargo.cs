using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forCargo : MonoBehaviour
{
    void Start()
    {

        

    }
    void OnTriggerEnter(Collider goods)
    {
        



        if (goods.gameObject.CompareTag("disCharge"))
        {
            print("targettarget");
            GetComponent<Collider>().enabled = false;




        }


    }
}
