using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePuzzleManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject puzzleUI; // Reference to the PuzzleUI Canvas
    public Transform buttonContainer; // Parent object for dynamically created buttons
    public GameObject buttonPrefab; // Prefab for a UI button with an Image component

    private Location correctFinalLocation; // Correct final location from FinalLocationManager
    private HelperManager helperManager; // Reference to the HelperManager

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

        // Get reference to the HelperManager
        helperManager = Object.FindFirstObjectByType<HelperManager>();
        if (helperManager == null)
        {
            Debug.LogError("HelperManager not found in the scene!");
            return;
        }

        // Generate location buttons and hide the UI initially
        GenerateLocationButtons();
        puzzleUI.SetActive(false);
    }

    /// <summary>
    /// Dynamically generates buttons for each location in the Location enum.
    /// </summary>
    private void GenerateLocationButtons()
    {
        // Clear existing buttons before generating new ones
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

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

            // Get Button, Image, and TextMeshProUGUI components
            Button buttonComponent = newButton.GetComponent<Button>();
            Image iconImage = newButton.GetComponentInChildren<Image>();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            // Assign location icon
            Sprite locationIcon = ClueIconManager.GetIconForLocation(location);
            if (locationIcon != null && iconImage != null)
            {
                iconImage.sprite = locationIcon; // Set button icon
            }
            else
            {
                Debug.LogWarning($"No icon found for location: {location}");
            }

            // Optionally hide the text label if not needed
            if (buttonText != null)
            {
                buttonText.text = ""; // Clear text label
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
            helperManager.GameWon(); // Update helper text for win
        }
        else
        {
            Debug.Log($"Incorrect location selected: {selectedLocation}. The correct location was: {correctFinalLocation}");
            Debug.Log("Game Over!"); // Lose message
            helperManager.GameLost(); // Update helper text for loss
        }

        // Hide puzzle UI after selection
        puzzleUI.SetActive(false);
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

        if (!helperManager.ShouldShowPuzzle())
        {
            helperManager.InteractWithMasterVillager();
        }
        else
        {
            // Clear existing buttons to avoid duplication
            foreach (Transform child in buttonContainer)
            {
                Destroy(child.gameObject);
            }

            // Regenerate the buttons
            GenerateLocationButtons();

            puzzleUI.SetActive(true);
        }
    }
}
