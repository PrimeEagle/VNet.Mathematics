public class Automaton3D<TState, TRule> : IAutomaton<Cell<TState>, TState, TRule>
    where TRule : IRule<TState>
{
    public Cell<TState>[,,] Grid { get; set; }
    public TRule Rule { get; set; }

    public Automaton3D(int width, int height, int depth, TRule rule)
    {
        Rule = rule;
        InitializeGrid(width, height, depth);
    }

    public void InitializeGrid(params int[] dimensions)
    {
        int width = dimensions[0];
        int height = dimensions[1];
        int depth = dimensions[2];

        Grid = new Cell<TState>[width, height, depth];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    Grid[i, j, k] = new Cell<TState>(default); // Initialize each cell with the default state of TState.
                }
            }
        }
    }

    public void InitializeAutomaton(IAutomatonInitialization<TState> initialization)
    {
        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);
        int depth = Grid.GetLength(2);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    Grid[i, j, k].UpdateState(initialization.GetInitialState(i, j, k));
                }
            }
        }
    }

    public void UpdateAutomaton()
    {
        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);
        int depth = Grid.GetLength(2);

        Cell<TState>[,,] newGrid = new Cell<TState>[width, height, depth];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    TState[] neighbors = GetNeighbors(i, j, k);

                    TState currentState = Grid[i, j, k].State;
                    TState newState = Rule.ApplyRule(currentState, neighbors);

                    newGrid[i, j, k] = new Cell<TState>(newState);
                }
            }
        }

        Grid = newGrid;
    }

    private TState[] GetNeighbors(int x, int y, int z)
    {
        int width = Grid.GetLength(0);
        int height = Grid.GetLength(1);
        int depth = Grid.GetLength(2);

        List<TState> neighbors = new List<TState>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int neighborX = (x + i + width) % width;
                    int neighborY = (y + j + height) % height;
                    int neighborZ = (z + k + depth) % depth;

                    TState neighborState = Grid[neighborX, neighborY, neighborZ].State;
                    neighbors.Add(neighborState);
                }
            }
        }

        return neighbors.ToArray();
    }
}