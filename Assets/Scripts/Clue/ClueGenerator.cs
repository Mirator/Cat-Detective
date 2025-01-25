using System.Collections.Generic;
using UnityEngine;

public class ClueGenerator
{
    private List<Location> locationSequence;
    private List<Location> locationSequenceBackup;
    private string[] times;
    private int numberOfVillagers;
    private Dictionary<Location, List<Location>> mapConnections;
    private Location finalLocation;

    public ClueGenerator(
        List<Location> locations,
        string[] times,
        Dictionary<Location, List<Location>> mapConnections,
        int numberOfVillagers,
        Location finalLocation)
    {
        this.locationSequence = new List<Location>(locations);
        this.locationSequenceBackup = new List<Location>(locations);
        this.times = times;
        this.mapConnections = mapConnections;
        this.numberOfVillagers = numberOfVillagers;
        this.finalLocation = finalLocation;
    }

    public List<Clue> GenerateClues()
    {
        List<Clue> clues = new List<Clue>();
        int correctClueCount = Mathf.CeilToInt(numberOfVillagers * 0.5f);
        int incorrectClueCount = numberOfVillagers - correctClueCount;

        for (int i = 0; i < correctClueCount; i++)
        {
            Clue correctClue = GenerateSequentialClue();
            if (correctClue != null)
            {
                clues.Add(correctClue);
            }
        }

        for (int i = 0; i < incorrectClueCount; i++)
        {
            Clue randomClue = GenerateRandomClue();
            if (randomClue != null)
            {
                clues.Add(randomClue);
            }
        }

        return clues;
    }

private Clue GenerateSequentialClue()
{
    if (locationSequence.Count < 2)
    {
        Debug.LogWarning("Not enough locations in the sequence to generate a sequential clue.");
        ResetLocationSequence();
    }

    Location seenAt = locationSequence[0];
    Location nextLocation = locationSequence.Count == 2 ? finalLocation : locationSequence[1];
    locationSequence.RemoveAt(0);

    string time = times[Random.Range(0, times.Length)];

    return new Clue
    {
        Time = time,
        SeenAt = seenAt,
        NextLocation = nextLocation
    };
}


    private void ResetLocationSequence()
    {
        Debug.Log("Resetting location sequence.");
        locationSequence = new List<Location>(locationSequenceBackup);
    }

    private Clue GenerateRandomClue()
    {
        Location seenAt = GetRandomLocation();
        Location nextLocation = GetRandomConnectedLocation(seenAt);

        string time = times[Random.Range(0, times.Length)];

        return new Clue
        {
            Time = time,
            SeenAt = seenAt,
            NextLocation = nextLocation
        };
    }

    private Location GetRandomLocation()
    {
        if (locationSequence == null || locationSequence.Count == 0)
        {
            Debug.LogError("No locations available for GetRandomLocation.");
            ResetLocationSequence();
        }

        return locationSequence[Random.Range(0, locationSequence.Count)];
    }

    private Location GetRandomConnectedLocation(Location currentLocation)
    {
        if (mapConnections.ContainsKey(currentLocation))
        {
            List<Location> connections = mapConnections[currentLocation];
            return connections[Random.Range(0, connections.Count)];
        }

        return currentLocation;
    }
}
