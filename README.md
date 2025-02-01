# Rota de Viagem

## Descrição

A aplicação **Rota de Viagem** tem como objetivo encontrar a rota de viagem mais barata entre dois pontos, independentemente da quantidade de conexões. Por exemplo, para uma viagem de **GRU** para **CDG**, o sistema deve identificar a rota com o menor custo final, mesmo que ela possua mais conexões. No exemplo apresentado, as opções são:

1. **GRU - BRC - SCL - ORL - CDG** com custo de $40
2. **GRU - ORL - CDG** com custo de $61
3. **GRU - CDG** com custo de $75
4. **GRU - SCL - ORL - CDG** com custo de $45

A aplicação deve retornar a **Rota 1** como a melhor opção, pois apesar de ter mais conexões, seu custo final é o menor.

Além disso, o sistema permite o **registro de novas rotas**, que serão persistidas em um arquivo para futuras consultas, e a **consulta de melhor rota** entre dois pontos.

**Importante:**
_Não deve ser utilizado o algoritmo Dijkstra._ Em vez disso, a solução utiliza uma busca em profundidade (DFS) com backtracking para encontrar o caminho de menor custo.

## Funcionalidades

-  **Registro de Novas Rotas:**
   O usuário pode inserir novas rotas no formato `Origem,Destino,Valor` (por exemplo, `GRU,BRC,10`). Essas rotas são armazenadas em um arquivo de entrada (por exemplo, `rotas.txt`) e carregadas em memória para as consultas futuras.

-  **Consulta de Melhor Rota:**
   Dada uma rota de consulta no formato `Origem-Destino` (por exemplo, `GRU-CDG`), o sistema retorna a melhor rota disponível e o custo total (por exemplo, `GRU - BRC - SCL - ORL - CDG ao custo de $40`).

-  **Testes Unitários:**
   A aplicação conta com um projeto de testes unitários para validar a funcionalidade da busca pela melhor rota e outros cenários.

## Como Executar a Aplicação

### Requisitos

-  [.NET SDK](https://dotnet.microsoft.com/download)

### Passos para Execução

1. **Clone o repositório** para sua máquina.
2. **Compile a solução:**
   ```bash
   dotnet build
   ```
3. **Execute a aplicação:**
   ```bash
   dotnet run --project TravelRoutes
   ```
4. **Utilize o menu interativo:**
   -  Ao iniciar a aplicação, será exibido um menu com as seguintes opções:
      -  **1 - Consultar melhor rota:**
         Exemplo: Digite `GRU-CDG` e o sistema retornará:
         ```
         Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
         ```
      -  **2 - Registrar nova rota:**
         Exemplo: Digite `GRU,BRC,10` para cadastrar uma nova rota.
      -  **3 - Sair:**
         Encerra a aplicação.

### Executando os Testes Unitários

No diretório raiz da solução, execute:

```bash
dotnet test
```

Isso rodará os testes definidos no projeto TravelRoutes.Tests e validará o funcionamento da lógica de busca da melhor rota.

## Estrutura dos Arquivos / Pastas

A solução está organizada em dois projetos principais:

-  **TravelRoutes/** (Projeto Principal)

   -  **Program.cs:**
      Ponto de entrada da aplicação. Contém o menu interativo para consulta e registro de rotas.
   -  **Graph.cs:**
      Implementa a estrutura do grafo e a lógica de busca da melhor rota utilizando DFS com backtracking.
   -  **Edge.cs:**
      Define a classe que representa uma rota (ou aresta) entre dois pontos, contendo Origem, Destino e Custo.
   -  **RouteRepository.cs:**
      Gerencia a persistência das rotas em um arquivo de texto (`rotas.txt`), possibilitando a leitura e gravação de rotas.

-  **TravelRoutes.Tests/** (Projeto de Testes Unitários)
   -  **GraphTests.cs:**
      Contém os testes unitários que validam a busca da melhor rota e outros cenários (por exemplo, consulta de rota inexistente).

## Decisões de Design

-  **Algoritmo de Busca:**
   Para atender à restrição de não utilizar Dijkstra, a busca pela rota de menor custo foi implementada utilizando uma abordagem de DFS (busca em profundidade) com backtracking. Essa técnica percorre todas as possibilidades de caminho, evitando ciclos e descartando rotas cujo custo parcial já ultrapasse o melhor custo encontrado.

-  **Persistência em Arquivo:**
   As rotas são armazenadas em um arquivo de texto (`rotas.txt`). Essa escolha permite persistir os dados sem a necessidade de um banco de dados ou frameworks externos, mantendo a simplicidade da aplicação.

-  **Modularização e Testabilidade:**
   O código foi organizado em classes com responsabilidades bem definidas (representação de rota, lógica de grafo, persistência de dados), facilitando a manutenção e a realização de testes unitários que asseguram a confiabilidade da solução.

## Considerações Finais

Esta aplicação demonstra uma solução prática para o problema de encontrar a rota de viagem mais barata entre dois pontos, considerando múltiplas conexões, e permite o registro e persistência de novas rotas. A abordagem adotada garante que, mesmo sem o uso de algoritmos avançados como o Dijkstra, a melhor rota possa ser identificada por meio de uma exploração completa dos caminhos possíveis.
