using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GamePuzzleManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject puzzleUI; // Reference to the PuzzleUI Canvas
    public Transform buttonContainer; // Parent object for dynamically created buttons
    public GameObject buttonPrefab; // Prefab for a UI button
    public TextMeshProUGUI headerText; // Header text for the UI

    private List<Location> generatedPath; // Stores the generated path
    private int currentPathIndex = 0; // Tracks player progress along the path

    /// <summary>
    /// Public property to expose the generated path for debugging or interaction.
    /// </summary>
    public List<Location> GeneratedPath => generatedPath;

    /// <summary>
    /// Initializes the GamePuzzleManager with a path.
    /// </summary>
    /// <param name="path">The path to use for the puzzle.</param>
    public void Initialize(List<Location> path)
    {
        if (path == null || path.Count == 0)
        {
            Debug.LogError("Failed to initialize GamePuzzleManager: path is null or empty!");
            return;
        }

        generatedPath = path;
        Debug.Log($"GamePuzzleManager initialized with path: {string.Join(" -> ", generatedPath)}");

        // Generate location buttons and hide the UI initially
        GenerateLocationButtons();
        puzzleUI.SetActive(false);
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
            }
            else
            {
                Debug.LogError($"PuzzleManager: No Button component found in {buttonPrefab.name} prefab!");
            }
        }
    }

    /// <summary>
    /// Handles location selection by the player.
    /// </summary>
    public void SelectLocation(Location selectedLocation)
    {
        if (generatedPath == null || generatedPath.Count == 0)
        {
            Debug.LogError("GamePuzzleManager is not initialized!");
            return;
        }

        if (selectedLocation == generatedPath[currentPathIndex])
        {
            Debug.Log($"Correct location selected: {selectedLocation}");
            currentPathIndex++;

            if (currentPathIndex >= generatedPath.Count)
            {
                Debug.Log("You Win!"); // Win message
            }
        }
        else
        {
            Debug.Log($"Incorrect location selected: {selectedLocation}. The correct location was: {generatedPath[currentPathIndex]}");
            Debug.Log("Game Over!"); // Lose message
        }
    }

    /// <summary>
    /// Displays the Puzzle UI for selecting a location.
    /// </summary>
    public void InteractWithMasterVillager()
    {
        if (generatedPath == null || generatedPath.Count == 0)
        {
            Debug.LogError("GamePuzzleManager is not initialized!");
            return;
        }
        puzzleUI.SetActive(true);
        headerText.text = "Choose a Location:"; // Update the header text
    }
}
