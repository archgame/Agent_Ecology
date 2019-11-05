using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    [Header ("Celestial Objects")]
    public GameObject sun;
    public GameObject moon;

    [Header("Time")]
    public float time;
    public TimeSpan currentTime;
    public Text text;
    public int days;
    public float speed;

    [Header("Lighting")]
    private float sunintensity;
    private float moonintensity;
    public float rotation;

    // Update is called once per frame
    void Update()
    {
        ChangeTime();
    }

    private void ChangeTime()
    {
        //update time
        time += Time.deltaTime * speed;
        if(time > 86400) //day in seconds
        {
            days += 1;
            if(days > 7) //keep it as weekdays
            {
                days = 0;
            }
            time = 0;
        }

        //print time
        currentTime = TimeSpan.FromSeconds(time);
        string[] temptime = currentTime.ToString().Split(":"[0]);
        text.text = temptime[0] + ":" + temptime[1];

        //celestial body rotations
        rotation = (time - 64800) / 86400 * -360; //21600
        sun.transform.rotation = Quaternion.Euler(new Vector3(rotation, 90, 0));
        moon.transform.rotation = Quaternion.Euler(new Vector3(-rotation, -90, 0));

        //lighting
        if (time > 21600 && time < 64800) //daytime
        {
            sunintensity = 1 - (Math.Abs(43200 - time) / 21600); //21600 is dawn, 64800 is dusk, 43200 is noon
            moonintensity = 0;
        }           
        else //night time
        {
            sunintensity = 0;
            if(time < 21600)
            {
                moonintensity = 1 - (time / 21600);
            }
            else
            {
                moonintensity = 1 - (Math.Abs(86400 - time) / 21600);
            }          
        }            
        
        sun.GetComponent<Light>().intensity = sunintensity;
        moon.GetComponent<Light>().intensity = moonintensity;
    }
}
