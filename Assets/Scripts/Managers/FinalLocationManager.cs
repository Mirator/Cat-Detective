using System.Collections.Generic;
using UnityEngine;

public class FinalLocationManager : MonoBehaviour
{
    private Location finalLocation;

    public void SetFinalLocation(List<Location> locations)
    {
        finalLocation = locations[locations.Count - 1]; // Set the last location in the sequence
        Debug.Log($"Final location set: {finalLocation}");
    }

    public Location GetFinalLocation()
    {
        return finalLocation;
    }
}
