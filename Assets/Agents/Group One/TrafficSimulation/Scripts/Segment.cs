﻿// Traffic Simulation
// https://github.com/mchrbn/unity-traffic-simulation

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace TrafficSimulation{

    public class Segment : MonoBehaviour {
        public List<Segment> nextSegments;
        [HideInInspector]
        public int id;
        [HideInInspector]
        public List<Waypoint> waypoints;

        public bool IsOnSegment(Vector3 p){
            TrafficSystem ts = GameObject.FindObjectOfType<TrafficSystem>();

            for(int i=0; i < waypoints.Count - 1; i++){
                float d1 = Vector3.Distance(waypoints[i].transform.position, p);
                float d2 = Vector3.Distance(waypoints[i+1].transform.position, p);
                float d3 = Vector3.Distance(waypoints[i].transform.position, waypoints[i+1].transform.position);
                float a = (d1 + d2) - d3;
                if(a < ts.segDetectThresh && a > -ts.segDetectThresh)
                    return true;

            }
            return false;
        }

        #if UNITY_EDITOR
        void OnDrawGizmos(){

            if(GameObject.FindObjectOfType<TrafficSystem>().hideGuizmos) return;

            GUIStyle style = new GUIStyle();
            style.normal.textColor = new Color(1, 0, 0);
            style.fontSize = 15;
            Handles.Label(this.transform.position, this.name, style);
        }
        #endif
    }
}