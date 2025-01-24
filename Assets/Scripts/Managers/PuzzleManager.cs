using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this namespace for TextMeshPro support

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzleUI; // Reference to the PuzzleUI Canvas
    public Transform buttonContainer; // Parent object for dynamically created buttons
    public GameObject buttonPrefab; // Prefab for a UI button
    public TextMeshProUGUI headerText; // Header text for the UI
    public Location correctLocation; // Correct location to be chosen

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        SetRandomCorrectLocation();
        GenerateLocationButtons();
        puzzleUI.SetActive(false); // Ensure UI is initially hidden
    }

    /// <summary>
    /// Assigns a random location from the enum as the correct location.
    /// </summary>
    private void SetRandomCorrectLocation()
    {
        Location[] allLocations = (Location[])System.Enum.GetValues(typeof(Location));
        correctLocation = allLocations[Random.Range(0, allLocations.Length)];
        Debug.Log("Correct location selected: " + correctLocation);
    }

    /// <summary>
    /// Dynamically generates buttons for each location.
    /// </summary>
    private void GenerateLocationButtons()
    {
        Location[] allLocations = (Location[])System.Enum.GetValues(typeof(Location));

        foreach (Location location in allLocations)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            Button buttonComponent = newButton.GetComponent<Button>();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = location.ToString(); // Set button text to location name

            // Add a listener to handle button clicks
            buttonComponent.onClick.AddListener(() => SelectLocation(location));
        }
    }

    /// <summary>
    /// Shows the Puzzle UI for location selection.
    /// </summary>
    public void InteractWithMasterVillager()
    {
        puzzleUI.SetActive(true); // Show the UI
        headerText.text = "Choose a Location:"; // Update the header text
    }

    /// <summary>
    /// Handles player selection of a location.
    /// </summary>
    /// <param name="location">The chosen location.</param>
    public void SelectLocation(Location location)
    {
        puzzleUI.SetActive(false); // Hide the UI after selection
        Debug.Log("You selected: " + location);

        if (location == correctLocation)
        {
            gameManager.WinGame();
        }
        else
        {
            gameManager.LoseGame();
        }
    }
}
