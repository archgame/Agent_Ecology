using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopyAndKill : MonoBehaviour
{

    public int enterNum;
   // public int exitNum;
    public GameObject copy;
    public GameObject copyOrigin;
   // public GameObject killEnd;
    public float timer = 1;
    float pause;
    int num = 0;

    //GameObject[] allPed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        oneHour -= Time.deltaTime;
        if (oneHour <= 0.0f && oneHour != -1)
        {
            leavingTime();
            oneHour = -1;
        }
        */

        if (num < enterNum)
        {
            pause -= Time.deltaTime;
            if (pause <= 0)
            {
                Instantiate(copy, copyOrigin.transform.position, Quaternion.identity);
                num++;
                pause = timer;
            }

        }
        else if (num > enterNum)
        {

        }

    }


}
