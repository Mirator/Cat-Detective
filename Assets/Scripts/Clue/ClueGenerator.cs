using System;
using UnityEngine;
using System.Collections.Generic;

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
        int timeIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TimeOfDay)).Length);

        for (int i = 0; i < count && i < correctPath.Count - 1; i++)
        {
            TimeOfDay time = (TimeOfDay)timeIndex;
            correctClues.Add(new Clue
            {
                SeenAt = correctPath[i],
                NextLocation = correctPath[i + 1],
                Time = time
            });

            timeIndex = (timeIndex + 1) % Enum.GetValues(typeof(TimeOfDay)).Length;
        }

        return correctClues;
    }

    private List<Clue> GenerateIncorrectClues(int count)
    {
        List<Clue> incorrectClues = new List<Clue>();
        int timeIndex = UnityEngine.Random.Range(0, times.Length);

        for (int i = 0; i < count; i++)
        {
            Location seenAt = GetRandomLocationExcluding(correctPath);
            List<Location> neighbors = mapManager.GetNeighbors(seenAt);

            if (neighbors == null || neighbors.Count == 0)
            {
                Location fallbackLocation;
                do
                {
                    fallbackLocation = GetRandomLocationExcluding(correctPath);
                } while (fallbackLocation == seenAt);

                neighbors = new List<Location> { fallbackLocation };
            }

            Location nextLocation = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];

            if (seenAt != nextLocation)
            {
                incorrectClues.Add(new Clue
                {
                    SeenAt = seenAt,
                    NextLocation = nextLocation,
                    Time = times[timeIndex]
                });

                timeIndex = (timeIndex + 1) % times.Length;
            }
            else
            {
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

            do
            {
                nextLocation = GetRandomLocation();
            } while (nextLocation == seenAt);

            randomClues.Add(new Clue
            {
                SeenAt = seenAt,
                NextLocation = nextLocation,
                Time = times[UnityEngine.Random.Range(0, times.Length)]
            });
        }

        return randomClues;
    }

    private Location GetRandomLocation()
    {
        var locations = new List<Location>(mapConnections.Keys);
        return locations[UnityEngine.Random.Range(0, locations.Count)];
    }

    private Location GetRandomLocationExcluding(List<Location> exclusions)
    {
        var locations = new List<Location>(mapConnections.Keys);
        var filtered = locations.FindAll(loc => !exclusions.Contains(loc));
        return filtered[UnityEngine.Random.Range(0, filtered.Count)];
    }

    private Location GetRandomConnectedLocation(Location current)
    {
        if (mapConnections.ContainsKey(current))
        {
            var neighbors = mapConnections[current];
            return neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
        }
        return current;
    }
}
