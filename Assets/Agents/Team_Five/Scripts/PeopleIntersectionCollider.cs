using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeopleIntersectionCollider : MonoBehaviour

{
    GameObject parent;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Intersection"))
        {
            agent.isStopped = true;
        }
    }
}
