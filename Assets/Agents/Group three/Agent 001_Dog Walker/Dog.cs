﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour
{
    public bool randomScale = true;
    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;

    public Transform goal;
    public float speed = 5f;
    public float accuracy = 1.0f;
    public float rotSpeed = 1.0f;
    //public float distance;
    //public GameObject from;

    // Use this for initialization
    void Start()
    {
        if (randomScale)
        {
            float x = Random.Range(xmin, xmax);
            float y = Random.Range(ymin, ymax);
            float z = Random.Range(zmin, zmax);
            transform.localScale = new Vector3(x, y, z);
        }
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
        Vector3 direction = lookAtGoal - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                Time.deltaTime * rotSpeed);

        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}

