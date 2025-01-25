using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public GameObject puzzleUI;
    public Transform buttonContainer;
    public GameObject buttonPrefab;
    public TextMeshProUGUI headerText;

    private GameManager gameManager;
    private Location finalLocation;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        puzzleUI.SetActive(false); // Ensure UI is initially hidden
    }

    public void SetFinalLocation(Location location)
    {
        finalLocation = location;
        Debug.Log($"Final location set in PuzzleManager: {finalLocation}");
    }

    private void GenerateLocationButtons()
    {
        Location[] allLocations = (Location[])System.Enum.GetValues(typeof(Location));

        foreach (Location location in allLocations)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            Button buttonComponent = newButton.GetComponent<Button>();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = location.ToString();

            buttonComponent.onClick.AddListener(() => SelectLocation(location));
        }
    }

    public void InteractWithMasterVillager()
    {
        puzzleUI.SetActive(true);
        headerText.text = "Choose a Location:";
    }

    public void SelectLocation(Location location)
    {
        puzzleUI.SetActive(false);

        if (location == finalLocation)
        {
            gameManager.WinGame();
        }
        else
        {
            gameManager.LoseGame();
        }
    }
}
