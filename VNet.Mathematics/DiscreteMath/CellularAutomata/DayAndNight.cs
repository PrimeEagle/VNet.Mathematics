using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.DiscreteMath.CellularAutomata
{
    using System;

    public enum DayAndNightState
    {
        Dead,
        Alive
    }

    public class DayAndNightCell : ICell<DayAndNightState>
    {
        public DayAndNightState State { get; set; }
    }

    public class DayAndNightRule : IRule<DayAndNightState>
    {
        public DayAndNightState GetNextState(DayAndNightState[] neighborStates)
        {
            int aliveCount = CountAliveNeighbors(neighborStates);

            if (neighborStates[1] == DayAndNightState.Alive)
            {
                return aliveCount == 3 || aliveCount == 4 || aliveCount == 6 || aliveCount == 7 || aliveCount == 8
                    ? DayAndNightState.Alive
                    : DayAndNightState.Dead;
            }
            else
            {
                return aliveCount == 3 || aliveCount == 6 || aliveCount == 7 || aliveCount == 8
                    ? DayAndNightState.Alive
                    : DayAndNightState.Dead;
            }
        }

        private int CountAliveNeighbors(DayAndNightState[] neighborStates)
        {
            int count = 0;
            foreach (DayAndNightState state in neighborStates)
            {
                if (state == DayAndNightState.Alive)
                    count++;
            }
            return count;
        }
    }

    public class DayAndNightAutomaton : IAutomaton<DayAndNightCell, DayAndNightState, DayAndNightRule>
    {
        public DayAndNightCell[,] Grid { get; private set; }
        private DayAndNightRule rule;
        private int width;
        private int height;

        public DayAndNightAutomaton(int width, int height, DayAndNightRule rule)
        {
            this.width = width;
            this.height = height;
            this.rule = rule;
            Grid = new DayAndNightCell[width, height];
        }

        public void InitializeGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = new DayAndNightCell { State = DayAndNightState.Dead };
                }
            }
        }

        public void UpdateAutomaton()
        {
            DayAndNightCell[,] newGrid = new DayAndNightCell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    DayAndNightState[] neighborStates = GetNeighborStates(x, y);
                    DayAndNightState nextState = rule.GetNextState(neighborStates);

                    newGrid[x, y] = new DayAndNightCell { State = nextState };
                }
            }

            Grid = newGrid;
        }

        private DayAndNightState[] GetNeighborStates(int x, int y)
        {
            DayAndNightState[] neighborStates = new DayAndNightState[8];

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int neighborX = (x + dx + width) % width;
                    int neighborY = (y + dy + height) % height;

                    neighborStates[(dx + 1) * 3 + (dy + 1)] = Grid[neighborX, neighborY].State;
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
            int iterations = 100;
            int updateDelay = 200; // Delay between updates in milliseconds

            // Create the Day and Night automaton
            var rule = new DayAndNightRule();
            var automaton = new DayAndNightAutomaton(width, height, rule);

            // Create the Cellular Automaton Manager
            var automatonManager = new CellularAutomatonManager<DayAndNightCell, DayAndNightState, DayAndNightRule>(automaton, updateDelay, GetCellCharacter);

            // Initialize the automaton grid
            automaton.InitializeGrid();

            // Run the automaton for the specified number of iterations
            for (int i = 0; i < iterations; i++)
            {
                automatonManager.DisplayAutomaton();
                automatonManager.UpdateAutomaton();
            }
        }

        private static (char, ConsoleColor) GetCellCharacter(DayAndNightState state)
        {
            switch (state)
            {
                case DayAndNightState.Dead:
                    return ('.', ConsoleColor.Black);
                case DayAndNightState.Alive:
                    return ('*', ConsoleColor.White);
                default:
                    return ('.', ConsoleColor.Black);
            }
        }
    }

}
