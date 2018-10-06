﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour {
    public Transform goal;
    public float walkRadiusMax = 2;
    public float walkRadiusMin = 4;
    NavMeshAgent agent;
    public float range = 10.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadiusMax;

        agent = GetComponent<NavMeshAgent>();
        ChooseNewLocation();
    }
    void ChooseNewLocation()
    {
        Vector3 point;
        while (!RandomPoint(transform.position,range,out point))
        {

        }
        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        agent.SetDestination(point);

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    // Update is called once per frame
    void Update () {
        if(!agent.hasPath)
        {
            ChooseNewLocation();
        }
    }
}
