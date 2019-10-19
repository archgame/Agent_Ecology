using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fly2 : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public GameObject[] targets;
    public int changeTargetDistance = 1;
    int t;

    
    private Vector3 startPos;
    private Vector3 endPos;
    public float distance = 30f;
    public float lerpTime = 5;
    private float currentlerpTime = 0;
    private bool keyHit = false;

    // Start is called before the first frame update
  
    void Start()
    {


        ////Debug.Log(this.name + "has" + targets.Length + "Targets");
        agent = GetComponent<NavMeshAgent>();
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);

    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector3.Distance(agent.transform.position, target.position);
        //Debug.Log("ditanceToTarget" + distanceToTarget);
        if (changeTargetDistance > distanceToTarget)

        {
            t++;
            if (t == targets.Length)
            {
                t = 0;
                //Debug.Log("finishfullrun");
            }

        }
        //Debug.Log(this.name + "change target" + " "+ t);
        target = targets[t].transform;
        agent.SetDestination(target.position);

        startPos = agent.transform.position;
        endPos = agent.transform.position + Vector3.up * distance;
        Debug.Log("startPos: " + startPos);
        Debug.Log("endPos: " + endPos);
        float perc = currentlerpTime / lerpTime;
        agent.transform.position = Vector3.Lerp(startPos, endPos, perc);



    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyHit = true;
        }

        if (keyHit == true)
        {
            currentlerpTime += Time.fixedDeltaTime;
            if (currentlerpTime >= lerpTime)
            {
                currentlerpTime = lerpTime;
            }
           
        }

    }



}