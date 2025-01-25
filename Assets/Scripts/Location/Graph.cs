using System.Collections.Generic;

public class Graph<T>
{
    private Dictionary<T, List<T>> adjacencyList = new Dictionary<T, List<T>>();

    public void AddNode(T node)
    {
        if (!adjacencyList.ContainsKey(node))
        {
            adjacencyList[node] = new List<T>();
        }
    }

    public void AddEdge(T from, T to)
    {
        if (!adjacencyList.ContainsKey(from)) AddNode(from);
        if (!adjacencyList.ContainsKey(to)) AddNode(to);

        adjacencyList[from].Add(to);
    }

    public List<T> GetConnections(T node)
    {
        return adjacencyList.ContainsKey(node) ? adjacencyList[node] : new List<T>();
    }

    public List<T> GetAllNodes()
    {
        return new List<T>(adjacencyList.Keys);
    }

    public List<T> GetPathTo(T destination)
    {
        // Placeholder for pathfinding (e.g., BFS or Dijkstra's algorithm)
        List<T> path = new List<T>(adjacencyList.Keys); // Simplified for now
        return path;
    }
}
