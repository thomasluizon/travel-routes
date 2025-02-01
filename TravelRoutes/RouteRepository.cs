namespace TravelRoutes
{
    public class RouteRepository
    {
        private readonly string filePath;

        public RouteRepository(string filePath)
        {
            this.filePath = filePath;

            if (!File.Exists(this.filePath))
            {
                File.WriteAllText(this.filePath, "");
            }
        }

        public List<Edge> LoadRoutes()
        {
            var routes = new List<Edge>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (!line.Contains(','))
                    continue;

                string[] parts = line.Split(',');

                if (parts.Length != 3)
                    continue;

                string origin = parts[0].Trim();
                string destination = parts[1].Trim();

                if (!int.TryParse(parts[2].Trim(), out int cost))
                    continue;

                routes.Add(new Edge(origin, destination, cost));
            }

            return routes;
        }

        public void SaveRoute(Edge route)
        {
            string line = $"{route.Origin},{route.Destination},{route.Cost}";
            File.AppendAllText(filePath, line + Environment.NewLine);
        }
    }
}
