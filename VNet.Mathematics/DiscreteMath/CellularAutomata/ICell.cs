public interface ICell<TState>
{
    TState State { get; set; } // This represents the state of the cell.
    void UpdateState(TState newState); // Method to update the state of the cell.
}