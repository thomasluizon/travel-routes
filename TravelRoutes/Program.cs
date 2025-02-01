namespace TravelRoutes
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "rotas.txt";
            var repository = new RouteRepository(filePath);
            var graph = new Graph();

            List<Edge> routes = repository.LoadRoutes();

            foreach (var route in routes)
            {
                graph.AddEdge(route.Origin, route.Destination, route.Cost);
            }

            while (true)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Consultar melhor rota");
                Console.WriteLine("2 - Registrar nova rota");
                Console.WriteLine("3 - Sair");
                Console.Write("Opção: ");

                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.Write("Digite a rota (ex: GRU-CDG): ");
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input) || !input.Contains('-'))
                    {
                        Console.WriteLine("Formato inválido. Use Origem-Destino (ex: GRU-CDG).");
                        continue;
                    }

                    string[] parts = input.Split('-');

                    if (parts.Length != 2)
                    {
                        Console.WriteLine("Formato inválido. Use Origem-Destino (ex: GRU-CDG).");
                        continue;
                    }

                    string origin = parts[0].Trim();
                    string destination = parts[1].Trim();
                    var (path, cost) = graph.FindBestRoute(origin, destination);

                    if (cost == -1 || path.Count == 0)
                    {
                        Console.WriteLine($"Não existe rota de {origin} para {destination}.");
                    }
                    else
                    {
                        Console.WriteLine($"Melhor Rota: {string.Join(" - ", path)} ao custo de ${cost}");
                    }
                }
                else if (option == "2")
                {
                    Console.Write("Digite a nova rota no formato Origem,Destino,Valor (ex: GRU,BRC,10): ");
                    string input = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.WriteLine("Entrada inválida.");
                        continue;
                    }

                    string[] parts = input.Split(',');

                    if (parts.Length != 3)
                    {
                        Console.WriteLine("Formato inválido. Use Origem,Destino,Valor (ex: GRU,BRC,10).");
                        continue;
                    }

                    string origin = parts[0].Trim();
                    string destination = parts[1].Trim();

                    if (!int.TryParse(parts[2].Trim(), out int cost))
                    {
                        Console.WriteLine("Valor inválido para custo.");
                        continue;
                    }

                    var newRoute = new Edge(origin, destination, cost);
                    graph.AddEdge(origin, destination, cost);
                    repository.SaveRoute(newRoute);
                    Console.WriteLine("Rota adicionada com sucesso!");
                }
                else if (option == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }

                Console.WriteLine();
            }
        }
    }
}
