﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class Unit : MonoBehaviour {

    NavMeshAgent agent;
    private Collider collisionGameObject;
    protected House house;
    public float destinationHitRadius;

    /// <summary>
    /// Sets the NavMeshAgent of the implementing class.
    /// </summary>
	protected void Initialize () {
        agent = GetComponent<NavMeshAgent>();
	}

    protected void SetDestination(Vector3 destination) {
        agent.destination = destination;
    }

    protected bool AtDestination() {
        Vector3 closestSurfacePoint1;
        Vector3 closestSurfacePoint2;

        closestSurfacePoint1 = collisionGameObject.ClosestPointOnBounds(house.transform.position);
        closestSurfacePoint2 = house.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

        float surfaceDistance = Vector3.Distance(closestSurfacePoint1, closestSurfacePoint2);
        return (surfaceDistance < destinationHitRadius);
    }
}
