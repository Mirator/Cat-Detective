using UnityEngine;
using TMPro; // Use Unity's TextMeshPro for better text handling

public class Bubble : MonoBehaviour
{
    public TextMeshProUGUI clueText;

    public void SetClue(string clue)
    {
        clueText.text = clue;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position + new Vector3(0, 1.5f, 0); // Offset above the villager
    }
}
