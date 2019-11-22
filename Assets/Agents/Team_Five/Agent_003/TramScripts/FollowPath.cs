using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        MoveTowards,
        LerpTowards
    }

    public MovementType Type = MovementType.MoveTowards;
    public movementpath MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;
    private IEnumerator<Transform> pointInPath;
    // Start is called before the first frame update
    void Start()
    {
        if (MyPath == null)
        {
            Debug.LogError("Movement Path cannot be null, I must have a path to follow.", gameObject);
            return;
        }
        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();
        if(pointInPath.Current == null)
        {
            Debug.LogError("A path must have points in it to follow", gameObject);
            return;
        }
        transform.position = pointInPath.Current.position;

    }


    // Update is called once per frame
    void Update()
    {
        if (pointInPath == null || pointInPath.Current ==null)
        {
            return;
        }
        if (Type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);
                    }
        else if (Type == MovementType.LerpTowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * Speed);

        }
        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }

        

       

    }
}
