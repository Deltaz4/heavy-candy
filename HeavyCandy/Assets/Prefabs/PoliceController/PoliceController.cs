using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoliceController : MonoBehaviour {

    public HouseController houseController;
    public PoliceStation policeStation;
    FactionLogic factionLogic;
    public float policeSpawnInterval;
    float timeSinceLastSpawn;

    void Start() {
        factionLogic = new FactionLogic();
        
    }

    void Update () {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > policeSpawnInterval) {
            FactionLogic.Genre targetGenre = PickGenre();
            House targetHouse = PickHouse(targetGenre);

            //Debug.Log(string.Format("Genre: {0}, House: {1}", targetGenre, targetHouse));

            if (targetHouse != null) { 
                // Problematic if no houses are partying with the picked genre
                // Can be solved by adjusting the PickGenre() method.
                policeStation.DeployPoliceSquad(targetGenre, targetHouse);
            } 
            timeSinceLastSpawn -= policeSpawnInterval;
        }
    }

    private FactionLogic.Genre PickGenre() {
        Dictionary<FactionLogic.Genre, float> probabilities = factionLogic.GetProbabilities();

        float randomValue = Random.Range(0.0f, 1.0f);
        float rangeStart = 0.0f;
        float rangeEnd = 0.0f;

        foreach (KeyValuePair<FactionLogic.Genre, float> entry in probabilities) {
            rangeEnd += entry.Value;

            if (randomValue > rangeStart && randomValue < rangeEnd) {
                return entry.Key;
            }
            rangeStart = rangeEnd;
        }

        Debug.LogError("PickGenre failed to calculate a genre to pick!");
        return FactionLogic.Genre.METAL;
    }

    private House PickHouse(FactionLogic.Genre genre) {
        List<House> houses = houseController.GetPartyingHousesByGenre(genre);
        if (houses.Count == 0)
            return null;

        System.Random random = new System.Random();
        int r = random.Next(houses.Count);

        return houses[r];
    }

    public void DestinationReached(FactionLogic.Genre genre, bool foundCandy) {
        //Debug.Log(string.Format("Reached house with genre {0}", genre));
    }
}
