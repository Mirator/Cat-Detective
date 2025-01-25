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

        Location current = start;
        HashSet<Location> visited = new HashSet<Location>();

        while (current != end && path.Count < Mathf.Max(3, numberOfVillagers / 2))
        {
            path.Add(current);
            visited.Add(current);

            List<Location> neighbors = mapManager.GetNeighbors(current);
            if (neighbors.Count == 0)
            {
                Debug.LogError($"No valid neighbors for {current}. Path generation failed.");
                break;
            }

            Location next = neighbors.Find(n => !visited.Contains(n) || n == end);

            current = next;
        }

        if (!path.Contains(end))
        {
            Debug.LogWarning($"Path does not include final location {end}. Forcing inclusion.");
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
