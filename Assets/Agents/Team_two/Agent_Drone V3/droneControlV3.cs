using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneControlV3 : MonoBehaviour
{
    public float speed = 1.0F;
    
    public GameObject drone;

    public Vector3 startPos;
    private Vector3 endPos;


    //public float lerpTime = 5;
    //private float currentlerpTime = 0;
    //private bool keyHit = false;
    private float h;
    
    private Vector3 localdronestartPos;
    private Vector3 localdroneendPos;


    //private bool isHeigh;

    // Start is called before the first frame update
    void Start()
    {

        //Vector3 startPos = GetComponent<droneControlV2>().localdronepoint;
        //Vector3 localdronestartPos = drone.transform.position;
        //Vector3 localdronepoint = new Vector3(startPos.x, startPos.y, startPos.z);
       
        

    }

    // Update is called once per frame
    void Update()
    {

        h = GetComponent<droneControlV2>().drone.transform.position.y;

        //Debug.Log("H " + drone.transform.position);
        //Debug.Log("Hlocal " + transform.position);

        Vector3 localdronestartPos = new Vector3(drone.transform.position.x,
            drone.transform.position.y,
            drone.transform.position.z);
        Vector3 localdroneendPos = new Vector3(drone.transform.position.x,
               drone.transform.position.y - h,
               drone.transform.position.z);

        //Debug.Log("localdronestartPos " + localdronestartPos);
        //Debug.Log("localdroneendPos " + localdroneendPos);

        //currentlerpTime += Time.deltaTime;
        //if (currentlerpTime >= lerpTime)
        //{
        //    currentlerpTime = lerpTime;
        //}
        //float perc = currentlerpTime / lerpTime;

        //drone.transform.position = Vector3.Lerp(localdronestartPos, localdroneendPos,perc);
        float step = speed * Time.deltaTime;
        drone.transform.position = Vector3.MoveTowards(localdronestartPos, localdroneendPos, step);
        //Debug.Log("localdronestartPos " + localdronestartPos);
        //Debug.Log("localdroneendPo " + localdroneendPos);

        if ( GetComponent<droneControlV2>().finishTour == true && drone.transform.position.y==0)
        {
            //gameObject.GetComponent<droneControlV1>().enabled = true;
            
            GetComponent<droneControlV2>().enabled = false;
            GetComponent<droneControlV2>().finishTour = false;

            GetComponent<droneControlV1>().enabled = true;
            GetComponent<droneControlV1>().currentlerpTime = 0;
            

            GetComponent<droneControlV3>().enabled = false;



            


        }
        

     


    }

}
