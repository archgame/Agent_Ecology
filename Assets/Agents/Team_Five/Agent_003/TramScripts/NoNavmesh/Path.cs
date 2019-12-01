﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public enum PathTypes
    {
        linear,
        loop
    }
    public PathTypes PathType;
    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathSequence;
    public bool Reached = false;
    private int t;
    private Transform Point;
    public int ClosestDistancetoPoint = 2;






    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        Point = PathSequence[t];


    }

    public void OnDrawGizmos()
    {
        if (PathSequence == null || PathSequence.Length < 2)
        {
            return;
        }

        for (var i = 1; i < PathSequence.Length; i++)
        {
            Gizmos.DrawLine(PathSequence[i - 1].position, PathSequence[i].position);

        }
        if (PathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathSequence[0].position, PathSequence[PathSequence.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathSequence == null || PathSequence.Length < 1)
        {
            yield break;
        }
        while (true)
        {
            yield return PathSequence[movingTo];

            float DistancefromPoint = Vector3.Distance(gameObject.transform.position, Point.transform.position);

            if (PathSequence.Length == 1)
            {
                continue;
            }
            if (PathType == PathTypes.linear)
            {
                if (movingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if (movingTo >= PathSequence.Length - 1)
                {
                    Reached = true;
                    movementDirection = 0;

                }


            }


            movingTo = movingTo + movementDirection;


            if (PathType == PathTypes.loop)
            {
                if (movingTo >= PathSequence.Length)
                {
                    yield break;
                }
                if (movingTo < 0)
                {
                    movingTo = PathSequence.Length - 1;
                }
            }
        }
    }

    public IEnumerator<Transform> GetStartPathPoint()
    {
        yield return PathSequence[0];
        movingTo = 0;
        Reached = false;
    }
}