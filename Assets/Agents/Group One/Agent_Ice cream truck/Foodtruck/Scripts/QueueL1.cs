using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueL1 : MonoBehaviour
{
    public GameObject[] targets;
    public GameObject[] targetsL2;
    NavMeshAgent agent;
    GameObject targetPoint;
    int i;
    int j;
    GameObject board;
    Board getBoard;
    //public GameObject site;
    public Vector3 origin;
    float waited = 0;
    float waitedL2 = 0;
    public int waitTimeL1 = 5;
    public int waitTimeL2 = 15;
    public bool finished;
     // Start is called before the first frame update
    void Start()
    {
        board = GameObject.FindGameObjectWithTag("board");
        getBoard = board.GetComponent<Board>();
        if (targets.Length == 0)
        {
            //get all game objects tagged with "Target"
            targets = GameObject.FindGameObjectsWithTag("L1");
            targetsL2 = GameObject.FindGameObjectsWithTag("L2");
            i = targets.Length;
            j = targetsL2.Length;
            //targetPoint = targets[i];

            agent = GetComponent<NavMeshAgent>();
            //site = GameObject.FindGameObjectWithTag("site");
            origin = transform.position;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (i == targets.Length && getBoard.occupiedNum >= 2)
        {
            agent.SetDestination(origin);
        }
        else
        {
            if (i != -1)
            {
                #region LINE 1
                if (i > 0)
                {
                    if (getBoard.L1targetIsOccupied[i - 1] == false)
                    {
                        targetPoint = targets[i - 1];
                        agent.SetDestination(targetPoint.transform.position);
                        if (Vector3.Distance(transform.position, targetPoint.transform.position) < 1)
                        {
                            i--;
                            getBoard.L1targetIsOccupied[i] = true;
                            if (i < (targets.Length - 1))
                            {
                                getBoard.L1targetIsOccupied[i + 1] = false;
                            }
                        }
                    }
                }

                else if (i == 0)
                {
                    if (waited > waitTimeL1)
                    {
                        getBoard.L1targetIsOccupied[0] = false;
                        agent.SetDestination(origin);
                        i--;
                    }
                    else
                    {
                        getBoard.L1targetIsOccupied[0] = true;
                        waited += Time.deltaTime;
                    }
                }
                #endregion
            }

            else if (i == -1)
            {
                #region LINE 2
                if (j > 0)
                {
                    if (getBoard.L2targetIsOccupied[j - 1] == false)
                    {
                        targetPoint = targetsL2[j - 1];
                        agent.SetDestination(targetPoint.transform.position);
                        if (Vector3.Distance(transform.position, targetPoint.transform.position) < 1)
                        {
                            j--;
                            getBoard.L2targetIsOccupied[j] = true;
                            if (j < (targetsL2.Length - 1))
                            {
                                getBoard.L2targetIsOccupied[j + 1] = false;
                            }
                        }
                    }
                }

                else if (j == 0)
                {
                    if (waitedL2 > waitTimeL2)
                    {
                        getBoard.L2targetIsOccupied[0] = false;
                        agent.SetDestination(origin);
                        j--;
                        finished = FinishStep2();
                    }
                    else
                    {
                        getBoard.L2targetIsOccupied[0] = true;
                        waitedL2 += Time.deltaTime;
                    }
                }
                #endregion
            }
        }
    }

    public bool FinishStep2()
    {
        bool n = false;
        if(waitedL2 > waitTimeL2)
        {
            n = true;
        }
        return n;
    }
}
