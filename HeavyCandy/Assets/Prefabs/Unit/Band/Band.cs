using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {

    private bool playing = false;
    
    private GameObject sprite;
    
	protected override void Awake() {
        base.Awake();
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
            StartPlaying();
        }
	}
    
    private void LateUpdate() {
        sprite.transform.forward = Camera.main.transform.forward;
    }

    void StartPlaying() {
        playing = true;
        FactionLogic.Genre randomGenre = RandomizeGenre();
        house.PlayMusic(randomGenre);
        gameObject.SetActive(false);
    }

    private FactionLogic.Genre RandomizeGenre() {
        int i = Random.Range(0, 3);
        if(i < 1) {
            return FactionLogic.Genre.HIP_HOP;
        }
        else if(1 <= i && i < 2) {
            return FactionLogic.Genre.METAL;
        }
        else {
            return FactionLogic.Genre.OPERA;
        }
    }

    public void StopPlaying() {
        if (playing) {
            playing = false;
            house.StopMusic();
        }
    }
}
