namespace TravelRoutes
{
    public class Edge(string origin, string destination, int cost)
    {
        public string Origin { get; } = origin;
        public string Destination { get; } = destination;
        public int Cost { get; } = cost;
    }
}
