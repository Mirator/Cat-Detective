using System.Collections.Generic;
using UnityEngine;

public class ClueGenerator
{
    private Graph<Location> map;
    private string[] times;
    private int numberOfVillagers;
    private Location finalLocation;

    private PathManager pathManager;
    private ClueFactory clueFactory;

    public ClueGenerator(Graph<Location> map, string[] times, int numberOfVillagers, Location finalLocation)
    {
        this.map = map;
        this.times = times;
        this.numberOfVillagers = numberOfVillagers;
        this.finalLocation = finalLocation;

        pathManager = new PathManager(map, finalLocation);
        clueFactory = new ClueFactory(map, times, pathManager);
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

        // Ensure at least one incorrect clue
        if (incorrectClueCount == 0)
        {
            incorrectClueCount = 1;
            if (randomClueCount > 0) randomClueCount--;
            else if (correctClueCount > 1) correctClueCount--;
        }

        Debug.Log($"Clues: {correctClueCount} correct, {incorrectClueCount} incorrect, {randomClueCount} random");

        // Generate clues
        clues.AddRange(clueFactory.GenerateCorrectClues(correctClueCount));
        clues.AddRange(clueFactory.GenerateIncorrectClues(incorrectClueCount));
        clues.AddRange(clueFactory.GenerateRandomClues(randomClueCount));

        return clues;
    }
}
