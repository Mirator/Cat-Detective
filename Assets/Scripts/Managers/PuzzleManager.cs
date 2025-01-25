using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject puzzleUI; // Reference to the PuzzleUI Canvas
    public Transform buttonContainer; // Parent object for dynamically created buttons
    public GameObject buttonPrefab; // Prefab for a UI button
    public TextMeshProUGUI headerText; // Header text for the UI

    private GameManager gameManager;
    private Location finalLocation; // The final location to match for a win condition

    void Start()
    {
        // Find GameManager
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        // Generate location buttons and hide the UI initially
        GenerateLocationButtons();
        puzzleUI.SetActive(false);
    }

    /// <summary>
    /// Sets the final location that the player must find.
    /// </summary>
    /// <param name="location">The final location to match for winning the puzzle.</param>
    public void SetFinalLocation(Location location)
    {
        finalLocation = location;
        Debug.Log($"Final location set in PuzzleManager: {finalLocation}");
    }

    /// <summary>
    /// Dynamically generates buttons for each location in the Location enum.
    /// </summary>
    private void GenerateLocationButtons()
    {
        // Get all locations from the Location enum
        Location[] allLocations = (Location[])System.Enum.GetValues(typeof(Location));

        // Ensure the prefab and container are correctly assigned
        if (buttonPrefab == null || buttonContainer == null)
        {
            Debug.LogError("PuzzleManager: Button prefab or container is not assigned!");
            return;
        }

        foreach (Location location in allLocations)
        {
            // Instantiate a new button from the prefab
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);

            // Get Button and TextMeshProUGUI components
            Button buttonComponent = newButton.GetComponent<Button>();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.text = location.ToString(); // Set button label to location name
            }
            else
            {
                Debug.LogError($"PuzzleManager: No TextMeshProUGUI component found in {buttonPrefab.name} prefab!");
            }

            if (buttonComponent != null)
            {
                // Add a listener to handle button clicks
                buttonComponent.onClick.AddListener(() => SelectLocation(location));
                //Debug.Log($"Button generated for location: {location}");
            }
            else
            {
                Debug.LogError($"PuzzleManager: No Button component found in {buttonPrefab.name} prefab!");
            }
        }
    }

    /// <summary>
    /// Displays the Puzzle UI for selecting a location.
    /// </summary>
    public void InteractWithMasterVillager()
    {
        puzzleUI.SetActive(true);
        headerText.text = "Choose a Location:"; // Update the header text
    }

    /// <summary>
    /// Handles player selection of a location.
    /// </summary>
    /// <param name="location">The location selected by the player.</param>
    public void SelectLocation(Location location)
    {
        puzzleUI.SetActive(false); // Hide the UI after selection

        // Check if the selected location matches the final location
        if (location == finalLocation)
        {
            gameManager.WinGame(); // Trigger win condition
        }
        else
        {
            gameManager.LoseGame(); // Trigger lose condition
        }
    }
}
