using UnityEngine;

public class VillagerColorChanger : MonoBehaviour
{
    // Updated predefined cat colors
    private readonly Color[] catColors = new Color[]
    {
        new Color(1f, 1f, 1f),       // White
        new Color(0.5f, 0.5f, 0.5f), // Gray
        new Color(1f, 0.65f, 0f),    // Orange
        new Color(1f, 0.87f, 0.68f), // Creamy Yellow
        new Color(0.68f, 0.85f, 1f), // Light Blue
        new Color(0.36f, 0.25f, 0.2f), // Dark Brown
        new Color(1f, 0.84f, 0.2f)   // Golden Yellow
    };

    // Reference to the SpriteRenderer
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Automatically assign a random color when the villager spawns
        SetRandomColor();
    }

    /// <summary>
    /// Sets the cat's color based on the index of predefined colors.
    /// </summary>
    /// <param name="colorIndex">The index of the color to use.</param>
    public void SetColor(int colorIndex)
    {
        // Validate the index and set the color
        if (colorIndex >= 0 && colorIndex < catColors.Length)
        {
            spriteRenderer.color = catColors[colorIndex];
        }
        else
        {
            Debug.LogWarning("Invalid color index. Using default color (white).");
            spriteRenderer.color = catColors[0]; // Default to white
        }
    }

    /// <summary>
    /// Sets a random cat color.
    /// </summary>
    public void SetRandomColor()
    {
        int randomIndex = Random.Range(0, catColors.Length);
        SetColor(randomIndex);
    }
}
