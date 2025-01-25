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
        List<Location> validatedPath = pathManager.GeneratePath(Location.Garden, finalLocation, numberOfVillagers);

        if (validatedPath == null || validatedPath.Count == 0)
        {
            Debug.LogError("Failed to generate a valid path.");
            return;
        }

        // Generate clues using ClueGenerator
        ClueGenerator clueGenerator = new ClueGenerator(mapManager, mapManager.GetConnections(), validatedPath, times);
        List<Clue> clues = clueGenerator.GenerateClues(
            correctClueCount: Mathf.Max(3, validatedPath.Count - 1), // Ensure sufficient correct clues
            incorrectClueCount: Mathf.Clamp(numberOfVillagers / 2 - 1, 1, numberOfVillagers - 1),
            randomClueCount: Mathf.Clamp(numberOfVillagers / 4, 0, numberOfVillagers - 1)
        );

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
                foreach (var clue in clues)
                {
                    Debug.Log($"[VillagerManager] Clue to assign: Time={clue.Time}, SeenAt={clue.SeenAt}, NextLocation={clue.NextLocation}");
                }
                villagerScript.AssignClue(clues[i]);
                Debug.Log($"Villager spawned at {spawnPoints[i].position} with clue: {clues[i].Time}, {clues[i].SeenAt}, {clues[i].NextLocation}");
            }
        }
    }
}
