using UnityEngine;
using TMPro; // Required for TextMeshPro

public class InstructionManager : MonoBehaviour
{
    public TextMeshProUGUI helperText; // Reference to the TMP Text component of the Helper Canvas
    private bool interactedOnce = false; // Tracks if the master villager was interacted with
    private bool puzzleTriggered = false; // Tracks if the puzzle should be shown
    private bool gameWon = false; // Tracks if the game has been won
    private bool gameLost = false; // Tracks if the game has been lost

    void Start()
    {
        // Initial instruction
        helperText.text = "Use WASD to move and SPACE to interact. Your mission: speak with the crying cat.";
    }

    public void InteractWithMasterVillager()
    {
        if (!interactedOnce)
        {
            // First interaction with master villager
            helperText.text = "A kitten wandered into the village. Gather clues from villagers about when and where it was last seen, and where it went next. Press H for Help.";
            interactedOnce = true;
        }
        else if (!puzzleTriggered && !gameWon && !gameLost)
        {
            // Second interaction, prepare to show puzzle
            helperText.text = "Where is the kitten?";
            puzzleTriggered = true; // Mark that the puzzle can now be shown
        }
    }

    public bool ShouldShowPuzzle()
    {
        return puzzleTriggered; // Returns true only when the puzzle should be shown
    }

    public void GameWon()
    {
        gameWon = true;
        helperText.text = "You found the kitten! Press R to restart.";
    }

    public void GameLost()
    {
        gameLost = true;
        helperText.text = "You lost. Press R to restart.";
    }

    void Update()
    {
        // Handle restart when the game ends
        if (gameWon || gameLost)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        // Reload the scene (requires using UnityEngine.SceneManagement)
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
