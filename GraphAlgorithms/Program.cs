﻿using System;

namespace GraphAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            UndirectedGraph graph = new UndirectedGraph(8);

            graph.AddEdge(0, 1);  // This will add (1, 0) also.
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 7);
            graph.AddEdge(4, 5);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 6);
            graph.AddEdge(6, 7);

            //graph.DFS();
            //graph.BFS();
            // Console.WriteLine(graph.IsCyclic() ? "Cyclic" : "Not Cyclic");
            graph.ShortestDistanceUnWeighted(0, 7);


            //DirectedGraph graph = new DirectedGraph(6);
            //graph.AddEdge(5, 2);
            //graph.AddEdge(5, 0);
            //graph.AddEdge(4, 0);
            //graph.AddEdge(4, 1);
            //graph.AddEdge(2, 3);
            //graph.AddEdge(3, 1);
            //graph.AddEdge(1, 5);

            //graph.TopologicalSortKahns();
            //graph.TopologicalSortDFS();

            //graph.PrintAllTopologicalSorts();
            //Console.WriteLine(graph.IsCyclic() ? "Cyclic" : "Not Cyclic");

            //DirectedGraph graph = new DirectedGraph(5);
            //graph.AddEdge(1, 0);
            //graph.AddEdge(0, 2);
            //graph.AddEdge(2, 1);
            //graph.AddEdge(0, 3);
            //graph.AddEdge(3, 4);

            //// Console.WriteLine(graph.IsStronglyConnected() ? "SC" : "Not SC");
            //graph.PrintAllSCC();
        }
    }
}
