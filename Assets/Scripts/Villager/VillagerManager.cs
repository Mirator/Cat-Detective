using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab; // Villager prefab
    public Transform[] spawnPoints; // Positions to spawn villagers
    public List<Clue> availableClues; // List of pre-defined clues

    void Start()
    {
        InitializeVillagers();
    }

    /// <summary>
    /// Initializes villagers and assigns clues.
    /// </summary>
    void InitializeVillagers()
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

            if (villagerScript != null && i < availableClues.Count)
            {
                villagerScript.AssignClue(availableClues[i]);
            }
        }
    }
}
