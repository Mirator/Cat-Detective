using System.Collections.Generic;
using UnityEngine;

public class ClueFactory
{
    private Graph<Location> map;
    private string[] times;
    private PathManager pathManager;

    public ClueFactory(Graph<Location> map, string[] times, PathManager pathManager)
    {
        this.map = map;
        this.times = times;
        this.pathManager = pathManager;
    }

    public List<Clue> GenerateCorrectClues(int count)
    {
        List<Clue> correctClues = new List<Clue>();
        List<Location> path = pathManager.GetValidatedPath();

        if (path == null || path.Count < 2)
        {
            Debug.LogWarning("No valid path generated for correct clues.");
            return correctClues;
        }

        Debug.Log($"Generating correct clues for path: {string.Join(" -> ", path)}");

        for (int i = 0; i < Mathf.Min(count, path.Count - 1); i++)
        {
            correctClues.Add(new Clue
            {
                Time = times[Mathf.Clamp(i, 0, times.Length - 1)],
                SeenAt = path[i],
                NextLocation = path[i + 1]
            });
        }

        return correctClues;
    }

    public List<Clue> GenerateIncorrectClues(int count)
    {
        List<Clue> incorrectClues = new List<Clue>();
        List<Location> correctPath = pathManager.GetValidatedPath();

        for (int i = 0; i < count; i++)
        {
            Location seenAt = ClueUtils.GetRandomLocationExcluding(map, correctPath);
            Location nextLocation = ClueUtils.GetRandomConnectedLocationExcluding(map, seenAt, correctPath);

            incorrectClues.Add(new Clue
            {
                Time = times[Random.Range(0, times.Length)],
                SeenAt = seenAt,
                NextLocation = nextLocation
            });
        }

        return incorrectClues;
    }

    public List<Clue> GenerateRandomClues(int count)
    {
        List<Clue> randomClues = new List<Clue>();

        for (int i = 0; i < count; i++)
        {
            randomClues.Add(new Clue
            {
                Time = times[Random.Range(0, times.Length)],
                SeenAt = ClueUtils.GetRandomLocation(map),
                NextLocation = ClueUtils.GetRandomLocation(map)
            });
        }

        return randomClues;
    }
}
