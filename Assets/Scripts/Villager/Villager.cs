using UnityEngine;

public class Villager : MonoBehaviour
{
    public string clueText; // The clue assigned to this villager

    public void AssignClue(string clue)
    {
        clueText = clue;
    }

    public string GetClue()
    {
        return clueText;
    }
}
