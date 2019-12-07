using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class another: MonoBehaviour
{

    NavMeshAgent agent;
    public int changeTargetDistance = 1;
    public GameObject[] targets;
    public GameObject[] pickup;
   // public GameObject[] riders;
    GameObject target;
    public string[] targetNames;
    public GameObject cloest = null;
    public bool shuffleTargets = true;
    public Transform leaveaway;

    private int t = 0;
    public GameObject[] riders;

    float waited = 0;
    int waitTime = 2;

    public GameObject[] stopBikes;
    GameObject x;
    // Start is called before the first frame update
    void Start()
    {
       // GameObject[] gos = GameObject.FindGameObjectsWithTag("Target");
        agent = GetComponent<NavMeshAgent>();
        riders = GameObject.FindGameObjectsWithTag("Riders");

        if (targets.Length == 0)
        {
            targets = GameObject.FindGameObjectsWithTag("Target");
            List<GameObject> targetList = new List<GameObject>();
            foreach (GameObject go in targets)
            {
                foreach (string targetName in targetNames)
                {
                    if (go.name.Contains(targetName))
                    {
                        targetList.Add(go);
                    }
                }
            }
            targets = targetList.ToArray();
        }

        if (shuffleTargets)
        {
            targets = Shuffle(targets);
        }
        t = 0;
        target = targets[t];


    }

    // Update is called once per frame
    void Update()
    {
        if(stopBikes.Length == 0)
        {
            stopBikes = GameObject.FindGameObjectsWithTag("stop bike");
        }
        if (agent.enabled)
        {
            Debug.Log("children" + transform.childCount);
            //GameObject cloest = null;
            if(cloest == null)
            {
                float distance = Mathf.Infinity;
                Vector3 position = transform.position;
                foreach (GameObject go in stopBikes)
                {
                    if (go.transform.childCount == 0)
                    {
                        Vector3 diff = go.transform.position - position;
                        Debug.Log("assign");
                        float curDistance = diff.sqrMagnitude;
                        if (curDistance < distance)
                        {
                            cloest = go;
                         
                            distance = curDistance;
                        }
                    }
                }
                agent.SetDestination(cloest.transform.position);
            }

            float distanceToTarget = Vector3.Distance(transform.position, cloest.transform.position);
            if (0.5f > distanceToTarget)
            {

                if (waited > waitTime)
                {
                    cloest.transform.position = transform.position;
                    cloest.transform.forward = transform.forward;
                    transform.SetParent(cloest.transform);
                    waited = 0;
                    gameObject.GetComponent<NavMeshAgent>().enabled = false;
                    cloest.GetComponent<NavMeshAgent>().isStopped = false;
                    cloest.GetComponent<NavMeshAgent>().SetDestination(leaveaway.position);

                }
                else
                {
                    waited += Time.deltaTime;
                }
            }

            // Debug.Log(cloest);

            // pickup = GameObject.FindGameObjectsWithTag("stop bike");
            //List<GameObject> pickups= new List<GameObject>();
            // pickups = pickup.ToList();
            // int rnd = Random.Range(0, pickups.Count);

            //for (int i = 0; i<pickup.Length ; i++)
            //{

            //    float distanceToTarget = Vector3.Distance(riders[i].transform.position, pickup[i].transform.position);
            //    if (0.5f > distanceToTarget)
            //    {

            //        riders[i].transform.SetParent(pickup[i].transform);
            //        riders[i].GetComponent<NavMeshAgent>().enabled = false;
            //        pickup[i].GetComponent<NavMeshAgent>().isStopped = false;
            //        pickup[i].GetComponent<NavMeshAgent>().SetDestination(leaveaway.position);
            //    }
            //    else
            //    {
            //        riders[i].GetComponent<NavMeshAgent>().SetDestination(pickup[i].transform.position);
            //    }








            //}






        }
    }
    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGO;
        for (int i = 0; i < objects.Length; i++)
        {
            //Debug.Log("i: " + i);
            int rnd = Random.Range(0, objects.Length);
            tempGO = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempGO;

        }
        return objects;
    }

    //void OnTriggerEnter(Collider bird)
    //{
    //    if (bird.gameObject.CompareTag("stop bike"))
    //    {
    //        if (transform.childCount == 0)
    //        {
    //            Transform getBird = bird.transform.parent;
    //            bird.transform.parent = agent.transform;
    //            bird.transform.forward = agent.transform.forward;


    //            bird.transform.position = agent.transform.position;
    //        }
    //    }
    //}

}
