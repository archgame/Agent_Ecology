using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : MonoBehaviour
{
    [Header("Controls")]
    public bool switchLights = false;

    [Header("Lights")]
    public GameObject[] TrafficLights;
    public GameObject[] Crosswalks;



    // Update is called once per frame
    void Update()
    {

        if (switchLights)
        {
            SwitchLights(true, false, true, false);
        }
        else
        {
            SwitchLights(false, true, false, true);
        }
        
    }

    public void SwitchLights(bool L1, bool L2, bool L3, bool L4)
    {
        TrafficLights[0].SetActive(L1);
        TrafficLights[1].SetActive(L2);
        TrafficLights[2].SetActive(L3);
        TrafficLights[3].SetActive(L4);

        Crosswalks[0].SetActive(!L1);
        Crosswalks[1].SetActive(!L2);
        Crosswalks[2].SetActive(!L3);
        Crosswalks[3].SetActive(!L4);
        Crosswalks[4].SetActive(!L1);
        Crosswalks[5].SetActive(!L2);
        Crosswalks[6].SetActive(!L3);
        Crosswalks[7].SetActive(!L4);
    }
}
