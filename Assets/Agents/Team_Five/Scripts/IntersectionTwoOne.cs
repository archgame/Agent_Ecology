using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntersectionTwoOne : MonoBehaviour
{
    [Header("Controls")]
    public bool switchLights = false;

    [Header("Lights")]
    public GameObject[] RedLights;
    public GameObject[] GreenLights;
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

        RedLights[0].SetActive(!l1);
        RedLights[1].SetActive(l2);
        RedLights[2].SetActive(l3);
        RedLights[3].SetActive(l4);

        GreenLights[0].SetActive(l1);
        GreenLights[1].SetActive(!l2);
        GreenLights[2].SetActive(!l3);
        GreenLights[3].SetActive(!l4);
    }

    private void Update()
    {
        waited += Time.deltaTime;
        crosswalks[0].SetActive(l1);
        crosswalks[1].SetActive(l2);
        crosswalks[2].SetActive(l3);
        crosswalks[3].SetActive(l4);

        if (waited < (waitTime * 6))
        {
            if (waited > 0)
            {
                RedLights[0].SetActive(!l1);
                RedLights[1].SetActive(l2);
                RedLights[2].SetActive(l3);
                RedLights[3].SetActive(l4);

                GreenLights[0].SetActive(l1);
                GreenLights[1].SetActive(!l2);
                GreenLights[2].SetActive(!l3);
                GreenLights[3].SetActive(!l4);


            }

            if (waited > waitTime)
            {
                RedLights[0].SetActive(l1);
                RedLights[1].SetActive(!l2);
                RedLights[2].SetActive(l3);
                RedLights[3].SetActive(l4);

                GreenLights[0].SetActive(!l1);
                GreenLights[1].SetActive(l2);
                GreenLights[2].SetActive(!l3);
                GreenLights[3].SetActive(!l4);
            }

            if (waited > (waitTime * 2))
            {
                RedLights[0].SetActive(l1);
                RedLights[1].SetActive(l2);
                RedLights[2].SetActive(l3);
                RedLights[3].SetActive(l4);

                GreenLights[0].SetActive(!l1);
                GreenLights[1].SetActive(!l2);
                GreenLights[2].SetActive(!l3);
                GreenLights[3].SetActive(!l4);

                crosswalks[0].SetActive(!l1);
                crosswalks[1].SetActive(!l2);
                crosswalks[2].SetActive(!l3);
                crosswalks[3].SetActive(!l4);

                // waited += Time.deltaTime;
            }

            if (waited > (waitTime * 3))
            {
                RedLights[0].SetActive(l1);
                RedLights[1].SetActive(l2);
                RedLights[2].SetActive(!l3);
                RedLights[3].SetActive(l4);

                GreenLights[0].SetActive(!l1);
                GreenLights[1].SetActive(!l2);
                GreenLights[2].SetActive(l3);
                GreenLights[3].SetActive(!l4);

                crosswalks[0].SetActive(l1);
                crosswalks[1].SetActive(l2);
                crosswalks[2].SetActive(l3);
                crosswalks[3].SetActive(l4);

                // waited += Time.deltaTime;
            }

            if (waited > (waitTime * 4))
            {
                RedLights[0].SetActive(l1);
                RedLights[1].SetActive(l2);
                RedLights[2].SetActive(l3);
                RedLights[3].SetActive(!l4);

                GreenLights[0].SetActive(!l1);
                GreenLights[1].SetActive(!l2);
                GreenLights[2].SetActive(!l3);
                GreenLights[3].SetActive(l4);
            }

            if (waited > (waitTime*5))
            {
                RedLights[0].SetActive(l1);
                RedLights[1].SetActive(l2);
                RedLights[2].SetActive(l3);
                RedLights[3].SetActive(l4);

                GreenLights[0].SetActive(!l1);
                GreenLights[1].SetActive(!l2);
                GreenLights[2].SetActive(!l3);
                GreenLights[3].SetActive(!l4);

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
