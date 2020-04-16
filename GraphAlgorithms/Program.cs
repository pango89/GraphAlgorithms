using System;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            UndirectedGraph graph = new UndirectedGraph(6);

            graph.AddEdge(0, 1);  // This will add (1, 0) also.
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(4, 5);

            graph.DFS();
            graph.BFS();
        }
    }
}
