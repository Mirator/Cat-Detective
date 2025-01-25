using System.Collections.Generic;
using UnityEngine;

public class ClueFactory
{
    private Dictionary<Location, List<Location>> mapConnections;
    private string[] times;
    private List<Location> validatedPath;

    public ClueFactory(Dictionary<Location, List<Location>> mapConnections, string[] times, List<Location> validatedPath)
    {
        this.mapConnections = mapConnections;
        this.times = times;
        this.validatedPath = validatedPath;
    }

    /// <summary>
    /// Generates all clues (correct, incorrect, and random).
    /// </summary>
    public List<Clue> GenerateClues(int correctClueCount, int incorrectClueCount, int randomClueCount)
    {
        List<Clue> clues = new List<Clue>();

        clues.AddRange(GenerateCorrectClues(correctClueCount));
        clues.AddRange(GenerateIncorrectClues(incorrectClueCount));
        clues.AddRange(GenerateRandomClues(randomClueCount));

        return clues;
    }

    private List<Clue> GenerateCorrectClues(int count)
    {
        List<Clue> correctClues = new List<Clue>();

        for (int i = 0; i < Mathf.Min(count, validatedPath.Count - 1); i++)
        {
            correctClues.Add(new Clue
            {
                Time = times[Mathf.Clamp(i, 0, times.Length - 1)],
                SeenAt = validatedPath[i],
                NextLocation = validatedPath[i + 1]
            });
        }

        return correctClues;
    }

    private List<Clue> GenerateIncorrectClues(int count)
    {
        List<Clue> incorrectClues = new List<Clue>();

        for (int i = 0; i < count; i++)
        {
            Location seenAt = ClueUtils.GetRandomLocationExcluding(validatedPath, mapConnections);
            Location nextLocation = ClueUtils.GetRandomConnectedLocationExcluding(seenAt, mapConnections, validatedPath);

            incorrectClues.Add(new Clue
            {
                Time = times[Random.Range(0, times.Length)],
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
            randomClues.Add(new Clue
            {
                Time = times[Random.Range(0, times.Length)],
                SeenAt = ClueUtils.GetRandomLocation(mapConnections),
                NextLocation = ClueUtils.GetRandomLocation(mapConnections)
            });
        }

        return randomClues;
    }
}
