public interface IAutomaton<TCell, TState, TRule> where TCell : ICell<TState>
    where TRule : IRule<TState>
{
    TCell[,] Grid { get; set; } // This is a generic grid of cells.
    void InitializeGrid(params int[] dimensions); // Method to initialize the grid with default cells.
    void InitializeAutomaton(IAutomatonInitialization<TState> initialization); // Method to initialize the automaton with custom conditions.
    void UpdateAutomaton(); // Method to update the automaton (step forward in time).
}