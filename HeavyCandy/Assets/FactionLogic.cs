using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FactionLogic
{
    int attractionStartValue = 50;
    int attractionMaxValue = 100;
    int attractionMinValue = 0;

    public enum Genre { OPERA, METAL, HIP_HOP };
    Dictionary<Genre, int> factionAttractions;

    public FactionLogic()
    {
        // Initizlize dictionary containing an attraction for each Genre
        factionAttractions = new Dictionary<Genre, int>();
        foreach (Genre genre in System.Enum.GetValues(typeof(Genre)))
        {
            factionAttractions.Add(genre, attractionStartValue);
        }
    }

    /// <summary>
    /// Increments the attraction of the given genre by the specified amount,
    /// or by 1 if not specified.
    /// </summary>
    /// <param name="genre"></param>
    /// <param name="amount"></param>
    public void increaseAttraction(Genre genre, int amount = 1)
    {
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
    public Dictionary<Genre, float> GetProbabilities()
    {
        int totalAttraction = 0; // Sum of attraction for all genres
        Dictionary<Genre, float> probabilities = new Dictionary<Genre, float>();

        // Get total attraction i for-each below
        foreach (Genre g in System.Enum.GetValues(typeof(Genre)))
        {
            int retreivedAttraction;
            bool successfulRetreival = factionAttractions.TryGetValue(g,
                out retreivedAttraction);

            if (successfulRetreival)
            {
                totalAttraction += retreivedAttraction;
                probabilities.Add(g, 0.0f);
            }
            else
            {
                Debug.LogError("Failed to retreive a value for " + g);
                return null;
            }
        }

        foreach (Genre g in System.Enum.GetValues(typeof(Genre)))
        {
            int retreivedAttraction;
            factionAttractions.TryGetValue(g, out retreivedAttraction);

            probabilities[g] = (((float)retreivedAttraction) / ((float)totalAttraction));
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
}
