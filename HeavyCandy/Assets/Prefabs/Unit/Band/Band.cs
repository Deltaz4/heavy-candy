using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {

    private bool playing = false;

    private GameObject sprite;

	void Awake () {
        base.Initialize();
        sprite = transform.Find("Sprite").gameObject;
	}

    public void SetDestination(House house)
    {
        base.SetDestination(house.transform.position);
    }

    void Update ()
    {
        sprite.GetComponent<UnitSprite>();
        sprite.GetComponent<UnitSprite>().SetRotation(transform.rotation.eulerAngles.y);

        if (AtDestination()) {
            playing = true;
            //Activate house animation

        }
	}

    private void LateUpdate()
    {
        sprite.transform.forward = Camera.main.transform.forward;
    }
}
