using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MODI : MonoBehaviour
{

    public GameObject LastPassenger;

    
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (LastPassenger.GetComponent<MeshRenderer>().enabled == true)
        {
           
            GetComponentInParent<FollowGoalMotorcycle>().enabled = false;
            GetComponentInParent<Motorcycle>().enabled = true;
            GetComponentInParent<NavMeshAgent>().speed = 15;
        }
    }
}
