using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FactionLogic : MonoBehaviour {
    public int attractionStartValue;
    public int attractionMaxValue;
    public int attractionMinValue;

    public enum Genre {OPERA, METAL, HIP_HOP};
    Dictionary<Genre, int> factionAttractions;

	// Use this for initialization
	void Start () {
        // Initizlize dictionary containing an attraction for each Genre
        factionAttractions = new Dictionary<Genre, int>();
        foreach (Genre genre in System.Enum.GetValues(typeof(Genre))) {
            factionAttractions.Add(genre, attractionStartValue);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Increments the attraction of the given genre by the specified amount,
    /// or by 1 if not specified.
    /// </summary>
    /// <param name="genre"></param>
    /// <param name="amount"></param>
    public void increaseAttraction(Genre genre, int amount = 1) {
        factionAttractions[genre] = clampAttraction(factionAttractions[genre] + amount);
    }

    /// <summary>
    /// Decrements the attraction of the given genre by the specified amount,
    /// or by 1 if not specified.
    /// </summary>
    /// <param name="genre"></param>
    /// <param name="amount"></param>
    public void decreaseAttraction(Genre genre, int amount = 1)
    {
        factionAttractions[genre] = clampAttraction(factionAttractions[genre] - amount);
    }

    /// <summary>
    /// Returns the percentage of attraction for each Genre.
    /// </summary>
    /// <returns>A Dictionary containing the probabilities (percentage of total attraction)
    /// for each genre. Values for each Key range between 0 to 1. </returns>
    public Dictionary<Genre, double> GetProbabilities() {
        int totalAttraction = 0; // Sum of attraction for all genres
        Dictionary<Genre, double> probabilities = new Dictionary<Genre, double>();

        // Get total attraction i for-each below
        foreach (Genre g in System.Enum.GetValues(typeof(Genre)))
        {
            int retreivedAttraction;
            bool successfulRetreival = factionAttractions.TryGetValue(g, 
                out retreivedAttraction);

            if (successfulRetreival) {
                totalAttraction += retreivedAttraction;
                probabilities.Add(g, 0.0);
            }
            else {
                Debug.LogError("Failed to retreive a value for " + g);
                return null;
            }
        }

        foreach (Genre g in System.Enum.GetValues(typeof(Genre))) {
            int retreivedAttraction;
            factionAttractions.TryGetValue(g, out retreivedAttraction);

            probabilities[g] = (((double)retreivedAttraction) / ((double)totalAttraction));
        }

        return probabilities;
    }

    private int clampAttraction(int attraction)
    {
        if (attraction > attractionMaxValue)
            return attractionMaxValue;
        else if (attraction < attractionMinValue)
            return attractionMinValue;

        return attraction;
    }

    // Ugly tests below
    private void Test()
    {
        factionAttractions[Genre.OPERA] = 50;
        factionAttractions[Genre.METAL] = 20;
        factionAttractions[Genre.HIP_HOP] = 80;

        Dictionary<Genre, double> probabilities = GetProbabilities();

        Debug.Log(string.Format("Opera: {0}", probabilities[Genre.OPERA]), gameObject);
        Debug.Log(string.Format("Metal: {0}", probabilities[Genre.METAL]), gameObject);
        Debug.Log(string.Format("Hip hop: {0}", probabilities[Genre.HIP_HOP]), gameObject);

        Debug.Log(string.Format("Opera: {0}", factionAttractions[Genre.OPERA]), gameObject);
        increaseAttraction(Genre.OPERA);

        Debug.Log(string.Format("Opera: {0}", factionAttractions[Genre.OPERA]), gameObject);
        increaseAttraction(Genre.OPERA, 1000);

        Debug.Log(string.Format("Opera: {0}", factionAttractions[Genre.OPERA]), gameObject);
        decreaseAttraction(Genre.OPERA, 1000);

        Debug.Log(string.Format("Opera: {0}", factionAttractions[Genre.OPERA]), gameObject);

        Debug.Log(string.Format("Metal: {0}", factionAttractions[Genre.METAL]), gameObject);
        increaseAttraction(Genre.METAL, 1000);

        Debug.Log(string.Format("Metal: {0}", factionAttractions[Genre.METAL]), gameObject);
        decreaseAttraction(Genre.METAL, 1000);
        Debug.Log(string.Format("Metal: {0}", factionAttractions[Genre.METAL]), gameObject);
    }
}
