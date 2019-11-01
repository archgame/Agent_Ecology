using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSpawner_lightrail : MonoBehaviour
{

    public bool stopSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //transform.parent.GetComponent<Spawner>().stop = true;
        stopSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //print("Collision");
        //transform.parent.GetComponent<Spawner>().stop = false;
        stopSpawn = false;
    }

    void OnTriggerExit(Collider other)
    {
        //transform.parent.GetComponent<Spawner>().stop = true;
        stopSpawn = true;
    }
}
