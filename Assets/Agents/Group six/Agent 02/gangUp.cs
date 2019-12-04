using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gangUp : MonoBehaviour
{
    #region GLOBAL VARIABLES

    public bool notFirst;
    public float wheelNumber;
    public float wheelerNumber;
    public bool strike;
    public GameObject[] wheelers;



    #endregion
    // Start is called before the first frame update
    void Start()
    {
        wheelNumber = 0;
        strike = false;
        wheelers = GameObject.FindGameObjectsWithTag("OneWheel");

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject wheeler in wheelers)
        {
            wheelerNumber++;
        }

        if (wheelNumber == wheelerNumber)
        {
            strike = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        foreach (GameObject wheeler in wheelers)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("OneWheel"))
            {
                if (collision.gameObject.GetComponent<gangUp>().notFirst == false)
                {
                    collision.gameObject.GetComponent<GoForRoller>().alpha = true;
                }
                notFirst = true;
                wheelNumber++;

            }
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (strike == true)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("OneWheel"))
            {
                gameObject.GetComponent<Collider>().enabled = false;
            }

        }
    }
}
