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
        //Debug.Log($"Final location already set to: {finalLocation}. Skipping.");
        return;
    }

    // Randomly select a final location that is different from the start
    Location newFinalLocation;
    do
    {
        newFinalLocation = locations[Random.Range(0, locations.Count)];
    } while (newFinalLocation == Location.Garden);

    finalLocation = newFinalLocation;
    //Debug.Log($"Final location set: {finalLocation}");
}


    /// <summary>
    /// Gets the final location.
    /// </summary>
    public Location GetFinalLocation()
    {
        return finalLocation;
    }
}
