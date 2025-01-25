using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab; // Villager prefab
    public Transform[] spawnPoints; // Spawn points for villagers
    public List<Location> locations; // Available locations for clues
    public string[] times = { "Morning", "Late Morning", "Noon", "Afternoon", "Evening" }; // Time options

    void Start()
    {
        InitializeLocations();
        InitializeVillagers();
    }

    /// <summary>
    /// Initializes the list of locations.
    /// </summary>
    private void InitializeLocations()
    {
        if (locations == null || locations.Count == 0)
        {
            locations = new List<Location>((Location[])System.Enum.GetValues(typeof(Location)));
            Debug.Log($"Initialized locations: {locations.Count} locations available.");
        }
    }

    /// <summary>
    /// Initializes villagers and assigns unique clues.
    /// </summary>
    private void InitializeVillagers()
    {
        if (spawnPoints.Length == 0 || villagerPrefab == null)
        {
            Debug.LogError("VillagerManager: No spawn points or villager prefab assigned.");
            return;
        }

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject villager = Instantiate(villagerPrefab, spawnPoints[i].position, Quaternion.identity);
            Villager villagerScript = villager.GetComponent<Villager>();

            if (villagerScript != null)
            {
                Clue clue = GenerateUniqueClue();
                villagerScript.AssignClue(clue);
            }
        }
    }

    /// <summary>
    /// Generates a unique clue for a villager.
    /// </summary>
    /// <returns>A unique Clue object.</returns>
    private Clue GenerateUniqueClue()
    {
        if (locations == null || locations.Count == 0)
        {
            Debug.LogError("VillagerManager: Locations list is empty. Cannot generate clues.");
            return null;
        }

        string time = times[Random.Range(0, times.Length)];
        Location seenAt = locations[Random.Range(0, locations.Count)];
        Location nextLocation = locations[Random.Range(0, locations.Count)];

        return new Clue
        {
            Time = time,
            SeenAt = seenAt,
            NextLocation = nextLocation
        };
    }
}
