using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : Unit {
    
    public bool playing = false;
    [HideInInspector]
    public FactionLogic.Genre genre;

    private GameObject sprite;
    private UnitSprite unitSprite;

    public Sprite operaBackLeft;
    public Sprite operaBackRight;
    public Sprite operaFrontLeft;
    public Sprite operaFrontRight;

    public Sprite metalBackLeft;
    public Sprite metalBackRight;
    public Sprite metalFrontLeft;
    public Sprite metalFrontRight;

    public Sprite hiphopBackLeft;
    public Sprite hiphopBackRight;
    public Sprite hiphopFrontLeft;
    public Sprite hiphopFrontRight;

    protected override void Awake()
    {
        base.Awake();
        sprite = transform.Find("Sprite").gameObject;
        unitSprite = sprite.GetComponent<UnitSprite>();
    }

    private void Start() {
        if (genre == FactionLogic.Genre.OPERA) {
            unitSprite.backLeft = operaBackLeft;
            unitSprite.backRight = operaBackRight;
            unitSprite.frontLeft = operaFrontLeft;
            unitSprite.frontRight = operaFrontRight;
        } else if (genre == FactionLogic.Genre.METAL) {
            unitSprite.backLeft = metalBackLeft;
            unitSprite.backRight = metalBackRight;
            unitSprite.frontLeft = metalFrontLeft;
            unitSprite.frontRight = metalFrontRight;
        } else if (genre == FactionLogic.Genre.HIP_HOP) {
            unitSprite.backLeft = hiphopBackLeft;
            unitSprite.backRight = hiphopBackRight;
            unitSprite.frontLeft = hiphopFrontLeft;
            unitSprite.frontRight = hiphopFrontRight;
        }
    }

    public void SetGenre(FactionLogic.Genre genre) {
        this.genre = genre;
        
    }

    public void SetDestination(House house)
    {
        base.SetDestination(house.transform.position);
    }

    public override void SetHouse(House house)
    {
        this.house = house;
        if (!playing)
        {
            SetDestination(house.transform.position);
        }
    }

    public void setDestination()
    {
        SetDestination(house.transform.position);
    }

    void Update () {
        sprite.GetComponent<UnitSprite>().SetRotation(transform.rotation.eulerAngles.y);
        if (!playing && AtDestination() && !house.hasPerformingBand) {
            StartPlaying();
        }
	}

    void StartPlaying()
    {
        playing = true;
        house.PlayMusic(this);
        gameObject.SetActive(false);
    }

    private FactionLogic.Genre RandomizeGenre() {
        int i = Random.Range(0, 3);
        if(i < 1) {
            return FactionLogic.Genre.HIP_HOP;
        }
        else if(1 <= i && i < 2)
        {
            return FactionLogic.Genre.METAL;
        }
        else
        {
            return FactionLogic.Genre.OPERA;
        }
    }

    public void StopPlaying() {
        if (playing)
        {
            playing = false;
        }
    }
}
