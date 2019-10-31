using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardV2 : MonoBehaviour
{
    public bool[] L1targetIsOccupied;
    public bool[] L2targetIsOccupied;
    public int occupiedNum;
    public int exitNum = 0;
    /*
    public int countExit = 0;
    */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        occupiedNum = CalculateValues(true);
    }

 
    int CalculateValues(bool val)
    {
        int count = 0;

        for (int i = 0; i < L2targetIsOccupied.Length; i++)
        {
            if (L2targetIsOccupied[i] == val)
                count++;
        }
        return count;
    }

}
