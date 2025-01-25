using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    public Transform[] spawnPoints;
    public string[] times = { "Morning", "Late Morning", "Noon", "Afternoon", "Evening" };
    public List<Location> locations;

    private Dictionary<Location, List<Location>> mapConnections;

    void Start()
    {
        InitializeMapConnections();
        InitializeLocations();
        GenerateAndAssignClues();
    }

    /// <summary>
    /// Initializes the map connections.
    /// </summary>
    private void InitializeMapConnections()
    {
        if (mapConnections != null && mapConnections.Count > 0)
        {
            return; // Already initialized
        }

        mapConnections = new Dictionary<Location, List<Location>>
        {
            { Location.Garden, new List<Location> { Location.Bakery } },
            { Location.Bakery, new List<Location> { Location.Garden, Location.Treehouse, Location.Market } },
            { Location.Treehouse, new List<Location> { Location.Bakery, Location.Barn } },
            { Location.Market, new List<Location> { Location.Bakery, Location.Barn, Location.Riverbank } },
            { Location.Barn, new List<Location> { Location.Market, Location.Treehouse } },
            { Location.Riverbank, new List<Location> { Location.Market } }
        };

        Debug.Log("Initialized map connections.");
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
    private void GenerateAndAssignClues()
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

        ClueGenerator clueGenerator = new ClueGenerator(locations, times, mapConnections, spawnPoints.Length);
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
