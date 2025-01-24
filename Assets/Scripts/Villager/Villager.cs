using UnityEngine;

public class Villager : MonoBehaviour
{
    [Header("Clue Settings")]
    public string clueText; // The clue assigned to this villager
    public GameObject bubblePrefab; // Prefab for this villager's bubble

    [Header("Highlight Settings")]
    public Color highlightColor = Color.yellow; // Highlight color
    private Color originalColor; // Original color of the sprite
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void AssignClue(string clue)
    {
        clueText = clue;
    }

    public string GetClue()
    {
        return clueText;
    }

    public void Highlight()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlightColor;
        }
    }

    public void RevertHighlight()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting with villager. Clue: " + clueText);
    }
}
