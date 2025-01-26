using UnityEngine;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Reference to the TMP Text component of the Instruction Canvas
    public SpriteRenderer masterVillagerSprite; // Reference to Master Villager's SpriteRenderer
    public Sprite happyCatSprite; // Sprite for the happy cat

    private bool interactedOnce = false; // Tracks if the master villager was interacted with
    private bool puzzleTriggered = false;
    private bool gameWon = false; // Tracks if the game has been won
    private bool gameLost = false; // Tracks if the game has been lost

    void Start()
    {
        instructionText.text = "Use WASD to move and SPACE to interact. Your mission: speak with the crying cat.";
    }

    public void InteractWithMasterVillager()
    {
        if (!interactedOnce)
        {
            instructionText.text = "A kitten wandered into the inn. Ask around for clues and return once you’ve gathered information.";
            interactedOnce = true;
        }
        else if (!puzzleTriggered && !gameWon && !gameLost)
        {
            instructionText.text = "Where is the kitten?";
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

        // Update instruction text
        instructionText.text = "You found the kitten! Press R to restart.";

        // Change the Master Villager's sprite to the happy cat
        if (masterVillagerSprite != null && happyCatSprite != null)
        {
            masterVillagerSprite.sprite = happyCatSprite;
        }
        else
        {
            Debug.LogError("Master Villager Sprite or Happy Cat Sprite is not assigned.");
        }
    }

    public void GameLost()
    {
        gameLost = true;
        instructionText.text = "You lost. Press R to restart.";
    }


    void Update()
    {
        if ((gameWon || gameLost) && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
