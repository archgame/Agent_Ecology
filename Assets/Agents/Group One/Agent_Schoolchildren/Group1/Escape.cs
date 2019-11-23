using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Escape : MonoBehaviour
{

    private NavMeshAgent agent;
    public GameObject[] Player;
    public float EnemyDistanceRun = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectsWithTag("Dog");
    }

    // Update is called once per frame
    void Update()
    {  foreach (GameObject go in Player)
        {
            float distance = Vector3.Distance(transform.position, go.transform.position);
            Debug.Log("Distance: " + distance);
            if (distance < EnemyDistanceRun)
            {
                Vector3 dirToPlayer = transform.position - go.transform.position;
                Vector3 newPos = transform.position + dirToPlayer;
                agent.SetDestination(newPos);

            }
        }
    }
}
