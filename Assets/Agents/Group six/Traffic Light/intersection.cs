using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intersection : MonoBehaviour
{
    [Header("Controls")]

    public bool switchLights = true;
    public float switchTime1 = 2;
    public float switchTime2 = 4;
    private float waited = 0;

    [Header("Lights")]
    public GameObject[] trafficLights;
    public GameObject[] greenTrafficLight;
    public GameObject[] croswalsk;
    public GameObject[] goCroswalsk;

    [Header("Center")]

    public GameObject CenterA;
    public GameObject CenterB;

    // public GameObject CenterA;
    // public GameObject CenterB;
    // public GameObject CenterC;
    // public GameObject CenterD;

    [Header("cent Dist")]

    public float Distance;
   


    // Start is called before the first frame update
    void Start()
    {
        Distance = Vector3.Distance(CenterA.transform.position, CenterB.transform.position);
        Debug.Log("DistAB= " + Distance);
        /*
        Distance = Vector3.Distance(CenterA.transform.position, CenterB.transform.position);
        Debug.Log("distAB" + Distance);
        Debug.DrawLine(CenterA.transform.position, CenterB.transform.position, Color.red);
        Distance = Vector3.Distance(CenterA.transform.position, CenterC.transform.position);
        Debug.Log("distAC" + Distance);
        Debug.DrawLine(CenterA.transform.position, CenterC.transform.position, Color.blue);
        Distance = Vector3.Distance(CenterA.transform.position, CenterD.transform.position);
        Debug.Log("distAD" + Distance);
        Debug.DrawLine(CenterA.transform.position, CenterD.transform.position, Color.blue);
        Distance = Vector3.Distance(CenterB.transform.position, CenterC.transform.position);
        Debug.Log("distBC" + Distance);
        Debug.DrawLine(CenterB.transform.position, CenterC.transform.position, Color.blue);
        Distance = Vector3.Distance(CenterB.transform.position, CenterD.transform.position);
        Debug.Log("distBD" + Distance);
        Debug.DrawLine(CenterB.transform.position, CenterD.transform.position, Color.blue);
        Distance = Vector3.Distance(CenterC.transform.position, CenterD.transform.position);
        Debug.Log("distCD" + Distance);
        Debug.DrawLine(CenterC.transform.position, CenterD.transform.position, Color.blue);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (Distance > 100)
        {
            if (switchLights) // turn booling lights true
            {
                SwitchLights(true, false, true, false);
                if (waited > switchTime2)
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
            else    // turn booling lights false
            {

                SwitchLights(false, true, false, true);
                if (waited > switchTime2)
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
        if (Distance < 100)
            {
                if (switchLights) // turn booling lights true
                {
                    SwitchLights(true, false, true, false);
                    if (waited > switchTime1)
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
                else    // turn booling lights false
                {

                    SwitchLights(false, true, false, true);
                    if (waited > switchTime1)
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
    }

    public void SwitchLights(bool l1, bool l2, bool l3, bool l4)
    {
        trafficLights[0].SetActive(l1);
        trafficLights[1].SetActive(l2);
        trafficLights[2].SetActive(l3);
        trafficLights[3].SetActive(l4);

        // green lights
        greenTrafficLight[0].SetActive(!l1);
        greenTrafficLight[1].SetActive(!l2);
        greenTrafficLight[2].SetActive(!l3);
        greenTrafficLight[3].SetActive(!l4);

        croswalsk[0].SetActive(!l1);
        croswalsk[1].SetActive(!l2);
        croswalsk[2].SetActive(!l3);
        croswalsk[3].SetActive(!l4);

        goCroswalsk[0].SetActive(l1);
        goCroswalsk[1].SetActive(l2);
        goCroswalsk[2].SetActive(l3);
        goCroswalsk[3].SetActive(l4);

    }
}
