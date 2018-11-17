using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class Unit : MonoBehaviour {

    NavMeshAgent agent;

    /// <summary>
    /// Sets the NavMeshAgent of the implementing class.
    /// </summary>
	protected void Initialize () {
        agent = GetComponent<NavMeshAgent>();
	}

    protected void SetDestination(Vector3 destination) {
        agent.destination = destination;
    }
}
