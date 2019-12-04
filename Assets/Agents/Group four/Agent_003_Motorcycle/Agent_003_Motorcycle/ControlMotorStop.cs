using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ControlMotorStop : MonoBehaviour
{
    NavMeshAgent agent;
    public bool ifMotor;
  
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("collision: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {

            agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
        if (collision.gameObject.name.Contains("RedLight"))
        {
            if (gameObject.GetComponent<Motorcycle>().enabled == true)
            {
                gameObject.GetComponent<Motorcycle>().enabled = false;
                ifMotor = true;
                gameObject.GetComponent<NavMeshAgent>().speed=0;
                
            }
            else
            {
                gameObject.GetComponent<FollowGoalMotorcycle>().enabled = false;
                ifMotor = false;
                gameObject.GetComponent<NavMeshAgent>().speed = 0;
            }
          
         
            //agent.isStopped = true;
            obstacles++; // obstacles = obstacles + 1; || obstacles += 1;
        }
        if (collision.gameObject.name.Contains("GreenLight"))
        {
            obstacles--; //count as obstacle removal
            if (obstacles < 0)
            {
                obstacles = 0;
            }

            if (obstacles == 0) //once there are zero obstacles, start the agent moving
            {
                if (ifMotor == true)
                {
                    gameObject.GetComponent<NavMeshAgent>().speed = 12;
                    gameObject.GetComponent<Motorcycle>().enabled = true;
                   
                }
                else
                {
                    gameObject.GetComponent<NavMeshAgent>().speed = 12;
                    gameObject.GetComponent<FollowGoalMotorcycle>().enabled = true;
                    
                }
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        //Debug.Log("exited");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pedestrian"))
        {
            obstacles--; //obstacles = obstacles - 1; || obstacles -= 1;
        }
        if (obstacles == 0) //once there are zero obstacles, start the agent moving
        {
            if (agent != null)
            {
                agent.isStopped = false;
            }
        }
    }

    private int obstacles = 0;
}
