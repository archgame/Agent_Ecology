using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class rest : MonoBehaviour
{
    [Header("rest")]
    public float restdistance;
    public GameObject restpoint;
    public float restTime = 0;
    private float rested = 0;
    public GameObject parent;
   
    // Start is called before the first frame update
    void Start()
    {
        
        restpoint = gameObject;
        //gameObject.transform.parent.DetachChildren();
        gameObject.GetComponentInChildren<NavMeshAgent>().isStopped = true;
        restpoint.transform.position = parent.transform.position;
        
        rested = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInChildren<NavMeshAgent>().isStopped = true;
        float distancetorestpoint = Vector3.Distance(parent.transform.position, restpoint.transform.position);
        if (distancetorestpoint >= restdistance)
        {
            

            if (rested > restTime)
            {
                restpoint.transform.position = parent.transform.position;

                parent.GetComponent<NavMeshAgent>().isStopped = false;

                rested = 0;
              

            }
            else
            {
                rested += Time.deltaTime;
                parent.GetComponent<NavMeshAgent>().isStopped = true;
                //parent.isStopped = true;
            }
        }
    }
}
