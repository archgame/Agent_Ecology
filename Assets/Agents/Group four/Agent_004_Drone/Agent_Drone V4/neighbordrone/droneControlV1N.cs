using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneControlV1N : MonoBehaviour
{
    public float speed = 10.0F;
    public GameObject drone;
    private Vector3 startPos;
    private Vector3 endPos;
    public float distance = 30f;
    public float lerpTime = 5;
    public float currentlerpTime = 0;
    public bool keyHit = false;
    
    public bool getheight = false;

    private Vector3 origin;

    //private bool isHeigh;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        

        startPos = drone.transform.position;
        endPos = drone.transform.position + Vector3.up * distance;
        //Debug.Log(drone.transform.position);

        if (GetComponent<droneControlV2N>().finishTour == false)
        {
            keyHit = true;
        }

        if (keyHit == true)
        {          


            currentlerpTime += Time.deltaTime;

            //Debug.Log("currentlerpTime" + " " + currentlerpTime);
            //Debug.Log("Time.deltaTime" + " " + Time.deltaTime);

            if (currentlerpTime >= lerpTime)
            {
                currentlerpTime = lerpTime;
            }
            float perc = currentlerpTime / lerpTime / 100;
            //Debug.Log("perc" + perc);

            drone.transform.position = Vector3.Lerp(startPos, endPos, perc);
            //Debug.Log(drone.transform.position);

            //Debug.Log("currentlerpTime2" + " " + currentlerpTime);
            //Debug.Log("Time.deltaTime2" + " " + Time.deltaTime);

            //foreach (Transform child in transform)
            //{
            //    Collider col = child.GetComponent<Collider>();
            //    col.enabled = true;
                
            //}
        
            if (drone.transform.position.y > distance / 2)
            {
                //Collider col = child.GetComponent<Collider>();
                //col.enabled = true;
                GetComponent<Collider>().isTrigger = false;
            }
        }

        if (drone.transform.position.y >= distance && keyHit == true)
        {
            
            getheight = true;
            gameObject.GetComponent<droneControlV2N>().enabled = true;

            
        }
        //Debug.Log("height" + drone.transform.position.y);
   
    }

    

}
