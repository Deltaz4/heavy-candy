﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

abstract public class Unit : MonoBehaviour
{

    NavMeshAgent agent;
    public Collider collisionGameObject;
    public House house;
    public float destinationHitRadius;

    /// <summary>
    /// Sets the NavMeshAgent of the implementing class.
    /// </summary>
	protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected void SetDestination(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }

    public virtual void SetHouse(House house)
    {
        this.house = house;
        SetDestination(house.transform.position);
    }

    protected bool AtDestination()
    {
        if (house != null)
        { // If the unit is navigating to a house
            Vector3 closestSurfacePoint1;
            Vector3 closestSurfacePoint2;

            closestSurfacePoint1 = collisionGameObject.ClosestPointOnBounds(house.transform.position);
            closestSurfacePoint2 = house.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

            float surfaceDistance = Vector3.Distance(closestSurfacePoint1, closestSurfacePoint2);
            return (surfaceDistance < destinationHitRadius);
        }
        else
        { // If the unit is aimlessly wandering around
            return (Vector3.Distance(agent.destination, transform.position) < destinationHitRadius);
        }
    }
}
