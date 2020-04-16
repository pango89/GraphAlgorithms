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
            if (visited[i])
                return;

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
    }
}
