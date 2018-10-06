using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour {
    public Transform goal;
    public float walkRadius = 2;

    // Use this for initialization
    void Start () {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
        Vector3 finalPosition = hit.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
