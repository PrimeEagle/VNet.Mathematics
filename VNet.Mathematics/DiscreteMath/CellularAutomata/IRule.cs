public interface IRule<TState>
{
    TState ApplyRule(TState currentState, TState[] neighbors);
}