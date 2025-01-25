using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab; // Prefab for villagers
    public Transform[] spawnPoints; // Spawn points for villagers
    public string[] times = { "Morning", "Late Morning", "Noon", "Afternoon", "Evening" }; // Available times
    public List<Location> locations; // List of locations in the game

    private FinalLocationManager finalLocationManager;
    private PuzzleManager puzzleManager; // Reference to PuzzleManager

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

        // Find PuzzleManager
        puzzleManager = FindFirstObjectByType<PuzzleManager>();
        if (puzzleManager == null)
        {
            Debug.LogError("PuzzleManager not found in the scene!");
            return;
        }

        // Set the final location in both managers
        finalLocationManager.SetFinalLocation(locations);
        Location finalLocation = finalLocationManager.GetFinalLocation();
        puzzleManager.SetFinalLocation(finalLocation);

        // Build the map and generate clues
        Graph<Location> map = BuildGraph();
        GenerateAndAssignClues(finalLocation, map);
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
    /// Builds a graph representation of the map.
    /// </summary>
    /// <returns>The graph representing the map.</returns>
    private Graph<Location> BuildGraph()
    {
        Graph<Location> map = new Graph<Location>();

        // Define map connections
        map.AddEdge(Location.Garden, Location.Bakery);
        map.AddEdge(Location.Bakery, Location.Treehouse);
        map.AddEdge(Location.Bakery, Location.Market);
        map.AddEdge(Location.Treehouse, Location.Barn);
        map.AddEdge(Location.Market, Location.Barn);
        map.AddEdge(Location.Market, Location.Riverbank);

        return map;
    }

    /// <summary>
    /// Generates and assigns clues to villagers.
    /// </summary>
    /// <param name="finalLocation">The final location for the correct path.</param>
    /// <param name="map">The graph representing the map.</param>
    private void GenerateAndAssignClues(Location finalLocation, Graph<Location> map)
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

        if (spawnPoints.Length > clues.Count)
        {
            Debug.LogWarning("Not enough clues for all villagers. Some villagers may not have clues.");
        }

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
