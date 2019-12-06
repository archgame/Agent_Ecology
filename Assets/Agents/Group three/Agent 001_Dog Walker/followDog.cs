using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followDog : MonoBehaviour
{

    public Transform goal;
    public float speed = 5f;
    public float accuracy = 1.0f;
    public float rotSpeed = 1.0f;
    //public float distance;
    //public GameObject from;

    // Use this for initialization
    void Start()
    {

    }
    private void Update()
    {
        /*distance = 1;
        float dist = Vector3.Distance(from.transform.position, transform.position);
        Debug.Log(dist);
        if (dist > distance)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
        else
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }*/
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x,
                                        this.transform.position.y,
                                        goal.position.z);
        Vector3 direction = lookAtGoal - this.transform.position  ;
        this.transform.Translate(direction * speed * Time.deltaTime);
        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
        //                                       Quaternion.LookRotation(direction),
        //                                      Time.deltaTime * rotSpeed);

       // this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
