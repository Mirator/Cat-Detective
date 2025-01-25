using System.Collections.Generic;
using UnityEngine;

public class PathManager
{
    private Graph<Location> map;
    private Location finalLocation;

    public PathManager(Graph<Location> map, Location finalLocation)
    {
        this.map = map;
        this.finalLocation = finalLocation;
    }

    /// <summary>
    /// Validates and truncates a path to the final location.
    /// </summary>
    public List<Location> GetValidatedPath()
    {
        List<Location> path = new List<Location>(map.GetPathTo(finalLocation));

        if (path == null || !path.Contains(finalLocation))
        {
            Debug.LogWarning("No valid path found to the final location.");
            return null;
        }

        // Truncate the path to stop at the final location
        int finalIndex = path.IndexOf(finalLocation);
        return path.GetRange(0, finalIndex + 1);
    }
}
