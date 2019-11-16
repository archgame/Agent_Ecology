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
    private bool waiting = false;
    private float waited = 0;

    private bool l1;
    private bool l2;
    private bool l3;
    private bool l4;




    // Update is called once per frame
    private void Start()
    {
        trafficLights[0].SetActive(l1);
    }

    void Update()
    {



        /*if (switchLights)
        {
            SwitchLights(true, false, true, false);
        }
        else
        {
            SwitchLights(false,true,false,true);
        }
        */








        if (waiting) // (waiting == false) (1 == 0)
        {
            if (waited > waitTime)
            {
                waiting = false;
                waited = 0;                
            }

            else
            {
                waited += Time.deltaTime;
            }

        } //if waiting
        else
        {
            waiting = true;            

        }
    }

    public void SwitchLights(bool l1, bool l2, bool l3, bool l4)
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
