using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static Graph<Location> CreateMap()
    {
        Graph<Location> map = new Graph<Location>();

        // Add bidirectional edges manually
        map.AddEdge(Location.Garden, Location.Bakery); // Garden -> Bakery
        map.AddEdge(Location.Bakery, Location.Garden); // Bakery -> Garden

        map.AddEdge(Location.Bakery, Location.Treehouse); // Bakery -> Treehouse
        map.AddEdge(Location.Treehouse, Location.Bakery); // Treehouse -> Bakery

        map.AddEdge(Location.Treehouse, Location.Market); // Treehouse -> Market
        map.AddEdge(Location.Market, Location.Treehouse); // Market -> Treehouse

        map.AddEdge(Location.Market, Location.Barn); // Market -> Barn
        map.AddEdge(Location.Barn, Location.Market); // Barn -> Market

        map.AddEdge(Location.Barn, Location.Riverbank); // Barn -> Riverbank
        map.AddEdge(Location.Riverbank, Location.Barn); // Riverbank -> Barn

        return map;
    }
}
