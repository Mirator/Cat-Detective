using System.Collections.Generic;

public class MapManager
{
    private Dictionary<Location, List<Location>> connections = new Dictionary<Location, List<Location>>();

    public MapManager()
    {
        InitializeConnections();
    }

    /// <summary>
    /// Initializes the map connections.
    /// </summary>
    private void InitializeConnections()
    {
        AddConnection(Location.Garden, Location.Bakery);
        AddConnection(Location.Bakery, Location.Treehouse);
        AddConnection(Location.Treehouse, Location.Barn);
        AddConnection(Location.Bakery, Location.Store);
        AddConnection(Location.Store, Location.Barn);
        AddConnection(Location.Store, Location.Beach);
        AddConnection(Location.Barn, Location.Beach);
    }


    /// <summary>
    /// Adds a bidirectional connection between two locations.
    /// </summary>
    private void AddConnection(Location from, Location to)
    {
        if (!connections.ContainsKey(from))
        {
            connections[from] = new List<Location>();
        }

        if (!connections.ContainsKey(to))
        {
            connections[to] = new List<Location>();
        }

        connections[from].Add(to);
        connections[to].Add(from); // Bidirectional
    }

    /// <summary>
    /// Gets the connections map.
    /// </summary>
    public Dictionary<Location, List<Location>> GetConnections()
    {
        return connections;
    }

    /// <summary>
    /// Gets neighbors for a specific location.
    /// </summary>
    public List<Location> GetNeighbors(Location location)
    {
        return connections.ContainsKey(location) ? connections[location] : new List<Location>();
    }

    /// <summary>
    /// Gets all locations in the map.
    /// </summary>
    public List<Location> GetAllLocations()
    {
        return new List<Location>(connections.Keys);
    }
}
