public class CellularAutomatonManager<TCell, TState, TRule> where TCell : ICell<TState>
    where TRule : IRule<TState>
{
    private IAutomaton<TCell, TState, TRule> automaton;
    private bool isRunning;
    private bool isPaused;
    private int updateDelay;
    private int pauseDelay;

    public class CellularAutomatonManager<TCell, TState, TRule> where TCell : ICell<TState>
        where TRule : IRule<TState>
    {
        private IAutomaton<TCell, TState, TRule> automaton;
        private bool isRunning;
        private bool isPaused;
        private int updateDelay;
        private Func<TState, (char, ConsoleColor)> getCellCharacter;

        public CellularAutomatonManager(IAutomaton<TCell, TState, TRule> automaton, int updateDelay = 100, Func<TState, (char, ConsoleColor)> getCellCharacter = null)
        {
            this.automaton = automaton;
            this.updateDelay = updateDelay;
            this.getCellCharacter = getCellCharacter ?? DefaultGetCellCharacter;
            isRunning = false;
            isPaused = false;
        }

        public void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                isPaused = false;
                RunAutomaton();
            }
        }

        public void Stop()
        {
            isRunning = false;
            isPaused = false;
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Reset()
        {
            Stop();
            InitializeAutomaton();
        }

        private void InitializeAutomaton()
        {
            automaton.InitializeGrid();
        }

        private void RunAutomaton()
        {
            Task.Run(async () =>
            {
                while (isRunning)
                {
                    if (!isPaused)
                    {
                        automaton.UpdateAutomaton();
                        DisplayAutomaton();
                        await Task.Delay(updateDelay); // Delay between each update
                    }
                    else
                    {
                        await Task.Delay(100); // Delay while paused
                    }
                }
            });
        }

        private void DisplayAutomaton()
        {
            int width = automaton.Grid.GetLength(0);
            int height = automaton.Grid.GetLength(1);

            Console.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    TState state = automaton.Grid[x, y].State;
                    (char character, ConsoleColor color) = getCellCharacter(state);

                    Console.ForegroundColor = color;
                    Console.Write(character);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        private (char, ConsoleColor) DefaultGetCellCharacter(TState state)
        {
            // Default implementation that assumes the state is a bool (true or false)
            // Uses 'X' for true with ConsoleColor.White foreground color,
            // and ' ' for false with ConsoleColor.Black foreground color
            if (state)
                return ('X', ConsoleColor.White);
            else
                return (' ', ConsoleColor.Black);
        }
    }