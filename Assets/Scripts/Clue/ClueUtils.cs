using System.Collections.Generic;
using UnityEngine;

public static class ClueUtils
{
    public static Location GetRandomLocation(Graph<Location> map)
    {
        List<Location> allLocations = map.GetAllNodes();
        return allLocations[Random.Range(0, allLocations.Count)];
    }

    public static Location GetRandomLocationExcluding(Graph<Location> map, List<Location> exclusions)
    {
        List<Location> allLocations = map.GetAllNodes();
        List<Location> filteredLocations = allLocations.FindAll(loc => !exclusions.Contains(loc));
        return filteredLocations.Count == 0 ? allLocations[0] : filteredLocations[Random.Range(0, filteredLocations.Count)];
    }

    public static Location GetRandomConnectedLocationExcluding(Graph<Location> map, Location currentLocation, List<Location> exclusions)
    {
        List<Location> connections = map.GetConnections(currentLocation);
        List<Location> filteredConnections = connections.FindAll(loc => !exclusions.Contains(loc));
        return filteredConnections.Count == 0 ? connections[0] : filteredConnections[Random.Range(0, filteredConnections.Count)];
    }
}
