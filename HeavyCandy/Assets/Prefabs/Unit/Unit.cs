using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class Unit : MonoBehaviour {

    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    protected void SetDestination(Vector3 destination) {
        agent.destination = destination;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
