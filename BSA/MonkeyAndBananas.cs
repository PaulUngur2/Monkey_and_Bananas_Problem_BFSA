namespace BSA
{
using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<Tuple<int, T>> elements = new List<Tuple<int, T>>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(T item, int priority)
    {
        elements.Add(Tuple.Create(priority, item));
    }

    public T Dequeue()
    {
        int bestIndex = 0;
        // Find the index of the element with the highest priority
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Item1 < elements[bestIndex].Item1)
            {
                bestIndex = i;
            }
        }
        // Store the item with the highest priority
        T bestItem = elements[bestIndex].Item2;
        // Remove the item with the highest priority from the queue
        elements.RemoveAt(bestIndex);
        // Remove the item with the highest priority from the queue
        return bestItem;
    }
}

public class Program
{
    public static bool BestFirstSearch(Dictionary<string, List<string>> graph, string start, string goal, string finalgoal, Dictionary<string, int> heuristic)
    {   bool boxCheck = false;
        PriorityQueue<string> queue = new PriorityQueue<string>();
        queue.Enqueue(start, heuristic[start]);  // Enqueue the start node with its heuristic value
        HashSet<string> visited = new HashSet<string>();  // Set to keep track of visited nodes
        // Loop until the queue is empty
        while (queue.Count > 0)
        {
            // Dequeue the node with the highest priority
            string current = queue.Dequeue();  // Dequeue the node with the lowest priority

            Console.WriteLine(current);

            if (current == goal && boxCheck == false)
            {
                boxCheck = true;
                visited.Clear();
                Console.WriteLine("Box has been reached!");
                queue.Enqueue(current, heuristic[current]);
            }
            
            if(current == finalgoal && boxCheck)
            {
                return true;
            }
            
            visited.Add(current);  // Mark the current node as visited

            foreach (string neighbor in graph[current])
            {
                if (!visited.Contains(neighbor))
                {
                    int priority = heuristic[neighbor];  
                    queue.Enqueue(neighbor, priority);  // Enqueue the neighbor with its priority
                }
            }
        }

        return false;  // No solution found
    }

    public static void Main()
    {
        // Example graph represented as an adjacency list
        Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>()
        {
            { "A", new List<string>() { "B", "C" } },
            { "B", new List<string>() { "D", "E" } },
            { "C", new List<string>() { "F" } },
            { "D", new List<string>() { "H" } },
            { "E", new List<string>() { "G" } },
            { "F", new List<string>() { "I", "J" } },
            { "G", new List<string>() { "K" } },
            { "H", new List<string>() { "L" } },
            { "I", new List<string>() },
            { "J", new List<string>() { "M" } },
            { "K", new List<string>() { "N" } },
            { "L", new List<string>() },
            { "M", new List<string>() },
            { "N", new List<string>() }
        };

        // Heuristic values for each node
        Dictionary<string, int> heuristic = new Dictionary<string, int>()
        {
            { "A", 10 },
            { "B", 8 },
            { "C", 5 },
            { "D", 4 },
            { "E", 3 },
            { "F", 2 },
            { "G", 6 },
            { "H", 3 },
            { "I", 1 },
            { "J", 2 },
            { "K", 4 },
            { "L", 0 },
            { "M", 1 },
            { "N", 2 }
        };

        string startNode = "A";
        string boxNode = "F";
        string bananaNode = "K";

        // Perform Best-First Search
        bool result = BestFirstSearch(graph, startNode, boxNode, bananaNode, heuristic);

        // Print the result
        Console.WriteLine(result ? "Banana has been reached!" : "No solution found.");
    }
}

}