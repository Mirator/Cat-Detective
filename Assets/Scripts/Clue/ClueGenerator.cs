using System.Collections.Generic;
using UnityEngine;

public class ClueGenerator
{
    private Graph<Location> map;
    private string[] times;
    private int numberOfVillagers;
    private Location finalLocation;

    public ClueGenerator(Graph<Location> map, string[] times, int numberOfVillagers, Location finalLocation)
    {
        this.map = map;
        this.times = times;
        this.numberOfVillagers = numberOfVillagers;
        this.finalLocation = finalLocation;
    }

    /// <summary>
    /// Generates all clues (correct, incorrect, random).
    /// </summary>
    public List<Clue> GenerateClues()
    {
        List<Clue> clues = new List<Clue>();

        int correctClueCount = Mathf.Clamp((int)(numberOfVillagers * Random.Range(0.51f, 0.75f)), 1, numberOfVillagers);
        int incorrectClueCount = Mathf.Clamp((int)(numberOfVillagers * Random.Range(0.1f, 0.25f)), 0, numberOfVillagers - correctClueCount);
        int randomClueCount = numberOfVillagers - correctClueCount - incorrectClueCount;

        Debug.Log($"Clues: {correctClueCount} correct, {incorrectClueCount} incorrect, {randomClueCount} random");

        clues.AddRange(GenerateCorrectClues(correctClueCount));
        clues.AddRange(GenerateIncorrectClues(incorrectClueCount));
        clues.AddRange(GenerateRandomClues(randomClueCount));

        return clues;
    }

    private List<Clue> GenerateCorrectClues(int count)
    {
        List<Clue> correctClues = new List<Clue>();
        List<Location> path = map.GetPathTo(finalLocation);

        for (int i = 0; i < count && path.Count > 1; i++)
        {
            Location seenAt = path[0];
            Location nextLocation = path[1];
            path.RemoveAt(0);

            string time = times[Random.Range(0, times.Length)];

            correctClues.Add(new Clue
            {
                Time = time,
                SeenAt = seenAt,
                NextLocation = nextLocation
            });

            if (nextLocation == finalLocation)
            {
                Debug.Log($"Final clue: {seenAt} -> {nextLocation}");
            }
        }

        return correctClues;
    }

    private List<Clue> GenerateIncorrectClues(int count)
    {
        List<Clue> incorrectClues = new List<Clue>();

        for (int i = 0; i < count; i++)
        {
            Location seenAt = GetRandomLocation();
            Location nextLocation = GetRandomConnectedLocation(seenAt);

            if (nextLocation == finalLocation)
            {
                nextLocation = GetRandomConnectedLocation(nextLocation);
            }

            string time = times[Random.Range(0, times.Length)];

            incorrectClues.Add(new Clue
            {
                Time = time,
                SeenAt = seenAt,
                NextLocation = nextLocation
            });
        }

        return incorrectClues;
    }

    private List<Clue> GenerateRandomClues(int count)
    {
        List<Clue> randomClues = new List<Clue>();

        for (int i = 0; i < count; i++)
        {
            Location seenAt = GetRandomLocation();
            Location nextLocation = GetRandomLocation();

            string time = times[Random.Range(0, times.Length)];

            randomClues.Add(new Clue
            {
                Time = time,
                SeenAt = seenAt,
                NextLocation = nextLocation
            });
        }

        return randomClues;
    }

    private Location GetRandomLocation()
    {
        List<Location> allLocations = map.GetAllNodes();
        return allLocations[Random.Range(0, allLocations.Count)];
    }

    private Location GetRandomConnectedLocation(Location currentLocation)
    {
        List<Location> connections = map.GetConnections(currentLocation);
        return connections[Random.Range(0, connections.Count)];
    }
}
