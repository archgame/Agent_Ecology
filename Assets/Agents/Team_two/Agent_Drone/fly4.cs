using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class fly4 : MonoBehaviour
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
    private bool hover = false;

    public float xmin = 1;
    public float xmax = 1;
    public float ymin = 1;
    public float ymax = 1;
    public float zmin = 1;
    public float zmax = 1;
    // Start is called before the first frame update

    void Start()
    {
        float x = Random.Range(xmin, xmax);
        float y = Random.Range(ymin, ymax);
        float z = Random.Range(zmin, zmax);
        transform.localScale = new Vector3(x, y, z);

        ////Debug.Log(this.name + "has" + targets.Length + "Targets");
        agent = GetComponent<NavMeshAgent>();
        t = 0;
        target = targets[t].transform;
        agent.SetDestination(target.position);
        agent.isStopped = true;

        //startPos = agent.transform.position;
        //endPos = agent.transform.position + Vector3.up * distance;
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
        //Debug.Log("startPos: " + startPos);
        //Debug.Log("endPos: " + endPos);
        float perc = currentlerpTime / lerpTime;
        agent.transform.position = Vector3.Lerp(startPos, endPos, perc);

        if (agent.transform.position.y > distance - 0.05 && hover == false)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
        }

        if (Input.GetKeyDown("d"))
        {
            hover = !hover;
            Debug.Log("d: " + hover);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown("a"))
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

        //if (Input.GetKeyDown("s"))
        //{
        //    keyHit = true;
        //}

        //if (keyHit == true)
        //{
            //startPos = agent.transform.position;
            //endPos = agent.transform.position + Vector3.down * distance;

            //currentlerpTime += Time.fixedDeltaTime;
            //if (currentlerpTime >= lerpTime)
            //{
           //     currentlerpTime = lerpTime;
            //}


        //}


    }



}