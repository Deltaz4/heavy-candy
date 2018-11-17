using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {

    public House house;
    public GameObject collisionChild;

	void Awake () {
        base.Initialize();
	}

    public void SetDestination(House house) {
        base.SetDestination(house.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool atDestination() {
        return false;
    } 
}
