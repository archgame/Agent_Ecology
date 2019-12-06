using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusControl : MonoBehaviour
{
    public GameObject MIDbus;
    public GameObject ReMIDbus;
    public GameObject Circlebus;
    bool active;


    // Update is called once per frame
    void Update()
    {
        active = Switch(active, 80);
        if (!MIDbus.activeInHierarchy&& !ReMIDbus.activeInHierarchy)
        {
            MIDbus.SetActive(active);
            ReMIDbus.SetActive(active);
        }
        if (!Circlebus.activeInHierarchy)
        {
            Circlebus.SetActive(active);
        }

    }


    float waited = 0;
    public bool Switch(bool ifswitchLights, float time)
    {
        //float waited = 0;
        float waitTime = time;
        if (waited > waitTime)
        {
            waited = 0;
            ifswitchLights = true;
        }
        else
        {
            waited += Time.deltaTime * 3f;
            //Debug.Log(waited);

        }
        return ifswitchLights;
    }
}
