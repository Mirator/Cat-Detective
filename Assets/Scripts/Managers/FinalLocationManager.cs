using System.Collections.Generic;
using UnityEngine;

public class FinalLocationManager : MonoBehaviour
{
    private Location finalLocation;

    /// <summary>
    /// Sets the final location from a list of possible locations.
    /// If the final location is already set, it won't change.
    /// </summary>
    /// <param name="locations">The list of locations to choose from.</param>
    public void SetFinalLocation(List<Location> locations)
    {
        if (finalLocation != default)
        {
            Debug.Log($"Final location is already set to: {finalLocation}. Skipping.");
            return;
        }

        if (locations == null || locations.Count == 0)
        {
            Debug.LogError("No locations available to set as the final location.");
            return;
        }

        // Randomly select the final location
        finalLocation = locations[Random.Range(0, locations.Count)];
        Debug.Log($"Final location set: {finalLocation}");
    }

    /// <summary>
    /// Gets the final location.
    /// </summary>
    public Location GetFinalLocation()
    {
        return finalLocation;
    }
}
