﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FollowGoalMotorcycle : MonoBehaviour {

	public Transform goal;
	public float speed = 15.0f;
	float accuracy = 1.0f;
	float rotSpeed = 1.0f;
    NavMeshAgent agent;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 lookAtGoal = new Vector3(goal.position.x, 
										this.transform.position.y, 
										goal.position.z);
		Vector3 direction = lookAtGoal - this.transform.position;

		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
												Quaternion.LookRotation(direction), 
												Time.deltaTime*rotSpeed);

		this.transform.Translate(0,0,speed*Time.deltaTime);
	}

  

    private int obstacles = 0;
}
