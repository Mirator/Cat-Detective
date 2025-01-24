using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Location correctLocation;
    public Location[] allLocations = { Location.Forest, Location.Cave, Location.Village, Location.Mountain, Location.River };
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    public void InteractWithMasterVillager()
    {
        Debug.Log("Choose a location:");
        for (int i = 0; i < allLocations.Length; i++)
        {
            Debug.Log((i + 1) + ". " + allLocations[i]);
        }

        // Optionally, trigger a UI here for the player to select a location
    }

    public void SelectLocation(int index)
    {
        if (index < 0 || index >= allLocations.Length)
        {
            Debug.LogError("Invalid location selection.");
            return;
        }

        Location selectedLocation = allLocations[index];
        Debug.Log("You selected: " + selectedLocation);

        if (selectedLocation == correctLocation)
        {
            gameManager.WinGame();
        }
        else
        {
            gameManager.LoseGame();
        }
    }
}
