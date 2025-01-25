using UnityEngine;

[System.Serializable]
public class Clue
{
    public string Time; // Time when the kitten was last seen
    public Location SeenAt; // Location where the kitten was seen
    public Location NextLocation; // Location where the kitten went next
}
