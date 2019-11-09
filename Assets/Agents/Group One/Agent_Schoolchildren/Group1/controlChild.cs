using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlChild : MonoBehaviour
{
    QueueL1 queueScript;
    move moveScript;

    // Start is called before the first frame update
    void Start()
    {
        queueScript = GetComponent<QueueL1>();
        moveScript = GetComponent<move>();
        queueScript.enabled = false;
        moveScript.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(moveScript.findFood == true && queueScript.finished == false)
        {
            moveScript.enabled = false;
            queueScript.enabled = true;
        }

        if( queueScript.finished == true)
        {
            moveScript.enabled = true;
            queueScript.enabled = false;
        }

    }
}
