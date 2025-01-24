using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void WinGame()
    {
        Debug.Log("You Win!");
        // Add logic for showing a win screen or transitioning to the next level
        ShowEndGameMessage("You Win!");
    }

    public void LoseGame()
    {
        Debug.Log("Game Over!");
        // Add logic for showing a lose screen or restarting the game
        ShowEndGameMessage("Game Over!");
    }

    private void ShowEndGameMessage(string message)
    {
        // Example of simple UI feedback
        Debug.Log(message);
        // Optionally, add Unity UI logic here to display on-screen messages
    }
}
