using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab; // Prefab for villagers
    public Transform[] spawnPoints; // Spawn points for villagers
    public string[] times = { "Morning", "Late Morning", "Noon", "Afternoon", "Evening" }; // Available times
    public List<Location> locations; // List of locations in the game

    private FinalLocationManager finalLocationManager;
    private MapManager mapManager;
    private PathManager pathManager;

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

        // Generate the path and clues
        List<Location> validatedPath = pathManager.GeneratePath(Location.Garden, finalLocation);
        if (validatedPath == null || validatedPath.Count == 0)
        {
            Debug.LogError("Failed to generate a valid path.");
            return;
        }

        ClueFactory clueFactory = new ClueFactory(mapManager.GetConnections(), times, validatedPath);
        List<Clue> clues = clueFactory.GenerateClues(correctClueCount: 3, incorrectClueCount: 1, randomClueCount: 2);

        GenerateAndAssignClues(clues);
    }

    private void InitializeLocations()
    {
        if (locations == null || locations.Count == 0)
        {
            locations = new List<Location>((Location[])System.Enum.GetValues(typeof(Location)));
            Debug.Log($"Initialized {locations.Count} locations.");
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
                Debug.Log($"Villager spawned at {spawnPoints[i].position} with clue: {clues[i].Time}, {clues[i].SeenAt}, {clues[i].NextLocation}");
            }
        }
    }
}
