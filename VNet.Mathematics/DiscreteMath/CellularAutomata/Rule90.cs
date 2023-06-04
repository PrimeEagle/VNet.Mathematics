using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.DiscreteMath.CellularAutomata
{
    using System;

    public class Rule90Cell : ICell<bool>
    {
        public bool State { get; set; }
    }

    public class Rule90Automaton : IAutomaton<Rule90Cell, bool, Rule90Rule>
    {
        public Rule90Cell[] Grid { get; private set; }
        private Rule90Rule rule;

        public Rule90Automaton(int size, Rule90Rule rule)
        {
            Grid = new Rule90Cell[size];
            this.rule = rule;
        }

        public void InitializeGrid()
        {
            for (int i = 0; i < Grid.Length; i++)
            {
                Grid[i] = new Rule90Cell { State = false };
            }

            Grid[Grid.Length / 2].State = true; // Set the initial state in the middle cell
        }

        public void UpdateAutomaton()
        {
            Rule90Cell[] newGrid = new Rule90Cell[Grid.Length];

            for (int i = 0; i < Grid.Length; i++)
            {
                bool[] neighborStates = GetNeighborStates(i);
                bool nextState = rule.GetNextState(neighborStates);

                newGrid[i] = new Rule90Cell { State = nextState };
            }

            Grid = newGrid;
        }

        private bool[] GetNeighborStates(int index)
        {
            bool[] neighborStates = new bool[3];

            int prevIndex = (index - 1 + Grid.Length) % Grid.Length;
            int nextIndex = (index + 1) % Grid.Length;

            neighborStates[0] = Grid[prevIndex].State;
            neighborStates[1] = Grid[index].State;
            neighborStates[2] = Grid[nextIndex].State;

            return neighborStates;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int size = 80; // Number of cells
            int iterations = 100; // Number of iterations
            int updateDelay = 100; // Delay between updates in milliseconds

            // Create the Rule 90 automaton
            var rule = new Rule90Rule();
            var automaton = new Rule90Automaton(size, rule);

            // Create the Cellular Automaton Manager
            var automatonManager = new CellularAutomatonManager<Rule90Cell, bool, Rule90Rule>(automaton, updateDelay, GetCellCharacter);

            // Initialize the automaton grid
            automaton.InitializeGrid();

            // Run the automaton for the specified number of iterations
            for (int i = 0; i < iterations; i++)
            {
                automatonManager.DisplayAutomaton();
                automatonManager.UpdateAutomaton();
            }
        }

        private static (char, ConsoleColor) GetCellCharacter(bool state)
        {
            if (state)
                return ('#', ConsoleColor.White);
            else
                return (' ', ConsoleColor.Black);
        }
    }

}
