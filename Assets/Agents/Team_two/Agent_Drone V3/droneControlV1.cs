using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneControlV1 : MonoBehaviour
{
    public float speed = 10.0F;
    public GameObject drone;
    private Vector3 startPos;
    private Vector3 endPos;
    public float distance = 30f;
    public float lerpTime = 5;
    private float currentlerpTime = 0;
    private bool keyHit = false;
    
    public bool getheight = false;

    //private bool isHeigh;

    // Start is called before the first frame update
    void Start()
    {
        

        startPos = drone.transform.position;
        endPos = drone.transform.position + Vector3.up * distance;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            keyHit = true;
        }

        if (keyHit == true)
        {
            currentlerpTime += Time.deltaTime;
            if (currentlerpTime >= lerpTime)
            {
                currentlerpTime = lerpTime;
            }
            float perc = currentlerpTime / lerpTime;
            //Debug.Log("perc" + perc);
            drone.transform.position = Vector3.Lerp(startPos, endPos, perc);

        }

        if (drone.transform.position.y >= distance)
        {
            getheight = true;
        }
        //Debug.Log("height" + drone.transform.position.y);
      

    }

}
