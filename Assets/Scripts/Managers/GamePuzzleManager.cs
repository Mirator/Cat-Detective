using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class GamePuzzleManager : MonoBehaviour
{
    [Header("Path Settings")]
    public Location startLocation; // Assign via Inspector
    public Location endLocation; // Assign via Inspector
    public int numberOfVillagers = 5; // Default: 5, adjustable in Inspector

    [Header("UI References")]
    public GameObject puzzleUI; // Reference to the PuzzleUI Canvas
    public Transform buttonContainer; // Parent object for dynamically created buttons
    public GameObject buttonPrefab; // Prefab for a UI button
    public TextMeshProUGUI headerText; // Header text for the UI

    private PathManager pathManager; // Initialized manually
    private MapManager mapManager; // Dependency for PathManager
    private List<Location> generatedPath; // Stores the generated path
    private int currentPathIndex = 0; // Tracks player progress along the path

    [Header("UI Settings")]
    public GameObject winScreen; // Optional win screen
    public GameObject loseScreen; // Optional lose screen

    void Start()
    {
        InitializeDependencies();

        if (startLocation.Equals(default(Location)) || endLocation.Equals(default(Location)))
        {
            Debug.LogError("Start or End location is not properly assigned.");
            return;
        }

        // Generate the path
        generatedPath = pathManager.GeneratePath(startLocation, endLocation, numberOfVillagers);
        if (generatedPath == null || generatedPath.Count == 0)
        {
            Debug.LogError("Failed to generate a valid path.");
            return;
        }

        Debug.Log($"Generated path: {string.Join(" -> ", generatedPath)}");
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
                //Debug.Log($"Button generated for location: {location}");
            }
            else
            {
                Debug.LogError($"PuzzleManager: No Button component found in {buttonPrefab.name} prefab!");
            }
        }
    }
    private void InitializeDependencies()
    {
        // Initialize MapManager
        mapManager = new MapManager();

        // Initialize PathManager with MapManager as a dependency
        pathManager = new PathManager(mapManager);

        Debug.Log("Dependencies initialized: MapManager and PathManager.");
    }

    public void SelectLocation(Location selectedLocation)
    {
        if (selectedLocation == generatedPath[currentPathIndex])
        {
            Debug.Log($"Correct location selected: {selectedLocation}");
            currentPathIndex++;

            if (currentPathIndex >= generatedPath.Count)
            {
                WinGame();
            }
        }
        else
        {
            Debug.Log($"Incorrect location selected: {selectedLocation}");
            LoseGame();
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
    public void WinGame()
    {
        Debug.Log("You Win!");
        ShowEndGameMessage("You Win!");
        if (winScreen != null) winScreen.SetActive(true);
    }

    public void LoseGame()
    {
        Debug.Log("Game Over!");
        ShowEndGameMessage("Game Over!");
        if (loseScreen != null) loseScreen.SetActive(true);
    }

    private void ShowEndGameMessage(string message)
    {
        Debug.Log(message);
        // Add Unity UI logic here if needed
    }
}
