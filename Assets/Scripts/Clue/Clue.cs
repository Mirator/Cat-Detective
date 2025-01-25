using UnityEngine;

[System.Serializable]
public class Clue
{
    public TimeOfDay Time { get; set; } // Use TimeOfDay enum
    public Location SeenAt { get; set; }
    public Location NextLocation { get; set; }
}
