using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random3 : MonoBehaviour
{
    public GameObject child3;
    GameObject obj = null;
    public int GenerateNumber;
    int i = 0;
    public int hour;
    GameObject[] child;
    public int[] arr;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GenerateNumber - 1; i++)
        {
            GameObject obj = (GameObject)Instantiate(child3);
            int x = Random.Range(-460, -450);
            int y = Random.Range(-460, -450);
            //  Debug.Log(x);
            obj.transform.position = new Vector3(x, 2, y);
        }

        for (int i = 0; i < arr.Length;  i++)
        {
            int max = hour * 500000;
            int rnd = Random.Range(0, max);
            arr[i] = rnd;
            // Debug.Log("arr:" + arr[i]);
        }
        
        //Debug.Log("sum: " + sum);
        child = GameObject.FindGameObjectsWithTag("childchild");
    }

    // Update is called once per frame
    void Update()
    {
        //int n = arr[2];
        for (int i = 0; i < arr.Length; i++)
        {
            int n;
            n = arr[i];
            if (Time.frameCount == n)
            {
                child[i].GetComponent<move>().enabled = true;
               
            }
        }
       

    }
}
