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

    public List<Location> GeneratePath(Location start, Location end, int numberOfVillagers)
    {
        path.Clear();
        HashSet<Location> visited = new HashSet<Location>();

        // Ensure path length is 4
        int pathLength = 4;

        Location current = start;
        path.Add(current);
        visited.Add(current);

        while (path.Count < pathLength - 1) // Generate intermediate locations
        {
            List<Location> neighbors = mapManager.GetNeighbors(current);

            if (neighbors.Count == 0)
            {
                Debug.LogError($"No valid neighbors for {current}. Path generation failed.");
                break;
            }

            // Pick a random unvisited neighbor
            List<Location> unvisitedNeighbors = neighbors.FindAll(n => !visited.Contains(n));
            if (unvisitedNeighbors.Count == 0)
            {
                // If all neighbors are visited, allow revisiting
                unvisitedNeighbors = neighbors;
            }

            current = unvisitedNeighbors[UnityEngine.Random.Range(0, unvisitedNeighbors.Count)];
            path.Add(current);
            visited.Add(current);
        }

        // Ensure the end location is always added to complete the path
        if (path.Count < pathLength)
        {
            path.Add(end);
        }

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
