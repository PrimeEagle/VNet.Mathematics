public class Automaton2D<TState, TRule> : IAutomaton<Cell<TState>, TState, TRule>
    where TRule : IRule<TState>
{
    public Cell<TState>[,] Grid { get; set; }
    public TRule Rule { get; set; }

    public Automaton2D(int width, int height, TRule rule)
    {
        Rule = rule;
        InitializeGrid(width, height);
    }

    public void InitializeGrid(params int[] dimensions)
    {
        int width = dimensions[0];
        int height = dimensions[1];

        Grid = new Cell<TState>[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Grid[i, j] = new Cell<TState>(default); // Initialize each cell with the default state of TState.
            }
        }
    }

    public void InitializeAutomaton(IAutomatonInitialization<TState> initialization)
    {
        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Grid[i, j].UpdateState(initialization.GetInitialState(i, j));
            }
        }
    }

    public void UpdateAutomaton()
    {
        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);

        Cell<TState>[,] newGrid = new Cell<TState>[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                TState[] neighbors = GetNeighbors(i, j);

                TState currentState = Grid[i, j].State;
                TState newState = Rule.ApplyRule(currentState, neighbors);

                newGrid[i, j] = new Cell<TState>(newState);
            }
        }

        Grid = newGrid;
    }

    private TState[] GetNeighbors(int x, int y)
    {
        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);

        List<TState> neighbors = new List<TState>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborX = (x + i + width) % width;
                int neighborY = (y + j + height) % height;

                TState neighborState = Grid[neighborX, neighborY].State;
                neighbors.Add(neighborState);
            }
        }

        return neighbors.ToArray();
    }
}