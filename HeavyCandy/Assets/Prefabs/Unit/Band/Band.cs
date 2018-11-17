using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {

    public Transform startingPoint;
    public House house;
    public Collider collisionGameObject;
    public float destinationHitRadius;

	void Start () {
        base.Initialize();

        // Remove
        SetDestination(house);
	}

    public void SetDestination(House house) {
        base.SetDestination(house.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        if (atDestination()) {
            // Do something! Resets to starting position for now.
            transform.position = startingPoint.position;
        }
	}

    private bool atDestination() {
        Vector3 closestSurfacePoint1;
        Vector3 closestSurfacePoint2;

        closestSurfacePoint1 = collisionGameObject.ClosestPointOnBounds(house.transform.position);
        closestSurfacePoint2 = house.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

        float surfaceDistance = Vector3.Distance(closestSurfacePoint1, closestSurfacePoint2);
        return (surfaceDistance < destinationHitRadius);
    } 
}
