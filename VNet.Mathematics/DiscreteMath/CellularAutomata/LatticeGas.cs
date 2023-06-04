using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.DiscreteMath.CellularAutomata
{
    using System;

    public enum LatticeGasState
    {
        Empty,
        Particle
    }

    public class Cell<TState> : ICell<TState>
    {
        public TState State { get; set; }
    }

    public interface IRule<TState>
    {
        TState GetNextState(TState currentState, TState[] neighborStates);
    }

    public class LatticeGasRule : IRule<LatticeGasState>
    {
        public LatticeGasState GetNextState(LatticeGasState currentState, LatticeGasState[] neighborStates)
        {
            if (currentState == LatticeGasState.Empty && Array.TrueForAll(neighborStates, s => s == LatticeGasState.Empty))
                return LatticeGasState.Empty;
            else if (currentState == LatticeGasState.Particle && Array.Exists(neighborStates, s => s == LatticeGasState.Particle))
                return LatticeGasState.Particle;
            else
                return LatticeGasState.Empty;
        }
    }

    public class LatticeGasAutomaton : IAutomaton<Cell<LatticeGasState>, LatticeGasState, LatticeGasRule>
    {
        public Cell<LatticeGasState>[,] Grid { get; private set; }
        private int width;
        private int height;
        private LatticeGasRule rule;

        public LatticeGasAutomaton(int width, int height, LatticeGasRule rule)
        {
            this.width = width;
            this.height = height;
            this.rule = rule;
            Grid = new Cell<LatticeGasState>[width, height];
        }

        public void InitializeGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = new Cell<LatticeGasState> { State = LatticeGasState.Empty };
                }
            }
        }

        public void UpdateAutomaton()
        {
            var newGrid = new Cell<LatticeGasState>[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    LatticeGasState[] neighborStates = GetNeighborStates(x, y);
                    LatticeGasState currentState = Grid[x, y].State;
                    LatticeGasState nextState = rule.GetNextState(currentState, neighborStates);

                    newGrid[x, y] = new Cell<LatticeGasState> { State = nextState };
                }
            }

            Grid = newGrid;
        }

        private LatticeGasState[] GetNeighborStates(int x, int y)
        {
            LatticeGasState[] neighborStates = new LatticeGasState[8];

            int index = 0;
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int neighborX = x + dx;
                    int neighborY = y + dy;

                    if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                        neighborStates[index] = Grid[neighborX, neighborY].State;
                    else
                        neighborStates[index] = LatticeGasState.Empty;

                    index++;
                }
            }

            return neighborStates;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int width = 40;
            int height = 20;
            int updateDelay = 500; // 500ms delay between updates

            // Create the Lattice Gas automaton
            var rule = new LatticeGasRule();
            var automaton = new LatticeGasAutomaton(width, height, rule);

            // Create the Cellular Automaton Manager
            var automatonManager = new CellularAutomatonManager<Cell<LatticeGasState>, LatticeGasState, LatticeGasRule>(automaton, updateDelay, GetCellCharacter);

            // Start the automaton
            automatonManager.Start();

            // Wait for user input to stop the automaton
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();

            // Stop the automaton
            automatonManager.Stop();
        }

        private static (char, ConsoleColor) GetCellCharacter(LatticeGasState state)
        {
            switch (state)
            {
                case LatticeGasState.Empty:
                    return (' ', ConsoleColor.Black);
                case LatticeGasState.Particle:
                    return ('*', ConsoleColor.White);
                default:
                    return (' ', ConsoleColor.Black);
            }
        }
    }

}
