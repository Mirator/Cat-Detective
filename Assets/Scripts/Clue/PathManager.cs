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

    public List<Location> GeneratePath(Location start, Location end)
    {
        path.Clear();
        Location current = start;

        while (current != end)
        {
            path.Add(current);
            List<Location> neighbors = mapManager.GetNeighbors(current);

            // Pick the next location leading closer to the end
            foreach (var neighbor in neighbors)
            {
                if (!path.Contains(neighbor))
                {
                    current = neighbor;
                    break;
                }
            }
        }

        path.Add(end);
        return path;
    }

    public List<Location> GetPath()
    {
        return path;
    }
}
