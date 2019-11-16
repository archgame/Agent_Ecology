using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intersection : MonoBehaviour
{
    [Header("Controls")]

    public bool switchLights = true;
    public float switchTime = 4;
    private float waited = 0;

    [Header("Lights")]
    public GameObject[] trafficLights;
    public GameObject[] croswalsk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (switchLights)
        {
            SwitchLights(true, false, true, false);
            if (waited > switchTime)
            {
                switchLights = false;
                waited = 0;
               // Debug.Log("SwitchLights");
            }
            else
            {
                waited += Time.deltaTime;
            }

        }
        else
        {
            SwitchLights(false, true, false, true);
            if (waited > switchTime)
            {
                switchLights = true;
                waited = 0;
                //Debug.Log("Switch");
            }
            else
            {
                waited += Time.deltaTime;
            }

        }
    }

    public void SwitchLights(bool l1, bool l2, bool l3, bool l4)
    {
        trafficLights[0].SetActive(l1);
        trafficLights[1].SetActive(l2);
        trafficLights[2].SetActive(l3);
        trafficLights[3].SetActive(l4);

        croswalsk[0].SetActive(!l1);
        croswalsk[1].SetActive(!l2);
        croswalsk[2].SetActive(!l3);
        croswalsk[3].SetActive(!l4);
      
    }
}
