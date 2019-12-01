using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class Intersection : MonoBehaviour
{
    [Header("Controls")]
    public bool switchLights1 = false;
    public bool switchLights2 = false;
    public bool switchLights3 = false;
    public bool switchLights4 = false;
    public bool switchLightsPed = false;


    [Header("Lights")]
    public GameObject[] trafficLights;
    public GameObject[] crosswalks;

    [Header("Wait")]
    public float waitTime = 0;
    private bool waiting = false;
    private float waited = 0;

    void Start()
    {
        //InvokeRepeating(Update);
        //StartCoroutine("Update");
    }


    // Update is called once per frame
    void Update()
    {
        while (true)
        {
            if (switchLights1)
            {
                SwitchLights(false, true, true, true);
                switchLightsPed = false;
            }
            else
            {
                SwitchLights(true, false, false, false);
            }

            if (switchLights2)
            {
                switchLights1 = false;
                switchLightsPed = false;
                SwitchLights(true, false, true, true);
            }
            else
            {
                //SwitchLights(false,true,false, false);
            }

            if (switchLights3)
            {

                switchLights1 = false;
                switchLights2 = false;
                switchLightsPed = false;
                SwitchLights(true, true, false, true);
            }
            else
            {
                //SwitchLights(false,false,true,false);
            }

            if (switchLights4)
            {
                switchLights1 = false;
                switchLights2 = false;
                switchLights3 = false;
                //switchLightsPed = false;
                SwitchLights(true, true, true, false);
            }
            else
            {
                //SwitchLights(false,false,false,true);
            }

            if (switchLightsPed)
            {
                switchLights1 = false;
                switchLights2 = false;
                switchLights3 = false;
                switchLights4 = false;
                SwitchLights(true, true, true, true);

                crosswalks[0].SetActive(false);
                crosswalks[1].SetActive(false);
                crosswalks[2].SetActive(false);
                crosswalks[3].SetActive(false);
            }
            else
            {
                //SwitchLights(true,true,true,true);
                crosswalks[0].SetActive(true);
                crosswalks[1].SetActive(true);
                crosswalks[2].SetActive(true);
                crosswalks[3].SetActive(true);
            }
        }
    }

    public void SwitchLights(bool l1, bool l2, bool l3, bool l4)
    {
        trafficLights[0].SetActive(l1);
        trafficLights[1].SetActive(l2);
        trafficLights[2].SetActive(l3);
        trafficLights[3].SetActive(l4);

        /*crosswalks[0].SetActive(!l1);
        crosswalks[1].SetActive(!l2);
        crosswalks[2].SetActive(!l3);
        crosswalks[3].SetActive(!l4);*/
    }

    
}
