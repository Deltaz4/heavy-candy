using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour {

    List<House> houses;

	// Use this for initialization
	void Awake () {
        houses = new List<House>();
	}

    public void AddHouse(House house) {
        houses.Add(house);
    }

    public List<House> GetPartyingHouses() {
        List<House> partyingHouses = new List<House>();

        foreach (House house in houses) {
            if (house.hasPerformingBand)
                partyingHouses.Add(house);
        }

        return partyingHouses;
    }

    public List<House> GetPartyingHousesByGenre(FactionLogic.Genre genre) {
        List<House> partyingHouses = new List<House>();

        foreach (House house in houses) {
            if (house.hasPerformingBand && house.genre == genre)
                partyingHouses.Add(house);
        }

        return partyingHouses;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
