using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonBounce : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * thrust);
    }
}
