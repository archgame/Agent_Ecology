using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class trashpickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Contains("trashCans"))
        {
            GameObject passenger = collision.gameObject;
            Destroy(passenger, 1);

        }
       
    }

}
