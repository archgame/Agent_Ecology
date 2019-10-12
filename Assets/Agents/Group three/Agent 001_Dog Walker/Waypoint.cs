﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Waypoint : MonoBehaviour
{
    [SerializeField]
    // Start is called before the first frame update
    protected float debugDrawRadius = 1.0f;

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
    }
}