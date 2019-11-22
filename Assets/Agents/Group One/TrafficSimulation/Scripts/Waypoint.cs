﻿// Traffic Simulation
// https://github.com/mchrbn/unity-traffic-simulation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficSimulation{
    public class Waypoint : MonoBehaviour
    {
        [HideInInspector]
        public int id;
        [HideInInspector]
        public Segment segment;

        void OnDrawGizmos(){
            //Draw sphere, increase color to show the direction
            Gizmos.color = new Color(0f, 0f, 1f, (id + 1) / (float) segment.waypoints.Count);
            Gizmos.DrawSphere(this.transform.position, .5f);
        }
    }
}
