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
    int successfulPoliceStings; // Will never decay, as opposed to gameOverMeter


    int gameOverMax; // The value of gameOverMeter at which the game is over
    int gameOverMeter; // A value between 0 and <gameOverMax>, used to determine how close the police are to busting the HQ 
    public int policeStingMultiplier; // How much each successful police sting will add to gameOverMeter
    public float gameOverDecayInterval; // Interval in seconds at which the gameOverMeter is reduced by 1. (Never reduced if interval is set to 0)
    float timeSinceLastDecay;

    void Start() {
        factionLogic = new FactionLogic();
        successfulPoliceStings = 0;
        gameOverMax = 100;
        gameOverMeter = 0;

        if (policeStingMultiplier == 0)
            policeStingMultiplier = 5;
    }

    void Update () {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > policeSpawnInterval) {
            GeneratePoliceSquad();
            timeSinceLastSpawn -= policeSpawnInterval;
        }

        if (gameOverDecayInterval != 0) {
            timeSinceLastDecay += Time.deltaTime;
            if (timeSinceLastDecay > gameOverDecayInterval) {
                DecayGameMeter();
                timeSinceLastDecay -= gameOverDecayInterval;
            }
        }
    }

    private void GeneratePoliceSquad() {
        FactionLogic.Genre targetGenre = PickGenre();
        House targetHouse = PickHouse(targetGenre);

        //Debug.Log(string.Format("Genre: {0}, House: {1}", targetGenre, targetHouse));

        if (targetHouse != null)
        {
            // Problematic if no houses are partying with the picked genre
            // Can be solved by adjusting the PickGenre() method.
            policeStation.DeployPoliceSquad(targetGenre, targetHouse);
        }
    }

    private void DecayGameMeter() {
        if (gameOverMeter == 0)
            return;
        gameOverMeter--;
    }

    private void IncrementGameMeter() {
        gameOverMeter += policeStingMultiplier;
        if (gameOverMeter > gameOverMax) {
            gameOverMeter = gameOverMax;
            // Game over!
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

    public bool IsGameOver() {
        return (gameOverMeter == gameOverMax);
    }

    public void DestinationReached(FactionLogic.Genre genre, bool foundCandy) {
        if (foundCandy) { 
            successfulPoliceStings++;
            IncrementGameMeter();
        }
    }

    public int GetGameOverMax() {
        return gameOverMax;
    }

    public int GetGameOverMeter() {
        return gameOverMeter;
    }
}
