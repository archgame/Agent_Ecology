using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random2 : MonoBehaviour
{
    public GameObject child;
    GameObject obj = null;
    int[] arr = new int[12];
    int i = 0;
    public int hour;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            int max = hour * 5000;
            int rnd = Random.Range(0, max);
            arr[i] = rnd;
            // Debug.Log("arr:" + arr[i]);
        }
        //Debug.Log("sum: " + sum);

    }

    // Update is called once per frame
    void Update()
    {
        //int n = arr[2];
        for (int i = 0; i < 12; i++)
        {
            int n;
            n = arr[i];
            if (Time.frameCount == n)
            {
                GameObject obj = (GameObject)Instantiate(child);
                int x = Random.Range(-237, -220);
                int y = Random.Range(-238, -226);
                //  Debug.Log(x);
                child.transform.position = new Vector3(x, 2, y);
                child.GetComponent<move>().enabled = true;

            }
        }

    }
}
