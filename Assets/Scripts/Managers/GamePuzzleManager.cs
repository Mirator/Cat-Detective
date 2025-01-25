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

    private Location correctFinalLocation; // Correct final location from FinalLocationManager

    /// <summary>
    /// Initializes the GamePuzzleManager and fetches the final location.
    /// </summary>
    public void Initialize()
    {
        // Fetch the final location from FinalLocationManager
        FinalLocationManager finalLocationManager = Object.FindFirstObjectByType<FinalLocationManager>();
        if (finalLocationManager == null)
        {
            Debug.LogError("FinalLocationManager not found in the scene!");
            return;
        }

        correctFinalLocation = finalLocationManager.GetFinalLocation();

        if (correctFinalLocation == default)
        {
            Debug.LogError("Correct final location is not set!");
            return;
        }

        Debug.Log($"GamePuzzleManager initialized. Correct final location: {correctFinalLocation}");

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
        if (selectedLocation == correctFinalLocation)
        {
            Debug.Log("You Win! Correct final location reached.");
        }
        else
        {
            Debug.Log($"Incorrect location selected: {selectedLocation}. The correct location was: {correctFinalLocation}");
            Debug.Log("Game Over!"); // Lose message
        }
    }

    /// <summary>
    /// Displays the Puzzle UI for selecting a location.
    /// </summary>
    public void InteractWithMasterVillager()
    {
        if (correctFinalLocation == default)
        {
            Debug.LogError("GamePuzzleManager is not initialized!");
            return;
        }
        puzzleUI.SetActive(true);
        headerText.text = "Choose a Location:"; // Update the header text
    }
}