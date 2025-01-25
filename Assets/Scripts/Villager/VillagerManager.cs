using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab; // Prefab for villagers
    public Transform[] spawnPoints; // Spawn points for villagers
    public string[] times = { "Morning", "Late Morning", "Noon", "Afternoon", "Evening" }; // Available times
    public List<Location> locations; // List of all locations in the game

    private FinalLocationManager finalLocationManager;
    private Graph<Location> map;

    void Start()
    {
        InitializeLocations();

        // Create the map
        map = MapManager.CreateMap();

        finalLocationManager = FindFirstObjectByType<FinalLocationManager>();
        if (finalLocationManager == null)
        {
            Debug.LogError("FinalLocationManager not found in the scene!");
            return;
        }

        finalLocationManager.SetFinalLocation(locations);
        Location finalLocation = finalLocationManager.GetFinalLocation();

        GenerateAndAssignClues(finalLocation);
    }

    /// <summary>
    /// Initializes the list of locations.
    /// </summary>
    private void InitializeLocations()
    {
        if (locations == null || locations.Count == 0)
        {
            locations = new List<Location>((Location[])System.Enum.GetValues(typeof(Location)));
            Debug.Log($"Initialized {locations.Count} locations.");
        }
    }

    /// <summary>
    /// Generates and assigns clues to villagers.
    /// </summary>
    private void GenerateAndAssignClues(Location finalLocation)
    {
        if (villagerPrefab == null)
        {
            Debug.LogError("VillagerManager: villagerPrefab is not assigned.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("VillagerManager: No spawn points assigned.");
            return;
        }

        ClueGenerator clueGenerator = new ClueGenerator(map, times, spawnPoints.Length, finalLocation);
        List<Clue> clues = clueGenerator.GenerateClues();

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
