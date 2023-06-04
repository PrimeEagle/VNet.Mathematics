using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.DiscreteMath.CellularAutomata
{
    using System;

    public enum Rule110State
    {
        Off,
        On
    }

    public class Cell<TState> : ICell<TState>
    {
        public TState State { get; set; }
    }

    public interface IRule<TState>
    {
        TState GetNextState(TState[] neighborStates);
    }

    public class Rule110Rule : IRule<Rule110State>
    {
        public Rule110State GetNextState(Rule110State[] neighborStates)
        {
            if (neighborStates[0] == Rule110State.On && neighborStates[1] == Rule110State.On && neighborStates[2] == Rule110State.On)
                return Rule110State.Off;
            else if (neighborStates[0] == Rule110State.On && neighborStates[1] == Rule110State.On && neighborStates[2] == Rule110State.Off)
                return Rule110State.On;
            else if (neighborStates[0] == Rule110State.On && neighborStates[1] == Rule110State.Off && neighborStates[2] == Rule110State.On)
                return Rule110State.On;
            else if (neighborStates[0] == Rule110State.On && neighborStates[1] == Rule110State.Off && neighborStates[2] == Rule110State.Off)
                return Rule110State.On;
            else if (neighborStates[0] == Rule110State.Off && neighborStates[1] == Rule110State.On && neighborStates[2] == Rule110State.On)
                return Rule110State.Off;
            else if (neighborStates[0] == Rule110State.Off && neighborStates[1] == Rule110State.On && neighborStates[2] == Rule110State.Off)
                return Rule110State.On;
            else if (neighborStates[0] == Rule110State.Off && neighborStates[1] == Rule110State.Off && neighborStates[2] == Rule110State.On)
                return Rule110State.On;
            else
                return Rule110State.Off;
        }
    }

    public class Rule110Automaton : IAutomaton<Cell<Rule110State>, Rule110State, Rule110Rule>
    {
        public Cell<Rule110State>[,] Grid { get; private set; }
        private int width;
        private int height;
        private Rule110Rule rule;

        public Rule110Automaton(int width, int height, Rule110Rule rule)
        {
            this.width = width;
            this.height = height;
            this.rule = rule;
            Grid = new Cell<Rule110State>[width, height];
        }

        public void InitializeGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = new Cell<Rule110State> { State = Rule110State.Off };
                }
            }

            Grid[width / 2, 0].State = Rule110State.On; // Set the initial state in the top center cell
        }

        public void UpdateAutomaton()
        {
            var newGrid = new Cell<Rule110State>[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Rule110State[] neighborStates = GetNeighborStates(x, y);
                    Rule110State nextState = rule.GetNextState(neighborStates);

                    newGrid[x, y] = new Cell<Rule110State> { State = nextState };
                }
            }

            Grid = newGrid;
        }

        private Rule110State[] GetNeighborStates(int x, int y)
        {
            Rule110State[] neighborStates = new Rule110State[3];

            int prevX = (x - 1 + width) % width;
            int nextX = (x + 1) % width;

            neighborStates[0] = Grid[prevX, y].State;
            neighborStates[1] = Grid[x, y].State;
            neighborStates[2] = Grid[nextX, y].State;

            return neighborStates;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int width = 80;
            int height = 20;
            int updateDelay = 100; // 100ms delay between updates

            // Create the Rule 110 automaton
            var rule = new Rule110Rule();
            var automaton = new Rule110Automaton(width, height, rule);

            // Create the Cellular Automaton Manager
            var automatonManager = new CellularAutomatonManager<Cell<Rule110State>, Rule110State, Rule110Rule>(automaton, updateDelay, GetCellCharacter);

            // Start the automaton
            automatonManager.Start();

            // Wait for user input to stop the automaton
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();

            // Stop the automaton
            automatonManager.Stop();
        }

        private static (char, ConsoleColor) GetCellCharacter(Rule110State state)
        {
            switch (state)
            {
                case Rule110State.Off:
                    return (' ', ConsoleColor.Black);
                case Rule110State.On:
                    return ('#', ConsoleColor.White);
                default:
                    return (' ', ConsoleColor.Black);
            }
        }
    }

}
