using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    [Header("Controls")]
    public bool switchLights = false;

    [Header("Lights")]
    public GameObject[] trafficLights;
    public GameObject[] crosswalks;

    [Header("Wait")]
    public float waitTime = 0;
    private bool waiting = true;
    private float waited = 0;

    private bool l1 = true;
    private bool l2 = true;
    private bool l3 = true;
    private bool l4 = true;
      
        

    // Update is called once per frame
    private void Start()
    {

        waited += Time.deltaTime;
        trafficLights[0].SetActive(!l1);
        trafficLights[1].SetActive(l2);
        trafficLights[2].SetActive(l3);
        trafficLights[3].SetActive(l4);

    }

    private void Update()
    {
        waited += Time.deltaTime;
        crosswalks[0].SetActive(l1);
        crosswalks[1].SetActive(l2);
        crosswalks[2].SetActive(l3);
        crosswalks[3].SetActive(l4);

        if (waited < (waitTime * 5))
        {
            if (waited > 0)
            {
                trafficLights[0].SetActive(!l1);
                trafficLights[1].SetActive(l2);
                trafficLights[2].SetActive(l3);
                trafficLights[3].SetActive(l4);
                //waited += Time.deltaTime;
            }

            if (waited > waitTime)
            {
                trafficLights[0].SetActive(l1);
                trafficLights[1].SetActive(!l2);
                trafficLights[2].SetActive(l3);
                trafficLights[3].SetActive(l4);
                //waited += Time.deltaTime;
            }

            if (waited > (waitTime * 2))
            {
                trafficLights[0].SetActive(l1);
                trafficLights[1].SetActive(l2);
                trafficLights[2].SetActive(!l3);
                trafficLights[3].SetActive(l4);
                // waited += Time.deltaTime;
            }

            if (waited > (waitTime * 3))
            {
                trafficLights[0].SetActive(l1);
                trafficLights[1].SetActive(l2);
                trafficLights[2].SetActive(l3);
                trafficLights[3].SetActive(!l4);
                // waited += Time.deltaTime;
            }

            if (waited > (waitTime * 4))
            {
                trafficLights[0].SetActive(l1);
                trafficLights[1].SetActive(l2);
                trafficLights[2].SetActive(l3);
                trafficLights[3].SetActive(l4);
                crosswalks[0].SetActive(!l1);
                crosswalks[1].SetActive(!l2);
                crosswalks[2].SetActive(!l3);
                crosswalks[3].SetActive(!l4);
            }
        }

        else
        {
            waited = 0;

        }



    }

}










