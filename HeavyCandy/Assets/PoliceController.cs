using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoliceController : MonoBehaviour {

    private UnityAction policeReachedDestination;

    public PoliceStation policeStation;
    FactionLogic factionLogic;
    public float policeSpawnInterval;
    float timeSinceLastSpawn;

    void Start() {
        factionLogic = new FactionLogic();
        
    }

    void Update () {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > policeSpawnInterval)
        {
            policeStation.DeployPoliceSquad(PickGenre());
            timeSinceLastSpawn -= policeSpawnInterval;
        }
    }

    private FactionLogic.Genre PickGenre() {
        Dictionary<FactionLogic.Genre, float> probabilities = factionLogic.GetProbabilities();

        float randomValue = Random.Range(0.0f, 1.0f);
        float rangeStart = 0.0f;
        float rangeEnd = 0.0f;

        foreach (KeyValuePair<FactionLogic.Genre, float> entry in probabilities)
        {
            rangeEnd += entry.Value;

            if (randomValue > rangeStart && randomValue < rangeEnd) {
                return entry.Key;
            }
            rangeStart = rangeEnd;
        }

        Debug.LogError("PickGenre failed to calculate a genre to pick!");
        return FactionLogic.Genre.METAL;
    }

    public void DestinationReached(FactionLogic.Genre genre, bool foundCandy) {
        Debug.Log(string.Format("Reached house with genre {0}", genre));
    }
}
