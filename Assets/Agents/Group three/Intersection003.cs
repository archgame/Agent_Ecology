using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection003 : MonoBehaviour
{
    [Header("Controls")]
    public bool switchLights = false;
    public float switchTime = 10;

    [Header("Lights")]
    public GameObject[] trafficLights;
    public GameObject[] crosswalks;
    float waited = 0;


    // Update is called once per frame
    void Update()
    {
        switchLights = Switch(switchLights, switchTime);
        //Debug.Log(switchLights);
        if(switchLights)
        {
            SwitchLights(true,false,true,false);
        }
        else
        {
            SwitchLights(false,true,false,true);
        }
    }

    public void SwitchLights(bool l1, bool l2, bool l3, bool l4)
    {
        trafficLights[0].SetActive(l1);
        trafficLights[1].SetActive(l2);
        trafficLights[2].SetActive(l3);
        trafficLights[3].SetActive(l4);
        trafficLights[4].SetActive(!l1);
        trafficLights[5].SetActive(!l2);
        trafficLights[6].SetActive(!l3);
        trafficLights[7].SetActive(!l4);

        crosswalks[0].SetActive(!l1);
        crosswalks[1].SetActive(!l2);
        crosswalks[2].SetActive(!l3);
        crosswalks[3].SetActive(!l4);
    }

    public bool Switch(bool ifswitchLights, float time)
    {
        //float waited = 0;
        float waitTime = time;
        if (waited > waitTime)
        {
            waited = 0;
            ifswitchLights = !ifswitchLights;
        }
        else
        {
            waited += Time.deltaTime * 3;
            //Debug.Log(waited);
            //Debug.Log("if " + ifswitchLights);

        }
        return ifswitchLights;
    }
}
