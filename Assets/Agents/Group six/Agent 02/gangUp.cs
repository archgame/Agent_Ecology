using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gangUp : MonoBehaviour
{
    #region GLOBAL VARIABLES

    public bool notFirst = false;
    public float wheelNumber;
    public float wheelerNumber;
    public bool strike;
    public GameObject[] wheelers;
    public GameObject alphamade;




    #endregion
    // Start is called before the first frame update
    void Start()
    {

        wheelNumber = 0;
        strike = false;
        wheelers = GameObject.FindGameObjectsWithTag("OneWheel");
        foreach (GameObject wheeler in wheelers)
        {
            wheelerNumber++;
        }


    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (wheelNumber == wheelerNumber)
        {
            strike = true;
            Debug.Log("STRIKE");
            alphamade.GetComponent<GoForRoller>().GO = enabled;
        }
        */

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "OneWheel")
        {
            Debug.Log("first one arrived");
            //if (collision.gameObject.GetComponent<one>().notFirst == false)
            if (notFirst == false)
            {
                Debug.Log("alpha was made");
                collision.gameObject.GetComponent<GoForRoller>().alpha = true;
                notFirst = true;
                alphamade = collision.gameObject;

            }
            else
            {
                collision.gameObject.GetComponent<GoForRoller>().GO = true;
            }

            wheelNumber++;
            Debug.Log("num of one wheel " + wheelerNumber);

        }

        if (strike == true)
        {
            if(collision.gameObject.GetComponent<GoForRoller>() != null)
                collision.gameObject.GetComponent<GoForRoller>().GO = true;
        }

    }

    void OnTriggerStay(Collider collision)
    {
        if (wheelNumber == wheelerNumber)
        {
            strike = true;
            Debug.Log("STRIKE");
            alphamade.GetComponent<GoForRoller>().GO = enabled;
        }
    }
    void OnTriggerExit(Collider collision)
    {

    }
}
