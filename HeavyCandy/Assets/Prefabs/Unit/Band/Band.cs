using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {

    private bool playing = false;

	void Awake() {
        base.Initialize();
	}

    public void SetDestination(House house)
    {
        base.SetDestination(house.transform.position);
    }

    void Update () {
        if (AtDestination()) {
            StartPlaying();
        }
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
