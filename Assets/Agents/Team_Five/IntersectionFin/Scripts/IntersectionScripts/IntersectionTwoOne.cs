﻿using System.Collections;
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
    public float yellowTime = 2;

    private bool l1 = true;
    private bool l2 = true;
    private bool l3 = true;
    private bool l4 = true;
    public bool TramBitch = false;

    [Header("Ped Colliders")]
    public GameObject PedColliderRed1;
    public GameObject PedColliderRed2;
    public GameObject PedColliderRed3;
    public GameObject PedColliderRed4;
    public GameObject PedColliderGreen;
    public GameObject YellowLight;
    



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

        PedColliderRed1.GetComponent<Collider>().enabled = true;
        PedColliderRed2.GetComponent<Collider>().enabled = true;
        PedColliderRed3.GetComponent<Collider>().enabled = true;
        PedColliderRed4.GetComponent<Collider>().enabled = true;
        PedColliderGreen.GetComponent<Collider>().enabled = false;
       

    }

    private void Update()
    {
        if (TramBitch == false)
        {
            waited += Time.deltaTime;
            /*
            crosswalks[0].SetActive(l1);
            crosswalks[1].SetActive(l2);
            crosswalks[2].SetActive(l3);
            crosswalks[3].SetActive(l4);
            */

            PedColliderRed1.GetComponent<Collider>().enabled = true;
            PedColliderRed2.GetComponent<Collider>().enabled = true;
            PedColliderRed3.GetComponent<Collider>().enabled = true;
            PedColliderRed4.GetComponent<Collider>().enabled = true;
            PedColliderGreen.GetComponent<Collider>().enabled = false;

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

                    PedColliderRed1.GetComponent<Collider>().enabled = true;
                    PedColliderRed2.GetComponent<Collider>().enabled = true;
                    PedColliderRed3.GetComponent<Collider>().enabled = true;
                    PedColliderRed4.GetComponent<Collider>().enabled = true;
                    PedColliderGreen.GetComponent<Collider>().enabled = false;
                    
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

                    PedColliderRed1.GetComponent<Collider>().enabled = true;
                    PedColliderRed2.GetComponent<Collider>().enabled = true;
                    PedColliderRed3.GetComponent<Collider>().enabled = true;
                    PedColliderRed4.GetComponent<Collider>().enabled = true;
                    PedColliderGreen.GetComponent<Collider>().enabled = false;
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

                    /*
                    crosswalks[0].SetActive(!l1);
                    crosswalks[1].SetActive(!l2);
                    crosswalks[2].SetActive(!l3);
                    crosswalks[3].SetActive(!l4);
                    */

                    PedColliderRed1.GetComponent<Collider>().enabled = false;
                    PedColliderRed2.GetComponent<Collider>().enabled = false;
                    PedColliderRed3.GetComponent<Collider>().enabled = false;
                    PedColliderRed4.GetComponent<Collider>().enabled = false;
                    PedColliderGreen.GetComponent<Collider>().enabled = true;



                    // waited += Time.deltaTime;
                }

                if (waited > (waitTime*5 / 2))
                {
                    PedColliderRed1.GetComponent<Collider>().enabled = true;
                    PedColliderRed2.GetComponent<Collider>().enabled = true;
                    PedColliderRed3.GetComponent<Collider>().enabled = true;
                    PedColliderRed4.GetComponent<Collider>().enabled = true;
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

                    /*
                    crosswalks[0].SetActive(l1);
                    crosswalks[1].SetActive(l2);
                    crosswalks[2].SetActive(l3);
                    crosswalks[3].SetActive(l4);
                    */

                    PedColliderRed1.GetComponent<Collider>().enabled = true;
                    PedColliderRed2.GetComponent<Collider>().enabled = true;
                    PedColliderRed3.GetComponent<Collider>().enabled = true;
                    PedColliderRed4.GetComponent<Collider>().enabled = true;
                    PedColliderGreen.GetComponent<Collider>().enabled = false;

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

                    PedColliderRed1.GetComponent<Collider>().enabled = true;
                    PedColliderRed2.GetComponent<Collider>().enabled = true;
                    PedColliderRed3.GetComponent<Collider>().enabled = true;
                    PedColliderRed4.GetComponent<Collider>().enabled = true;
                    PedColliderGreen.GetComponent<Collider>().enabled = false;
                }

             
                if (waited > (waitTime * 5))
                {
                    RedLights[0].SetActive(l1);
                    RedLights[1].SetActive(l2);
                    RedLights[2].SetActive(l3);
                    RedLights[3].SetActive(l4);

                    GreenLights[0].SetActive(!l1);
                    GreenLights[1].SetActive(!l2);
                    GreenLights[2].SetActive(!l3);
                    GreenLights[3].SetActive(!l4);

                    /*
                    crosswalks[0].SetActive(!l1);
                    crosswalks[1].SetActive(!l2);
                    crosswalks[2].SetActive(!l3);
                    crosswalks[3].SetActive(!l4);
                    */

                    PedColliderRed1.GetComponent<Collider>().enabled = false;
                    PedColliderRed2.GetComponent<Collider>().enabled = false;
                    PedColliderRed3.GetComponent<Collider>().enabled = false;
                    PedColliderRed4.GetComponent<Collider>().enabled = false;
                    PedColliderGreen.GetComponent<Collider>().enabled = true;
                }

                if (waited > (waitTime * 9 / 2))
                {
                    PedColliderRed1.GetComponent<Collider>().enabled = true;
                    PedColliderRed2.GetComponent<Collider>().enabled = true;
                    PedColliderRed3.GetComponent<Collider>().enabled = true;
                    PedColliderRed4.GetComponent<Collider>().enabled = true;
                }
            }

            else
            {
                waited = 0;

            }
        }

        if (TramBitch==true)
        {
            RedLights[0].SetActive(l1);
            RedLights[1].SetActive(l2);
            RedLights[2].SetActive(l3);
            RedLights[3].SetActive(l4);

            GreenLights[0].SetActive(!l1);
            GreenLights[1].SetActive(!l2);
            GreenLights[2].SetActive(!l3);
            GreenLights[3].SetActive(!l4);

            /*crosswalks[0].SetActive(l1);
            crosswalks[1].SetActive(l2);
            crosswalks[2].SetActive(l3);
            crosswalks[3].SetActive(l4);
            */

            PedColliderRed1.GetComponent<Collider>().enabled = true;
            PedColliderRed2.GetComponent<Collider>().enabled = true;
            PedColliderRed3.GetComponent<Collider>().enabled = true;
            PedColliderRed4.GetComponent<Collider>().enabled = true;
            PedColliderGreen.GetComponent<Collider>().enabled = false;
                

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Tram"))
        {
            TramBitch = true;
            Debug.Log("collided finally");           

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Tram"))
        {
            TramBitch = false;
            Debug.Log("TramBitches");

        }
    }

}
