using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAgent : MonoBehaviour

{
    public GameObject nagent;
    public GameObject[] goalObject;

    public int minTime = 5;
    public int maxTime = 10;
    public int startTime = 5;

    // Start is called before the first frame update
    void Start()
        

    {
        if (nagent == null)
        {
            Destroy(this);
        }
        else
        {
            Invoke("SpawnAgent",startTime);
            Debug.Log("Startclone");
        }
        
    }

    // Update is called once per frame
    void SpawnAgent()
        
    {
        if (nagent == null)
        {
            Destroy(this);
        }
        else
        {
            GameObject na = (GameObject)Instantiate(nagent, this.transform.position, Quaternion.identity);
            Debug.Log(nagent + " Cloned");
            na.GetComponent<trashAgent>().targets = goalObject;
            Invoke("SpawnAgent", Random.Range(minTime, maxTime));
        }
    }
            
        
    
}
