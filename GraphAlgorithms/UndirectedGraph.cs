using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    class UndirectedGraph
    {
        private readonly int vertices;
        private readonly List<int>[] adjacencyList;

        public UndirectedGraph(int vertices)
        {
            this.vertices = vertices;
            this.adjacencyList = new List<int>[vertices];

            for (int i = 0; i < this.vertices; i++)
                this.adjacencyList[i] = new List<int>();
        }

        public void AddEdge(int u, int v)
        {
            this.adjacencyList[u].Add(v);
            this.adjacencyList[v].Add(u);
        }

        // DFS for all vertex one by one
        // Following implementation does the complete graph 
        // traversal even if the nodes are unreachable.
        public void DFS()
        {
            bool[] visited = new bool[this.vertices];

            for (int i = 0; i < this.vertices; i++)
            {
                if (!visited[i])
                {
                    DFSUtil(i, visited);
                    Console.WriteLine();
                }
            }
        }

        // DFS only for a given vertex
        public void DFS(int i)
        {
            bool[] visited = new bool[this.vertices];
            DFSUtil(i, visited);
        }

        private void DFSUtil(int i, bool[] visited)
        {
            if (visited[i])
                return;

            Console.Write(i + " ");

            visited[i] = true;

            for (int j = 0; j < this.adjacencyList[i].Count; j++)
            {
                int next = this.adjacencyList[i][j];

                if (!visited[next])
                    DFSUtil(next, visited);
            }
        }

        public void BFS()
        {
            bool[] visited = new bool[this.vertices];

            for (int i = 0; i < this.vertices; i++)
            {
                if (!visited[i])
                {
                    BFSUtil(i, visited);
                    Console.WriteLine();
                }
            }
        }

        private void BFSUtil(int i, bool[] visited)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(i);

            while (queue.Count > 0)
            {
                int item = queue.Dequeue();
                Console.Write(item + " ");
                visited[item] = true;

                for (int j = 0; j < this.adjacencyList[item].Count; j++)
                {
                    int next = this.adjacencyList[item][j];
                    if (!visited[next])
                    {
                        queue.Enqueue(next);
                        visited[next] = true;
                    }
                }
            }
        }

        public bool IsCyclic()
        {
            bool[] visited = new bool[this.vertices];

            for (int i = 0; i < this.vertices; i++)
            {
                if (!visited[i])
                    if (IsCyclicUtil(i, visited, -1))
                        return true;
            }

            return false;
        }

        private bool IsCyclicUtil(int i, bool[] visited, int parent)
        {
            visited[i] = true;

            foreach (int adjacent in this.adjacencyList[i])
            {
                if (visited[adjacent])
                {
                    if (adjacent != parent)
                        return true;
                }
                else
                {
                    if (IsCyclicUtil(adjacent, visited, i))
                        return true;
                }
            }

            return false;
        }

        // Modified BFS
        // Can work for Directed graph as well
        // Each Edge costs 1
        public void ShortestDistanceUnWeighted(int source, int destination)
        {
            int[] distance = new int[this.vertices];
            int[] predecessor = new int[this.vertices];

            bool found = this.ModifiedBFS(source, destination, distance, predecessor);

            if (!found)
            {
                Console.WriteLine("Source and Destination are not connected!");
                return;
            }

            List<int> path = new List<int>();

            int start = destination;

            while (start != source)
            {
                path.Add(start);
                start = predecessor[start];
            }

            path.Add(source);

            Console.WriteLine("Shortest path length is " + distance[destination]);
            Console.Write("Shortest path is ");

            for (int i = path.Count - 1; i >= 0; i--)
                Console.Write(path[i] + " ");
        }

        private bool ModifiedBFS(int source, int destination, int[] distance, int[] predecessor)
        {
            bool[] visited = new bool[this.vertices];

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                int next = queue.Dequeue();
                visited[next] = true;

                foreach (int adjacent in this.adjacencyList[next])
                {
                    if (!visited[adjacent])
                    {
                        distance[adjacent] = distance[next] + 1;
                        predecessor[adjacent] = next;
                        queue.Enqueue(adjacent);

                        if (adjacent == destination)
                            return true;
                    }
                }
            }

            return false;
        }
    }
}
