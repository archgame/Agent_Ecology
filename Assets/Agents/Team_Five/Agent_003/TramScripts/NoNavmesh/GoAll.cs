using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAll : MonoBehaviour
{
    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }

    private Coroutine Loop;
    public MovementType Type = MovementType.MoveTowards;
    public Path[] MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;
    private IEnumerator<Transform> pointInPath;
    private Vector3 Direction;
    private float step;
    public float waitTime = 0;
    public bool waiting = false;
    private float waited = 0;
    public bool Iamstoppedandwaiting = false;
    private int i;
    public bool ReadyToGo;
    private Path cpath;


    // Start is called before the first frame update
    void Start()
    {


        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
            return;
        }


        i = 0;
        cpath = MyPath[i];


        pointInPath = MyPath[i].GetNextPathPoint();
        pointInPath.MoveNext();
        transform.position = pointInPath.Current.position;

        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return;
        }



    }


    // Update is called once per frame
    void Update()
    {
        //guard statement
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        if (Type == MovementType.MoveTowards)
        {
            //Debug.Log("MovementType.MoveTowards");
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
            Direction = pointInPath.Current.position - transform.position;
            step = Speed * Time.deltaTime;
            Vector3 NewDirection = Vector3.RotateTowards(transform.forward, Direction, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(NewDirection);
        }
        else if (Type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
            Direction = pointInPath.Current.position - transform.position;
            step = Speed * Time.deltaTime;
            Vector3 NewDirection = Vector3.RotateTowards(transform.forward, Direction, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(NewDirection);
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            //Debug.Log("pointInPath.MoveNext()");
            pointInPath.MoveNext();
        }

        //condition for end of path reached station
        if (MyPath[i].Reached == true && i<=2)
        {
            Debug.Log("Station");

            if (waiting == false )
            {
                if (waited < waitTime)
                {
                    waited += Time.deltaTime;
                    Iamstoppedandwaiting = true;
                    ReadyToGo = false;
                }
                else
                {
                    waiting = false;
                    waited = 0;
                    Iamstoppedandwaiting = false;
                    ReadyToGo = true;                                       
                }

            }
            else
            {
                waiting = true;

            }
        }

        i = i + 1;
        Debug.Log(" i = i + 1");
        pointInPath = MyPath[i].GetNextPathPoint();
        pointInPath.MoveNext();
        transform.position = pointInPath.Current.position;

        if (MyPath[3].Reached == true)
        {
            Debug.Log("Station");

            if (waiting == false)
            {
                if (waited < waitTime)
                {
                    waited += Time.deltaTime;
                    Iamstoppedandwaiting = true;
                    ReadyToGo = false;
                }
                else
                {
                    waiting = false;
                    waited = 0;
                    Iamstoppedandwaiting = false;
                    ReadyToGo = true;

                    i = 0;
                    Debug.Log(" i = 0");
                    pointInPath = MyPath[i].GetNextPathPoint();
                    pointInPath.MoveNext();
                    transform.position = pointInPath.Current.position;
                    

                }

            }
            else
            {
                waiting = true;

            }


        }



        //Debug.Log("i >= MyPath.Length");



    }









}
