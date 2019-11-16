using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection003 : MonoBehaviour
{
    [Header("Controls")]
    public bool switchLights = false;
    public float switchtime=0;

    [Header("Lights")]
    public GameObject[] NS_Obstacle;
    public GameObject[] EW_Obstacle;
    int j = 0;
    int k = 0;
    float waited = 0;

    // Update is called once per frame


    void Update()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject temp = gameObject.transform.GetChild(i).gameObject;
            if (temp.name.Contains("NS"))
            {
                NS_Obstacle[j] = temp;
                if(j<NS_Obstacle.Length-1)
                {
                    j++;
                }
            }
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GameObject temp = gameObject.transform.GetChild(i).gameObject;
            if (temp.name.Contains("EW"))
            {
                EW_Obstacle[k] = temp;
                if (k < NS_Obstacle.Length-1)
                {
                    k++;
                }
            }
        }
        //bool waiting = false;
        if (switchLights)
        {
            SwitchLights(true, false);
            if (waited > switchtime)
            {
                waited = 0;
                switchLights = !switchLights;
            }
            else
            {
                waited += Time.deltaTime;
                Debug.Log(waited);
            }
        }
        else
        {
            SwitchLights(false,true);
            if (waited > switchtime)
            {
                waited = 0;
                switchLights = !switchLights;
            }
            else
            {
                waited += Time.deltaTime;
                Debug.Log(waited);
            }
        }

    }

    public void SwitchLights(bool l1, bool l2)
    {
        foreach(GameObject NS in NS_Obstacle)
        {
            NS.SetActive(l1);
        }
        foreach (GameObject EW in EW_Obstacle)
        {
            EW.SetActive(l2);
        }

        //Debug.Log(Time.deltaTime*100f);
    }

    /*public void Switch(bool ifswitchLights, float time)
    {
        float waitTime = time;
        //bool waiting = false;
        if (waited > waitTime)
        {
            waited = 0;
            ifswitchLights = !ifswitchLights;
        }
        else
        {
            waited += Time.deltaTime*3;
            Debug.Log(waited);
        }

    }*/
}
