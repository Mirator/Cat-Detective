using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab; // Prefab for villagers
    public Transform[] spawnPoints; // Spawn points for villagers
    public TimeOfDay[] times = { TimeOfDay.Morning, TimeOfDay.Noon, TimeOfDay.Evening }; // Available times
    public List<Location> locations; // List of locations in the game

    private FinalLocationManager finalLocationManager;
    private MapManager mapManager;
    private PathManager pathManager;
    private List<Location> validatedPath; // Store the generated path

    void Start()
    {
        InitializeLocations();

        // Find FinalLocationManager
        finalLocationManager = FindFirstObjectByType<FinalLocationManager>();
        if (finalLocationManager == null)
        {
            Debug.LogError("FinalLocationManager not found in the scene!");
            return;
        }

        // Set the final location
        finalLocationManager.SetFinalLocation(locations);
        Location finalLocation = finalLocationManager.GetFinalLocation();

        // Initialize MapManager and PathManager
        mapManager = new MapManager();
        pathManager = new PathManager(mapManager);

        // Generate the path
        int numberOfVillagers = spawnPoints.Length; // Total number of villagers
        validatedPath = pathManager.GeneratePath(Location.Garden, finalLocation, numberOfVillagers);

        if (validatedPath == null || validatedPath.Count == 0)
        {
            Debug.LogError("Failed to generate a valid path.");
            return;
        }

        // Generate clues using ClueGenerator
        ClueGenerator clueGenerator = new ClueGenerator(mapManager, mapManager.GetConnections(), validatedPath, times);
        List<Clue> clues = clueGenerator.GenerateClues(
            correctClueCount: 3,
            incorrectClueCount: 0,
            randomClueCount: 3
        );

        GenerateAndAssignClues(clues);
    }

    private void InitializeLocations()
    {
        if (locations == null || locations.Count == 0)
        {
            locations = new List<Location>((Location[])System.Enum.GetValues(typeof(Location)));
        }
    }

    private void GenerateAndAssignClues(List<Clue> clues)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject villager = Instantiate(villagerPrefab, spawnPoints[i].position, Quaternion.identity);
            Villager villagerScript = villager.GetComponent<Villager>();

            if (villagerScript != null && i < clues.Count)
            {
                villagerScript.AssignClue(clues[i]);
            }
        }
    }

    /// <summary>
    /// Expose the generated path for other managers to use.
    /// </summary>
    public List<Location> GetPath()
    {
        return validatedPath;
    }
}
