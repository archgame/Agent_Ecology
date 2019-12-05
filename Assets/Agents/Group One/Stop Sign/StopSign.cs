using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StopSign : MonoBehaviour
{
    public GameObject[] arriveSign;
    //public List<GameObject> arriveSign = new List<GameObject>();
    bool waiting = false;
    public int waitTime = 3;
    float waited = 0;
    int pedCrossing = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pedCrossing == 0)
        {
            if (arriveSign.Length != 0 && arriveSign[0] == null)
            {
                for (int i = 0; i < (arriveSign.Length - 1); i++)
                {
                    arriveSign[i] = arriveSign[i + 1];
                    arriveSign[i + 1] = null;
                }
            }

            if (arriveSign[0] != null)
            {

                if (waited > waitTime)
                {
                    Debug.Log("go");
                    arriveSign[0].GetComponent<NavMeshAgent>().isStopped = false;
                    for (int i = 0; i < (arriveSign.Length - 1); i++)
                    {
                        arriveSign[i] = arriveSign[i + 1];
                        arriveSign[i + 1] = null;
                    }
                    waited = 0;
                }
                else
                {
                    waited += Time.deltaTime;
                    Debug.Log("count");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 9)
        {
            pedCrossing ++;
        }
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
        {
            //arriveSign.Add(collision.gameObject);
            for (int i = 0; i < arriveSign.Length; i++)
            {
                if(arriveSign[i] == null)
                {

                    arriveSign[i] = collision.gameObject;
                    break;
                }

            }
            collision.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 9)
        {
            pedCrossing --;
        }
    }
}
