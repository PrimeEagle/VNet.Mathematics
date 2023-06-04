using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.DiscreteMath.CellularAutomata
{
    using System;

    public class Cell<TState> : ICell<TState>
    {
        public TState State { get; set; }
    }

    public interface IRule<TState>
    {
        TState GetNextState(TState currentState, int liveNeighbors);
    }

    public class ConwayRule : IRule<bool>
    {
        public bool GetNextState(bool currentState, int liveNeighbors)
        {
            if (currentState)
            {
                if (liveNeighbors < 2 || liveNeighbors > 3)
                    return false;
                else
                    return true;
            }
            else
            {
                if (liveNeighbors == 3)
                    return true;
                else
                    return false;
            }
        }
    }

    public class GameOfLifeAutomaton : IAutomaton<Cell<bool>, bool, ConwayRule>
    {
        public Cell<bool>[,] Grid { get; private set; }
        private int width;
        private int height;
        private ConwayRule rule;

        public GameOfLifeAutomaton(int width, int height, ConwayRule rule)
        {
            this.width = width;
            this.height = height;
            this.rule = rule;
            Grid = new Cell<bool>[width, height];
        }

        public void InitializeGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = new Cell<bool> { State = false };
                }
            }
        }

        public void UpdateAutomaton()
        {
            var newGrid = new Cell<bool>[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);
                    bool currentState = Grid[x, y].State;
                    bool nextState = rule.GetNextState(currentState, liveNeighbors);

                    newGrid[x, y] = new Cell<bool> { State = nextState };
                }
            }

            Grid = newGrid;
        }

        private int CountLiveNeighbors(int x, int y)
        {
            int liveNeighbors = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int neighborX = x + dx;
                    int neighborY = y + dy;

                    if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                    {
                        if (Grid[neighborX, neighborY].State)
                            liveNeighbors++;
                    }
                }
            }

            return liveNeighbors;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int width = 40;
            int height = 20;
            int updateDelay = 500; // 500ms delay between updates

            // Create the Game of Life automaton
            var rule = new ConwayRule();
            var automaton = new GameOfLifeAutomaton(width, height, rule);

            // Create the Cellular Automaton Manager
            var automatonManager = new CellularAutomatonManager<Cell<bool>, bool, ConwayRule>(automaton, updateDelay, GetCellCharacter);

            // Start the automaton
            automatonManager.Start();

            // Wait for user input to stop the automaton
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();

            // Stop the automaton
            automatonManager.Stop();
        }

        private static (char, ConsoleColor) GetCellCharacter(bool state)
        {
            if (state)
                return ('X', ConsoleColor.White);
            else
                return (' ', ConsoleColor.Black);
        }
    }

}
