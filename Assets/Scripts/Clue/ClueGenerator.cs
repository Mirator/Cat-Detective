using System.Collections.Generic;
using UnityEngine;

public class ClueGenerator
{
    private MapManager mapManager;
    private List<Location> correctPath;
    private string[] times;

    public ClueGenerator(MapManager mapManager, List<Location> correctPath, string[] times)
    {
        this.mapManager = mapManager;
        this.correctPath = correctPath;
        this.times = times;
    }

    public List<Clue> GenerateClues(int correctClueCount, int incorrectClueCount, int randomClueCount)
    {
        List<Clue> clues = new List<Clue>();

        // Generate correct clues
        for (int i = 0; i < correctClueCount && i < correctPath.Count - 1; i++)
        {
            clues.Add(new Clue
            {
                SeenAt = correctPath[i],
                NextLocation = correctPath[i + 1],
                Time = times[Random.Range(0, times.Length)]
            });
        }

        // Generate incorrect clues
        for (int i = 0; i < incorrectClueCount; i++)
        {
            Location seenAt = GetRandomLocationExcluding(correctPath);
            Location nextLocation = GetRandomConnectedLocation(seenAt);

            clues.Add(new Clue
            {
                SeenAt = seenAt,
                NextLocation = nextLocation,
                Time = times[Random.Range(0, times.Length)]
            });
        }

        // Generate random clues
        for (int i = 0; i < randomClueCount; i++)
        {
            Location seenAt = GetRandomLocation();
            Location nextLocation = GetRandomLocation();

            clues.Add(new Clue
            {
                SeenAt = seenAt,
                NextLocation = nextLocation,
                Time = times[Random.Range(0, times.Length)]
            });
        }

        return clues;
    }

    private Location GetRandomLocation()
    {
        var locations = mapManager.GetAllLocations();
        return locations[Random.Range(0, locations.Count)];
    }

    private Location GetRandomLocationExcluding(List<Location> exclusions)
    {
        var locations = mapManager.GetAllLocations();
        var filtered = locations.FindAll(loc => !exclusions.Contains(loc));
        return filtered[Random.Range(0, filtered.Count)];
    }

    private Location GetRandomConnectedLocation(Location current)
    {
        var neighbors = mapManager.GetNeighbors(current);
        return neighbors[Random.Range(0, neighbors.Count)];
    }
}
