using UnityEngine;

public class Villager : MonoBehaviour
{
    [Header("Clue Settings")]
    public Clue assignedClue; // Clue assigned to this villager

    [Header("Highlight Settings")]
    public Color highlightColor = Color.yellow; // Highlight color
    private Color originalColor; // Original color of the villager
    private SpriteRenderer spriteRenderer;

    [Header("UI Settings")]
    public GameObject bubblePrefab; // Bubble prefab for clue display

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    /// <summary>
    /// Assigns a clue to the villager.
    /// </summary>
    public void AssignClue(Clue clue)
    {
        assignedClue = clue;
    }

    /// <summary>
    /// Returns the details of the assigned clue.
    /// </summary>
    public string GetClueDetails()
    {
        if (assignedClue == null)
        {
            return "No clue assigned to this villager.";
        }

        return $"Time: {assignedClue.Time}, Seen At: {assignedClue.SeenAt}, Next Location: {assignedClue.NextLocation}";
    }

    /// <summary>
    /// Highlights the villager when the player is nearby.
    /// </summary>
    public void Highlight()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor;
        }
    }

    /// <summary>
    /// Reverts the villager's color to its original state.
    /// </summary>
    public void RevertHighlight()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    /// <summary>
    /// Handles interaction with the villager.
    /// </summary>
    public void Interact()
    {
        if (assignedClue != null)
        {
            //Debug.Log($"Clue from villager: {GetClueDetails()}");
        }
        else
        {
            Debug.Log("This villager has no clue to provide.");
        }
    }
}
