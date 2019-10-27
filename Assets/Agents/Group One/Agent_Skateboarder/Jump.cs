using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    float time = 4f;
    float countTime = 0;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countTime > time)
        {
            //na.enabled = false;
            rb.isKinematic = false;
            rb.useGravity = true;

            rb.AddForce(new Vector3(0, 0.02f, 0), ForceMode.Impulse);
            countTime = 0;
            //rb.AddRelativeForce(new Vector3(0, 5, 5), ForceMode.Impulse);

        }
        else
        {
            countTime += Time.deltaTime;
        }
        Debug.Log(countTime);
    }
}
