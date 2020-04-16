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
    }
}
