using System.Collections.Generic;
using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public List<Clue> clues = new List<Clue>(); // All generated clues
    public List<Location> locations = new List<Location>(); // Available locations
    public string[] times = { "Morning", "Noon", "Evening" }; // Time options
    public List<Villager> villagers = new List<Villager>(); // All villagers in the scene

    private Dictionary<Location, List<Location>> mapConnections = new Dictionary<Location, List<Location>>(); // Map structure

    void Start()
    {
        InitializeLocations();
        GenerateMapConnections();
        GenerateClues();
        AssignCluesToVillagers();
    }

    /// <summary>
    /// Initializes the locations.
    /// </summary>
    private void InitializeLocations()
    {
        locations.AddRange((Location[])System.Enum.GetValues(typeof(Location)));
    }

    /// <summary>
    /// Creates the valid connections between locations.
    /// </summary>
    private void GenerateMapConnections()
    {
        mapConnections.Add(Location.Garden, new List<Location> { Location.Bakery });
        mapConnections.Add(Location.Bakery, new List<Location> { Location.Garden, Location.Treehouse, Location.Store });
        mapConnections.Add(Location.Treehouse, new List<Location> { Location.Bakery, Location.Barn });
        mapConnections.Add(Location.Store, new List<Location> { Location.Bakery, Location.Barn, Location.Beach });
        mapConnections.Add(Location.Barn, new List<Location> { Location.Store, Location.Treehouse });
        mapConnections.Add(Location.Beach, new List<Location> { Location.Store });
    }

    /// <summary>
    /// Generates clues for the game.
    /// </summary>
    private void GenerateClues()
    {
        foreach (Location location in locations)
        {
            string time = times[Random.Range(0, times.Length)];
            Location nextLocation = GetRandomConnectedLocation(location);

            Clue clue = new Clue
            {
                Time = time,
                SeenAt = location,
                NextLocation = nextLocation
            };

            clues.Add(clue);
        }
    }

    /// <summary>
    /// Assigns clues to villagers randomly.
    /// </summary>
    private void AssignCluesToVillagers()
    {
        for (int i = 0; i < villagers.Count && i < clues.Count; i++)
        {
            villagers[i].AssignClue(clues[i]);
        }
    }

    /// <summary>
    /// Gets a random connected location from the map.
    /// </summary>
    /// <param name="currentLocation">The current location.</param>
    /// <returns>A random connected location.</returns>
    private Location GetRandomConnectedLocation(Location currentLocation)
    {
        if (mapConnections.ContainsKey(currentLocation))
        {
            List<Location> connections = mapConnections[currentLocation];
            return connections[Random.Range(0, connections.Count)];
        }

        return currentLocation; // If no connections, return the same location
    }
}
