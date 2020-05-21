using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{

    List<House> houses;
    List<Buyer> buyers;
    public int maxBuyers = 50;

    public Buyer buyerPrefab;
    private FactionLogic buyersFactionLogic;

    // Use this for initialization
    void Awake()
    {
        houses = new List<House>();
        buyers = new List<Buyer>();
        buyersFactionLogic = new FactionLogic();

        // Call once, and let it invoke itself as it pleases
        SpawnBuyer();
        InvokeRepeating("AttemptToLureBuyer", 1.0f, 1.0f);
    }

    public void SpawnBuyer()
    {
        if (houses.Count != 0 && buyers.Count < maxBuyers)
        {
            System.Random random = new System.Random();
            int r = random.Next(houses.Count);

            Buyer deployedBuyer = (Buyer)Instantiate(buyerPrefab,
                houses[r].transform.position - 10 * Vector3.right,
                houses[r].transform.rotation);

            deployedBuyer.transform.parent = gameObject.transform;
            deployedBuyer.SetHouse(houses[r]);

            buyers.Add(deployedBuyer);
        }

        Invoke("SpawnBuyer", Random.Range(1.0f, 3.0f));
    }

    public void AddHouse(House house)
    {
        houses.Add(house);
    }

    public List<House> GetPartyingHouses()
    {
        List<House> partyingHouses = new List<House>();

        foreach (House house in houses)
        {
            if (house.hasPerformingBand)
                partyingHouses.Add(house);
        }

        return partyingHouses;
    }

    public List<House> GetPartyingHousesByGenre(FactionLogic.Genre genre)
    {
        List<House> partyingHouses = new List<House>();

        foreach (House house in houses)
        {
            if (house.hasPerformingBand && house.genre == genre)
                partyingHouses.Add(house);
        }

        return partyingHouses;
    }

    public List<Buyer> GetAimlessBuyers()
    {
        List<Buyer> aimlessBuyers = new List<Buyer>();
        foreach (Buyer buyer in buyers)
        {
            if (buyer.IsWalkingRandomly())
                aimlessBuyers.Add(buyer);
        }
        return aimlessBuyers;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AttemptToLureBuyer()
    {
        List<House> partyingHouses = GetPartyingHouses();
        List<Buyer> targets = GetAimlessBuyers();

        if (partyingHouses.Count == 0 || targets.Count == 0)
            return;

        // Pick a random index
        System.Random random = new System.Random();
        int houseIndex = random.Next(partyingHouses.Count);

        List<House> partyingHousesByGenre = GetPartyingHousesByGenre(partyingHouses[houseIndex].genre);

        Dictionary<FactionLogic.Genre, float> probabilities = buyersFactionLogic.GetProbabilities();
        float attraction;
        bool successfulRetrieval = probabilities.TryGetValue(partyingHouses[houseIndex].genre, out attraction);

        if (!successfulRetrieval)
        {
            Debug.LogError("Failed to retrieve value from a genre!");
            return;
        }

        float ratioGenreHouses = ((float)partyingHousesByGenre.Count) / ((float)partyingHouses.Count);
        float totalProbability = attraction * ratioGenreHouses;

        if (Random.Range(0.0f, 1.0f) > totalProbability)
        {
            int targetIndex = random.Next(targets.Count);
            targets[targetIndex].SetHouse(partyingHouses[houseIndex]);
        }
    }

    public void DestinationReached(Buyer buyer)
    {
        buyers.Remove(buyer);
    }
}
