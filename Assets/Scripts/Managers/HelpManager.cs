using UnityEngine;

public class HelpManager : MonoBehaviour
{
    public GameObject helpBubble; // Reference to the help bubble GameObject

    private bool isHelpVisible = false; // Tracks the visibility of the help bubble

    void Start()
    {
        if (helpBubble != null)
        {
            helpBubble.SetActive(false); // Ensure the help bubble is hidden initially
        }
    }

    /// <summary>
    /// Toggles the visibility of the help bubble.
    /// </summary>
    public void ToggleHelp()
    {
        if (helpBubble == null)
        {
            Debug.LogError("HelpBubble is not assigned in HelpManager.");
            return;
        }

        isHelpVisible = !isHelpVisible;
        helpBubble.SetActive(isHelpVisible);
    }
}
