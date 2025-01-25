using System.Collections.Generic;
using UnityEngine;

public class PathManager
{
    private MapManager mapManager;
    private List<Location> path = new List<Location>();

    public PathManager(MapManager mapManager)
    {
        this.mapManager = mapManager;
    }

    /// <summary>
    /// Generates a path from the start location to the end location.
    /// Allows revisiting a path if necessary.
    /// </summary>
    public List<Location> GeneratePath(Location start, Location end, bool allowRevisit = false)
    {
        path.Clear();
        Location current = start;
        HashSet<Location> visited = allowRevisit ? null : new HashSet<Location>();

        while (current != end)
        {
            path.Add(current);
            if (!allowRevisit) visited.Add(current);

            List<Location> neighbors = mapManager.GetNeighbors(current);
            Location next = GetNextLocation(neighbors, visited);

            if (next == default)
            {
                Debug.LogError($"Failed to find a valid path from {start} to {end}. Exiting loop.");
                break;
            }

            current = next;
        }

        path.Add(end);
        Debug.Log($"Generated path: {string.Join(" -> ", path)}");
        return path;
    }

    /// <summary>
    /// Returns the currently generated path.
    /// </summary>
    public List<Location> GetPath()
    {
        return path;
    }

    /// <summary>
    /// Finds the next valid location from neighbors.
    /// </summary>
    private Location GetNextLocation(List<Location> neighbors, HashSet<Location> visited)
    {
        if (neighbors == null || neighbors.Count == 0)
        {
            return default;
        }

        // If revisiting is allowed, pick any neighbor
        if (visited == null)
        {
            return neighbors[Random.Range(0, neighbors.Count)];
        }

        // Filter neighbors to exclude already visited locations
        List<Location> unvisited = neighbors.FindAll(loc => !visited.Contains(loc));
        if (unvisited.Count > 0)
        {
            return unvisited[Random.Range(0, unvisited.Count)];
        }

        // If all neighbors are visited, allow revisiting
        return neighbors[Random.Range(0, neighbors.Count)];
    }
}
