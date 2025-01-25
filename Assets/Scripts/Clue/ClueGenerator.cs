using System.Collections.Generic;
using UnityEngine;

public class ClueGenerator
{
    private Dictionary<Location, List<Location>> mapConnections;
    private List<Location> correctPath;
    private TimeOfDay[] times;
    private MapManager mapManager;

    public ClueGenerator(MapManager mapManager, Dictionary<Location, List<Location>> mapConnections, List<Location> correctPath, TimeOfDay[] times)
    {
        this.mapManager = mapManager;
        this.mapConnections = mapConnections;
        this.correctPath = correctPath;
        this.times = times;
    }

    /// <summary>
    /// Generates all clues at once (correct, incorrect, random).
    /// </summary>
    public List<Clue> GenerateClues(int correctClueCount, int incorrectClueCount, int randomClueCount)
    {
        List<Clue> clues = new List<Clue>();

        // Generate correct clues
        clues.AddRange(GenerateCorrectClues(correctClueCount));

        // Generate incorrect clues
        clues.AddRange(GenerateIncorrectClues(incorrectClueCount));

        // Generate random clues
        clues.AddRange(GenerateRandomClues(randomClueCount));

        return clues;
    }

    private List<Clue> GenerateCorrectClues(int count)
    {
        List<Clue> correctClues = new List<Clue>();
        int timeIndex = 0; // Start from Morning

        for (int i = 0; i < count && i < correctPath.Count - 1; i++)
        {
            TimeOfDay time = (TimeOfDay)timeIndex;
            correctClues.Add(new Clue
            {
                SeenAt = correctPath[i],
                NextLocation = correctPath[i + 1],
                Time = time
            });

            Debug.Log($"[GenerateCorrectClues] Generating clue for index {i}. Time: {time}");
            Debug.Log($"[GenerateCorrectClues] Updated timeIndex to: {timeIndex}");

            // Increment the timeIndex and wrap around if necessary
            timeIndex = (timeIndex + 1) % times.Length;
        }

        return correctClues;
    }
 private List<Clue> GenerateIncorrectClues(int count)
{
    List<Clue> incorrectClues = new List<Clue>();

    for (int i = 0; i < count; i++)
    {
        // Get a random location that is not part of the correct path
        Location seenAt = GetRandomLocationExcluding(correctPath);

        // Get a connected location for the 'NextLocation'
        List<Location> neighbors = mapManager.GetNeighbors(seenAt);
        
        if (neighbors == null || neighbors.Count == 0)
        {
            // If no neighbors exist, fallback to another random location
            Location fallbackLocation;
            do
            {
                fallbackLocation = GetRandomLocationExcluding(correctPath);
            } while (fallbackLocation == seenAt);

            neighbors = new List<Location> { fallbackLocation };
        }

        Location nextLocation = neighbors[Random.Range(0, neighbors.Count)];

        // Ensure SeenAt and NextLocation are not identical
        if (seenAt != nextLocation)
        {
            incorrectClues.Add(new Clue
            {
                SeenAt = seenAt,
                NextLocation = nextLocation,
                Time = times[Random.Range(0, times.Length)]
            });
        }
        else
        {
            // Skip this iteration to avoid adding invalid clues
            i--;
        }
    }

    return incorrectClues;
}
    private List<Clue> GenerateRandomClues(int count)
    {
        List<Clue> randomClues = new List<Clue>();

        for (int i = 0; i < count; i++)
        {
            Location seenAt = GetRandomLocation();
            Location nextLocation;

            // Ensure the start and end are not the same
            do
            {
                nextLocation = GetRandomLocation();
            } while (nextLocation == seenAt);

            randomClues.Add(new Clue
            {
                SeenAt = seenAt,
                NextLocation = nextLocation,
                Time = times[Random.Range(0, times.Length)]
            });
        }

        return randomClues;
    }

    private Location GetRandomLocation()
    {
        var locations = new List<Location>(mapConnections.Keys);
        return locations[Random.Range(0, locations.Count)];
    }

    private Location GetRandomLocationExcluding(List<Location> exclusions)
    {
        var locations = new List<Location>(mapConnections.Keys);
        var filtered = locations.FindAll(loc => !exclusions.Contains(loc));
        return filtered[Random.Range(0, filtered.Count)];
    }

    private Location GetRandomConnectedLocation(Location current)
    {
        if (mapConnections.ContainsKey(current))
        {
            var neighbors = mapConnections[current];
            return neighbors[Random.Range(0, neighbors.Count)];
        }
        return current; // Fallback to the same location if no connections are found
    }
}