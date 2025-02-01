namespace TravelRoutes
{
    public class Graph
    {
        private readonly Dictionary<string, List<Edge>> adjacencyList = new(StringComparer.OrdinalIgnoreCase);

        public void AddEdge(string origin, string destination, int cost)
        {
            var edge = new Edge(origin, destination, cost);

            if (!adjacencyList.TryGetValue(origin, out List<Edge> value))
            {
                value = [];
                adjacencyList[origin] = value;
            }

            value.Add(edge);
        }
        public (List<string> path, int cost) FindBestRoute(string start, string end)
        {
            List<string> bestPath = null;
            int bestCost = int.MaxValue;
            var currentPath = new List<string> { start };

            DFS(start, end, 0, currentPath, ref bestPath, ref bestCost);

            if (bestPath == null)
            {
                return (new List<string>(), -1);
            }

            return (bestPath, bestCost);
        }

        private void DFS(string current, string destination, int cost, List<string> currentPath, ref List<string> bestPath, ref int bestCost)
        {
            if (current.Equals(destination, StringComparison.OrdinalIgnoreCase))
            {
                if (cost < bestCost)
                {
                    bestCost = cost;
                    bestPath = new List<string>(currentPath);
                }
                return;
            }

            if (!adjacencyList.TryGetValue(current, out List<Edge> value))
                return;

            foreach (Edge edge in value)
            {
                if (currentPath.Contains(edge.Destination, StringComparer.OrdinalIgnoreCase))
                    continue;

                int newCost = cost + edge.Cost;

                if (newCost >= bestCost)
                    continue;

                currentPath.Add(edge.Destination);
                DFS(edge.Destination, destination, newCost, currentPath, ref bestPath, ref bestCost);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }
}
