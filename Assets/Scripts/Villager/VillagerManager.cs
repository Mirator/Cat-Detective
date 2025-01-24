using UnityEngine;

public class VillagerManager : MonoBehaviour
{
    public GameObject villagerPrefab;
    public Transform[] spawnPoints; // Positions to spawn villagers
    private string[] clues = { "Clue 1", "Clue 2", "Clue 3" }; // Example clues

    void Start()
    {
        InitializeVillagers();
    }

    void InitializeVillagers()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            GameObject villager = Instantiate(villagerPrefab, spawnPoints[i].position, Quaternion.identity);
            Villager villagerScript = villager.GetComponent<Villager>();

            if (villagerScript != null)
            {
                villagerScript.AssignClue(clues[i % clues.Length]); // Assign a clue
            }
        }
    }
}
