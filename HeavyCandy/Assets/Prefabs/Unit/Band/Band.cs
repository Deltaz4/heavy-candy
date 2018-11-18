﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {

    private bool playing = false;

    public void SetDestination(House house)
    {
        base.SetDestination(house.transform.position);
    }

    void Update () {
        if (AtDestination()) {
            playing = true;
            //Activate house animation

        }
	}
}
