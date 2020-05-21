using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDelivery : Unit
{
    private GameObject sprite;
    int candyCount;

    new protected virtual void Awake()
    {
        base.Awake();
        destinationHitRadius = 5.0f;
        sprite = transform.Find("Sprite").gameObject;
    }

    public void SetCandyCount(int candyCount)
    {
        this.candyCount = candyCount;
    }

    void Update()
    {
        sprite.GetComponent<UnitSprite>().SetRotation(transform.rotation.eulerAngles.y);
        if (base.house != null && AtDestination())
        {
            house.IncreaseCandyCount(candyCount);
            Destroy(gameObject);
        }
    }
}