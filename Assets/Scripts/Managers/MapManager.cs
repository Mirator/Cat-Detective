using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static Graph<Location> CreateMap()
    {
        Graph<Location> map = new Graph<Location>();

        // Define bidirectional edges
        map.AddEdge(Location.Garden, Location.Bakery);
        map.AddEdge(Location.Bakery, Location.Treehouse);
        map.AddEdge(Location.Treehouse, Location.Market);
        map.AddEdge(Location.Market, Location.Barn);
        map.AddEdge(Location.Barn, Location.Riverbank);

        Debug.Log("Map created with bidirectional connections.");
        return map;
    }
}
