using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jump : MonoBehaviour
{
    float pi;
    public int i = 1;
    double waited1 = 0;
    double waited2 = 0;
    public int frequency = 1;
    double waitTime1;
    double waitTime2;

    // Start is called before the first frame update
    void Start()
    {
        pi = Mathf.PI;
        waitTime2 = pi ;
        waitTime1 = pi ;
    }

    // Update is called once per frame
    void Update()
    {
        if (waited1 > waitTime1)
        {
            float height = Mathf.Sin(( Time.time * 2 - pi / 2 ) * frequency )  + 2;
            gameObject.GetComponent<NavMeshAgent>().baseOffset = height;
            if(waited2 > waitTime2)
            {
                waited1 = 0;
                waited2 = 0;
            }
            else
            {
                waited2 += Time.deltaTime;
            }
        }
        else
        {
            waited1 += Time.deltaTime;
        }
    }
}
