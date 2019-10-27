using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate2 : MonoBehaviour
{
    public GameObject Rider;
    public int RiderAmount;
    public GameObject[] RGPs;

    // Start is called before the first frame update
    void Start()
    {
        int a = RiderAmount % RGPs.Length;
        for (int j = 0; j < RiderAmount / RGPs.Length; j++)
        {
            for (int i = 0; i < RGPs.Length; i++)
            {
                GameObject obj = Instantiate(Rider);    //实例化敌人

                obj.transform.position = RGPs[i].transform.position;
            }
        }


        //Debug.Log("sum: " + sum);
    }

    // Update is called once per frame
    void Update()
    {
    }



}
