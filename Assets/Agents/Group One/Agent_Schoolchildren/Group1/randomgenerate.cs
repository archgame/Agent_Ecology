using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class randomgenerate : MonoBehaviour
{
    public GameObject children;
    GameObject obj = null;
    int[] arr = new int[6];
    int i = 0;
    public int hour;






    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            int max = hour * 5000;
            int rnd = Random.Range(0, max);
            arr[i] = rnd;
            Debug.Log("arr:" + arr[i]);
        }
        //Debug.Log("sum: " + sum);




    }

    // Update is called once per frame
    void Update()
    {
        //int n = arr[2];
        for (int i = 0; i < 6; i++)
        {
            int n;
            n = arr[i];
            if (Time.frameCount == n)
            {
                GameObject obj = (GameObject)Instantiate(children);
                int x1 = -64;
                int y1 = -132;
              

                children.transform.position = new Vector3(x1, 0, y1);
              

            }
        }






    }



}
