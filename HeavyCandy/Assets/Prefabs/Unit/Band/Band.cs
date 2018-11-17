using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {

    public Transform startingPoint;

	void Awake () {
        base.Initialize();

        // Remove
        SetDestination(house);
	}

    public void SetDestination(House house)
    {
        base.SetDestination(house.transform.position);
    }

    void Update () {
        if (atDestination()) {
            // Do something! Resets to starting position for now.
            transform.position = startingPoint.position;
        }
	}
}
