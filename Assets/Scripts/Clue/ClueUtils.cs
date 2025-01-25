using System.Collections.Generic;
using UnityEngine;

public static class ClueUtils
{
    /// <summary>
    /// Gets a random location from the map connections.
    /// </summary>
    /// <param name="mapConnections">The map connections dictionary.</param>
    /// <returns>A random location.</returns>
    public static Location GetRandomLocation(Dictionary<Location, List<Location>> mapConnections)
    {
        // Get all locations as a list
        List<Location> allLocations = new List<Location>(mapConnections.Keys);

        // Ensure there are valid locations to pick from
        if (allLocations.Count == 0)
        {
            Debug.LogError("Map connections are empty. Cannot get a random location.");
            return default; // Return default location (None)
        }

        // Pick a random location
        return allLocations[Random.Range(0, allLocations.Count)];
    }
    /// <summary>
    /// Gets a random location excluding specified exclusions.
    /// </summary>
    /// <param name="exclusions">A list of locations to exclude.</param>
    /// <param name="mapConnections">The map connections dictionary.</param>
    /// <returns>A random location not in the exclusions list.</returns>
    public static Location GetRandomLocationExcluding(List<Location> exclusions, Dictionary<Location, List<Location>> mapConnections)
    {
        // Get all available locations
        List<Location> allLocations = new List<Location>(mapConnections.Keys);

        // Filter out excluded locations
        allLocations.RemoveAll(loc => exclusions.Contains(loc));

        // If no valid locations remain, return the first key as fallback
        if (allLocations.Count == 0)
        {
            Debug.LogWarning("No valid locations found. Returning fallback location.");
            return mapConnections.Keys.Count > 0 ? new List<Location>(mapConnections.Keys)[0] : default;
        }

        // Pick a random location
        return allLocations[Random.Range(0, allLocations.Count)];
    }

    /// <summary>
    /// Gets a random connected location for a given location.
    /// </summary>
    /// <param name="currentLocation">The location whose connections are considered.</param>
    /// <param name="mapConnections">The map connections dictionary.</param>
    /// <returns>A random connected location or the current location if no connections exist.</returns>
    public static Location GetRandomConnectedLocation(Location currentLocation, Dictionary<Location, List<Location>> mapConnections)
    {
        // Ensure the current location has connections
        if (!mapConnections.ContainsKey(currentLocation) || mapConnections[currentLocation].Count == 0)
        {
            Debug.LogWarning($"Location {currentLocation} has no connections.");
            return currentLocation; // Fallback to the current location
        }

        // Pick a random connection
        List<Location> connections = mapConnections[currentLocation];
        return connections[Random.Range(0, connections.Count)];
    }

    /// <summary>
    /// Gets a random connected location excluding specified exclusions.
    /// </summary>
    /// <param name="currentLocation">The location whose connections are considered.</param>
    /// <param name="mapConnections">The map connections dictionary.</param>
    /// <param name="exclusions">A list of locations to exclude.</param>
    /// <returns>A random connected location not in the exclusions list.</returns>
    public static Location GetRandomConnectedLocationExcluding(Location currentLocation, Dictionary<Location, List<Location>> mapConnections, List<Location> exclusions)
    {
        // Ensure the current location has connections
        if (!mapConnections.ContainsKey(currentLocation) || mapConnections[currentLocation].Count == 0)
        {
            Debug.LogWarning($"Location {currentLocation} has no connections.");
            return currentLocation; // Fallback to the current location
        }

        // Filter connections by exclusions
        List<Location> connections = mapConnections[currentLocation];
        List<Location> filteredConnections = connections.FindAll(loc => !exclusions.Contains(loc));

        // If no valid connections remain, return a random connection or fallback to the current location
        if (filteredConnections.Count == 0)
        {
            Debug.LogWarning($"No valid connections found for {currentLocation}. Returning fallback.");
            return connections[Random.Range(0, connections.Count)];
        }

        // Pick a random filtered connection
        return filteredConnections[Random.Range(0, filteredConnections.Count)];
    }
}
