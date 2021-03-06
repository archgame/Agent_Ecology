﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class IntersectionControl : MonoBehaviour
{
    DayNightCycle timeScript;

    public GameObject[] Intersectionlist;
    Intersection[] intersectionScriptList;
    public float previous;
    public float stoptime;
    private float now;
    // Start is called before the first frame update
    void Start()
    {
        /*Intersectionlist = GameObject.FindGameObjectsWithTag("Intersection");*/
        List<Intersection> a = new List<Intersection>();
        for (int i = 0; i < Intersectionlist.Length; i++)
        {
            a.Add(Intersectionlist[i].GetComponent<Intersection>());
        }
        intersectionScriptList = a.ToArray();
        

        timeScript = Camera.main.GetComponent<DayNightCycle>();
        previous = 0;
        
      
    }

    // Update is called once per frame
    void Update()
    {
      
        now += Time.deltaTime * 3;
        float D = Mathf.Abs(now - previous);
        if (D > stoptime)
        {
            previous = now;
            foreach (Intersection b in intersectionScriptList)
            {
                b.switchLights = !b.switchLights;
            }
        }
    }
}
