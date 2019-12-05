using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneControlV2N : MonoBehaviour
{

    public GameObject drone;
    //public float speed = 1.0f;
    public GameObject[] targets;

    //public string targetName = "";

    private int t;

    public int maximumAddHeight = 15;
    public int maximumReduceHeight = 5;
    private float addHeight = 0;
    //private float h = 0.01f;

    public float lerpTime = 5;
    public float currentlerpTime = 0;

    Transform target;
    public float changeTargetDistance = 1;
    public bool ShuffleTargets = true;

    [HideInInspector]
    public Vector3 hangingpoint;
    [HideInInspector]
    public Vector3 newlocaltargetpoint;
    
    //private Vector3 localdronepoint;

    private bool hover = false;

    public bool finishTour = false;


    //public Vector3 prePoition;
    //public Vector3 rotateTo;
    //public Vector3 newDir;
    




    //public float twoDroneDistance = 1;
    //public GameObject[] Dronecol;


    void Start()
    {

        if (targets == null || targets.Length == 0)
        {
            //List<GameObject> targetList = new List<GameObject>();            
            targets = GameObject.FindGameObjectsWithTag("dronetarget2");
            //foreach (GameObject go in targets)
            //{
            //    if(go.name == targetName)
            //    {
            //        targetList.Add(go);
            //    }
            //    targets = targetList.ToArray();
            //}      
        }

        targets = Shuffle(targets);




    }

    void Update()
    {
        if (gameObject.GetComponent<droneControlV1N>().getheight == true)
        {
            gameObject.GetComponent<droneControlV1N>().keyHit = false;
            gameObject.GetComponent<droneControlV1N>().enabled = false;

            //hangingpoint = GetComponent<droneControlV1>().drone.transform.position;

            float h = Random.Range(-0.001f, 0.001f);
            addHeight = (addHeight + h);

            //print(addHeight);
            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    addHeight =+ h;
            //}
            if ((hangingpoint.y + addHeight) >= maximumAddHeight)
            {
                addHeight = 0;
            }
            //if (Input.GetKeyDown(KeyCode.V))
            //{
            //    addHeight =- h;            
            //}
            if ((hangingpoint.y + addHeight) <= maximumReduceHeight)
            {
                addHeight = 0;
            }

            //float step = speed * Time.deltaTime;
            target = targets[t].transform;

            currentlerpTime += Time.deltaTime;

            if (currentlerpTime >= lerpTime)
            {
                currentlerpTime = lerpTime;
            }
            float perc = currentlerpTime / lerpTime / 30;

            hangingpoint = GetComponent<droneControlV1N>().drone.transform.position;
            //Debug.Log("hp" + hangingpoint);
            //Vector3 hangingPoint = Vector3 localpoint;



            Vector3 localtargetpoint = new Vector3(target.transform.position.x,
                hangingpoint.y + addHeight,
                target.transform.position.z);


            Vector3 localdronepoint = new Vector3(drone.transform.position.x,
                hangingpoint.y + addHeight,
                drone.transform.position.z);

            
            
            

            //Dronecol = GameObject.FindGameObjectsWithTag("Drone");
            //twoDroneDistance = Vector3.Distance(drone.transform.position, Dronecol[0].transform.position);

            if (hover == false)
            {
                drone.transform.position = Vector3.MoveTowards(localdronepoint, localtargetpoint, perc);

                //transform.position += transform.forward * Time.deltaTime * step;

                //drone.transform.position = Vector3.Lerp(localdronepoint,
                //localtargetpoint + drone.transform.forward,
                //Time.deltaTime * 10);

                //Vector3 targetDir = localtargetpoint - localdronepoint;
                //Vector3 newDir = Vector3.RotateTowards(drone.transform.forward, targetDir, step, 0.0f);
                //drone.transform.rotation = Quaternion.LookRotation(newDir);
            }

            if (Vector3.Distance(localdronepoint, localtargetpoint) < changeTargetDistance && t <= targets.Length - 1)
            {
                t++;
                //if (t == targets.Length)
                //{
                //    t = 0;
                //}
                //target = targets[t].transform;
                if (hover == false)
                {
                    drone.transform.position = Vector3.MoveTowards(localdronepoint, localtargetpoint, perc);

                    //drone.transform.position = Vector3.Lerp(localdronepoint,
                    //localtargetpoint + drone.transform.forward,
                    //Time.deltaTime * 10); ;

                    //transform.position += transform.forward * Time.deltaTime * step;
                }
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                hover = !hover;
            }

            if (t >= targets.Length)
            {
                gameObject.GetComponent<droneControlV3N>().enabled = true;
                gameObject.GetComponent<droneControlV1N>().getheight = false;
                finishTour = true;
                t = 0;
                targets = Shuffle(targets);
            }

            int p = targets.Length - 1;
            newlocaltargetpoint = GetComponent<droneControlV2N>().targets[p].transform.position;

            //print("p" + " " + newlocaltargetpoint);
        }



       

    }
    GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGo;
        for (int i = 0; i < objects.Length; i++)
        {
            int rnd = Random.Range(0, objects.Length);
            tempGo = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempGo;
        }
        return objects;
    }






}