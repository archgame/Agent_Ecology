using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public float transformTime = 2.0f;
    public GameObject target = null;
    private float movespeed;
    private float rotspeed;
        // Start is called before the first frame update
    void Start()
    {
        movespeed = Vector3.Distance(this.transform.position, target.transform.position) / transformTime;
        rotspeed = Quaternion.Angle(this.transform.rotation, target.transform.rotation) / transformTime;
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, movespeed * Time.deltaTime);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, target.transform.rotation, rotspeed * Time.deltaTime);
        
    }
}
