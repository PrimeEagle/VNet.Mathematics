public class Automaton1D<TState, TRule> : IAutomaton<Cell<TState>, TState, TRule>
    where TRule : IRule<TState>
{
    public Cell<TState>[] Grid { get; set; }
    public TRule Rule { get; set; }

    public Automaton1D(int size, TRule rule)
    {
        Rule = rule;
        InitializeGrid(size);
    }

    public void InitializeGrid(int size)
    {
        Grid = new Cell<TState>[size];
        for (int i = 0; i < size; i++)
        {
            Grid[i] = new Cell<TState>(default); // Initialize each cell with the default state of TState.
        }
    }

    public void InitializeAutomaton(IAutomatonInitialization<TState> initialization)
    {
        int size = Grid.Length;

        for (int i = 0; i < size; i++)
        {
            Grid[i].UpdateState(initialization.GetInitialState(i));
        }
    }

    public void UpdateAutomaton()
    {
        int size = Grid.Length;

        Cell<TState>[] newGrid = new Cell<TState>[size];

        for (int i = 0; i < size; i++)
        {
            TState[] neighbors = GetNeighbors(i);

            TState currentState = Grid[i].State;
            TState newState = Rule.ApplyRule(currentState, neighbors);

            newGrid[i] = new Cell<TState>(newState);
        }

        Grid = newGrid;
    }

    private TState[] GetNeighbors(int index)
    {
        int size = Grid.Length;

        List<TState> neighbors = new List<TState>();

        for (int i = -1; i <= 1; i++)
        {
            int neighborIndex = (index + i + size) % size;

            TState neighborState = Grid[neighborIndex].State;
            neighbors.Add(neighborState);
        }

        return neighbors.ToArray();
    }
}