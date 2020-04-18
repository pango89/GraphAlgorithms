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
            Console.Write(i + " ");

            visited[i] = true;
            foreach (int adjacent in this.adjacencyList[i])
                if (!visited[adjacent])
                    DFSUtil(adjacent, visited);
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

        public bool IsCyclic()
        {
            bool[] visited = new bool[this.vertices];

            Color[] vertexColors = new Color[this.vertices];
            for (int i = 0; i < this.vertices; i++)
                vertexColors[i] = Color.white;

            for (int i = 0; i < this.vertices; i++)
            {
                if (!visited[i])
                    if (IsCyclicUtil(i, visited, vertexColors))
                        return true;
            }

            return false;
        }

        private bool IsCyclicUtil(int i, bool[] visited, Color[] vertexColors)
        {
            visited[i] = true;
            vertexColors[i] = Color.gray;

            foreach (int adjacent in this.adjacencyList[i])
            {
                if (!visited[adjacent])
                    IsCyclicUtil(adjacent, visited, vertexColors);

                if (vertexColors[adjacent] == Color.black)
                    continue;

                if (vertexColors[adjacent] == Color.gray)
                    return true;
            }

            vertexColors[i] = Color.black;

            return false;
        }

        public enum Color
        {
            white,
            gray,
            black
        }

        private DirectedGraph TransposeGraph()
        {
            DirectedGraph transpose = new DirectedGraph(this.vertices);

            for (int i = 0; i < this.vertices; i++)
                foreach (int adjacent in this.adjacencyList[i])
                    transpose.adjacencyList[adjacent].Add(i);

            return transpose;
        }

        // Kosaraju's Algorithm to check if Directed Graph is Strongly connected or not
        public bool IsStronglyConnected()
        {
            bool[] visited = new bool[this.vertices];

            // Run DFS on any one node
            this.DFSUtil(0, visited);

            // After DFS, if any of the nodes is not visited then it is disconnected
            for (int i = 0; i < this.vertices; i++)
                if (!visited[i])
                    return false;

            // Now that the graph is connected, check if strongly connected or not

            DirectedGraph transpose = this.TransposeGraph();

            for (int i = 0; i < transpose.vertices; i++)
                visited[i] = false;

            // Run DFS on any one node
            transpose.DFSUtil(0, visited);

            // After DFS, if any of the nodes is not visited then it is disconnected
            for (int i = 0; i < transpose.vertices; i++)
                if (!visited[i])
                    return false;

            return true;
        }

        /// https://www.geeksforgeeks.org/strongly-connected-components/
        /// Kosaraju's Algo to Print All SCCs 
        public void PrintAllSCC()
        {
            bool[] visited = new bool[this.vertices];
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < this.vertices; i++)
                if (!visited[i])
                    this.FillStack(i, visited, stack);

            DirectedGraph transpose = this.TransposeGraph();

            for (int i = 0; i < transpose.vertices; i++)
                visited[i] = false;

            while(stack.Count > 0)
            {
                int next = stack.Pop();

                if (!visited[next])
                {
                    transpose.DFSUtil(next, visited);
                    Console.WriteLine();
                }
            }
        }

        private void FillStack(int i, bool[] visited, Stack<int> stack)
        {
            visited[i] = true;

            foreach (int adjacent in this.adjacencyList[i])
                if (!visited[adjacent])
                    this.FillStack(adjacent, visited, stack);

            stack.Push(i);
        }
    }
}
