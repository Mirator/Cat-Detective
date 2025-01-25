using System.Collections.Generic;
using UnityEngine;

public static class ClueUtils
{
    /// <summary>
    /// Gets a random location from the map connections.
    /// </summary>
    public static Location GetRandomLocation(Dictionary<Location, List<Location>> mapConnections)
    {
        List<Location> allLocations = new List<Location>(mapConnections.Keys);

        if (allLocations.Count == 0)
        {
            Debug.LogError("Map connections are empty. Cannot get a random location.");
            return default; // Return default location (None)
        }

        return allLocations[Random.Range(0, allLocations.Count)];
    }

    /// <summary>
    /// Gets a random location excluding specified exclusions.
    /// </summary>
    public static Location GetRandomLocationExcluding(List<Location> exclusions, Dictionary<Location, List<Location>> mapConnections)
    {
        List<Location> allLocations = new List<Location>(mapConnections.Keys);
        allLocations.RemoveAll(loc => exclusions.Contains(loc));

        if (allLocations.Count == 0)
        {
            Debug.LogWarning("No valid locations found. Returning fallback location.");
            return mapConnections.Keys.Count > 0 ? new List<Location>(mapConnections.Keys)[0] : default;
        }

        return allLocations[Random.Range(0, allLocations.Count)];
    }

    /// <summary>
    /// Gets a random connected location for a given location.
    /// </summary>
    public static Location GetRandomConnectedLocation(Location currentLocation, Dictionary<Location, List<Location>> mapConnections)
    {
        if (!mapConnections.ContainsKey(currentLocation) || mapConnections[currentLocation].Count == 0)
        {
            Debug.LogWarning($"Location {currentLocation} has no connections.");
            return currentLocation; // Fallback to the current location
        }

        List<Location> connections = mapConnections[currentLocation];
        return connections[Random.Range(0, connections.Count)];
    }

    /// <summary>
    /// Gets a random connected location excluding specified exclusions.
    /// Ensures at least one connection is returned even if all are excluded.
    /// </summary>
    public static Location GetRandomConnectedLocationExcluding(
        Location currentLocation,
        Dictionary<Location, List<Location>> mapConnections,
        List<Location> exclusions)
    {
        if (!mapConnections.ContainsKey(currentLocation) || mapConnections[currentLocation].Count == 0)
        {
            Debug.LogWarning($"Location {currentLocation} has no connections.");
            return currentLocation; // Fallback to the current location
        }

        List<Location> connections = mapConnections[currentLocation];
        List<Location> filteredConnections = connections.FindAll(loc => !exclusions.Contains(loc));

        //Debug.Log($"Connections for {currentLocation}: {string.Join(", ", connections)}");
        //Debug.Log($"Exclusions for {currentLocation}: {string.Join(", ", exclusions)}");
        //Debug.Log($"Filtered connections for {currentLocation}: {string.Join(", ", filteredConnections)}");

        // If no valid connections remain, allow one excluded connection
        if (filteredConnections.Count == 0)
        {
            Debug.LogWarning($"No valid connections found for {currentLocation}. Returning one excluded connection.");
            return connections[Random.Range(0, connections.Count)];
        }

        return filteredConnections[Random.Range(0, filteredConnections.Count)];
    }

}
