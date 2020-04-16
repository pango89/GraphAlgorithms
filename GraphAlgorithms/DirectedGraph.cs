using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithms
{
    class DirectedGraph
    {
        private readonly int vertices;
        private readonly List<int>[] adjacencyList;

        public DirectedGraph(int vertices)
        {
            this.vertices = vertices;
            this.adjacencyList = new List<int>[vertices];

            for (int i = 0; i < this.vertices; i++)
                this.adjacencyList[i] = new List<int>();
        }

        public void AddEdge(int u, int v)
        {
            this.adjacencyList[u].Add(v);
        }

        public void TopologicalSortKahns()
        {
            int[] inDegree = new int[this.vertices];

            for (int i = 0; i < this.vertices; i++)
                foreach (int adjacenct in this.adjacencyList[i])
                    inDegree[adjacenct]++;

            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < inDegree.Length; i++)
                if (inDegree[i] == 0)
                    queue.Enqueue(i);

            List<int> sorted = new List<int>();
            int countVisited = 0;

            while (queue.Count > 0)
            {
                int item = queue.Dequeue();
                sorted.Add(item);

                foreach (int next in this.adjacencyList[item])
                {
                    inDegree[next]--;
                    if (inDegree[next] == 0)
                        queue.Enqueue(next);
                }

                countVisited++;
            }

            if (countVisited != this.vertices)
            {
                Console.WriteLine("Cycle Exists!");
                return;
            }

            foreach (int item in sorted)
                Console.Write(item + " ");
        }


        // Doesn't detect if cycle is present and returns wrong solution
        // Kahn's Algorithm takes care of Cycle also.
        public void TopologicalSortDFS()
        {
            bool[] visited = new bool[this.vertices];
            Stack<int> result = new Stack<int>();

            for (int i = 0; i < this.vertices; i++)
                if (!visited[i])
                    this.TopologicalSortDFSUtil(i, visited, result);

            while (result.Count > 0)
                Console.Write(result.Pop() + " ");
        }

        private void TopologicalSortDFSUtil(int i, bool[] visited, Stack<int> result)
        {
            if (visited[i])
                return;

            visited[i] = true;

            foreach (int adjacent in this.adjacencyList[i])
                if (!visited[adjacent])
                    TopologicalSortDFSUtil(adjacent, visited, result);

            result.Push(i);
        }

        public void PrintAllTopologicalSorts()
        {
            int[] inDegree = new int[this.vertices];

            for (int i = 0; i < this.vertices; i++)
                foreach (int adjacenct in this.adjacencyList[i])
                    inDegree[adjacenct]++;

            bool[] visited = new bool[this.vertices];
            LinkedList<int> result = new LinkedList<int>();

            this.PrintAllTopologicalSortsUtil(visited, inDegree, result);
        }

        private void PrintAllTopologicalSortsUtil(bool[] visited, int[] inDegree, LinkedList<int> result)
        {
            if (result.Count == this.vertices)
            {
                foreach (int item in result)
                    Console.Write(item + " ");
                Console.WriteLine();
            }

            for (int i = 0; i < this.vertices; i++)
            {
                if (!visited[i] && inDegree[i] == 0)
                {
                    visited[i] = true;
                    result.AddLast(i);
                    foreach (int adjacent in this.adjacencyList[i])
                        inDegree[adjacent]--;

                    PrintAllTopologicalSortsUtil(visited, inDegree, result);

                    // Back Track
                    visited[i] = false;
                    result.RemoveLast();
                    foreach (int adjacent in this.adjacencyList[i])
                        inDegree[adjacent]++;
                }
            }
        }
    }
}
