namespace TravelRoutes.Tests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void FindBestRoute_GRU_to_CDG_Returns_Correct_Route()
        {
            // Arrange: usando os dados do enunciado
            var graph = new Graph();
            graph.AddEdge("GRU", "BRC", 10);
            graph.AddEdge("BRC", "SCL", 5);
            graph.AddEdge("GRU", "CDG", 75);
            graph.AddEdge("GRU", "SCL", 20);
            graph.AddEdge("GRU", "ORL", 56);
            graph.AddEdge("ORL", "CDG", 5);
            graph.AddEdge("SCL", "ORL", 20);

            // Act
            var (path, cost) = graph.FindBestRoute("GRU", "CDG");

            // Assert
            var expectedPath = new List<string> { "GRU", "BRC", "SCL", "ORL", "CDG" };
            int expectedCost = 40;
            CollectionAssert.AreEqual(expectedPath, path);
            Assert.AreEqual(expectedCost, cost);
        }

        [TestMethod]
        public void FindBestRoute_BRC_to_SCL_Returns_Direct_Route()
        {
            // Arrange
            var graph = new Graph();
            graph.AddEdge("BRC", "SCL", 5);

            // Act
            var (path, cost) = graph.FindBestRoute("BRC", "SCL");

            // Assert
            var expectedPath = new List<string> { "BRC", "SCL" };
            int expectedCost = 5;
            CollectionAssert.AreEqual(expectedPath, path);
            Assert.AreEqual(expectedCost, cost);
        }

        [TestMethod]
        public void FindBestRoute_No_Route_Returns_Empty()
        {
            // Arrange
            var graph = new Graph();
            graph.AddEdge("A", "B", 10);

            // Act
            var result = graph.FindBestRoute("A", "C");

            // Assert
            Assert.AreEqual(-1, result.cost);
            Assert.AreEqual(0, result.path.Count);
        }
    }
}
