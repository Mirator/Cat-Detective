using System.Collections.Generic;
using UnityEngine;

public class ClueDisplayManager : MonoBehaviour
{
    public GameObject cluesPanel; // Reference to the CluesPanel in the canvas
    public GameObject clueItemPrefab; // Prefab for clue display items

    private List<string> discoveredClues = new List<string>(); // List of clues already discovered

    public void AddClue(string clueText, Sprite clueIcon)
    {
        if (discoveredClues.Contains(clueText)) return; // Avoid duplicates

        discoveredClues.Add(clueText);

        // Create a new clue item
        GameObject clueItem = Instantiate(clueItemPrefab, cluesPanel.transform);
        ClueItem clueItemScript = clueItem.GetComponent<ClueItem>();

        if (clueItemScript != null)
        {
            clueItemScript.SetClue(clueIcon, clueText);
        }
    }
}
