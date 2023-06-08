//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.DiscreteMath.CellularAutomata
//{
//    using System;

//    public enum Rule30State
//    {
//        Off,
//        On
//    }

//    public class Cell<TState> : ICell<TState>
//    {
//        public TState State { get; set; }
//    }

//    public interface IRule<TState>
//    {
//        TState GetNextState(TState[] neighborStates);
//    }

//    public class Rule30Rule : IRule<Rule30State>
//    {
//        public Rule30State GetNextState(Rule30State[] neighborStates)
//        {
//            bool isOn = neighborStates[0] == Rule30State.On && neighborStates[1] == Rule30State.On;
//            isOn ^= neighborStates[2] == Rule30State.On;
//            return isOn ? Rule30State.On : Rule30State.Off;
//        }
//    }

//    public class Rule30Automaton : IAutomaton<Cell<Rule30State>, Rule30State, Rule30Rule>
//    {
//        public Cell<Rule30State>[,] Grid { get; private set; }
//        private int width;
//        private int height;
//        private Rule30Rule rule;

//        public Rule30Automaton(int width, int height, Rule30Rule rule)
//        {
//            this.width = width;
//            this.height = height;
//            this.rule = rule;
//            Grid = new Cell<Rule30State>[width, height];
//        }

//        public void InitializeGrid()
//        {
//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    Grid[x, y] = new Cell<Rule30State> { State = Rule30State.Off };
//                }
//            }

//            Grid[width / 2, 0].State = Rule30State.On; // Set the initial state in the top center cell
//        }

//        public void UpdateAutomaton()
//        {
//            var newGrid = new Cell<Rule30State>[width, height];

//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    Rule30State[] neighborStates = GetNeighborStates(x, y);
//                    Rule30State nextState = rule.GetNextState(neighborStates);

//                    newGrid[x, y] = new Cell<Rule30State> { State = nextState };
//                }
//            }

//            Grid = newGrid;
//        }

//        private Rule30State[] GetNeighborStates(int x, int y)
//        {
//            Rule30State[] neighborStates = new Rule30State[3];

//            int prevX = (x - 1 + width) % width;
//            int nextX = (x + 1) % width;

//            neighborStates[0] = Grid[prevX, y].State;
//            neighborStates[1] = Grid[x, y].State;
//            neighborStates[2] = Grid[nextX, y].State;

//            return neighborStates;
//        }
//    }

//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            int width = 80;
//            int height = 20;
//            int updateDelay = 100; // 100ms delay between updates

//            // Create the Rule 30 automaton
//            var rule = new Rule30Rule();
//            var automaton = new Rule30Automaton(width, height, rule);

//            // Create the Cellular Automaton Manager
//            var automatonManager = new CellularAutomatonManager<Cell<Rule30State>, Rule30State, Rule30Rule>(automaton, updateDelay, GetCellCharacter);

//            // Start the automaton
//            automatonManager.Start();

//            // Wait for user input to stop the automaton
//            Console.WriteLine("Press any key to stop...");
//            Console.ReadKey();

//            // Stop the automaton
//            automatonManager.Stop();
//        }

//        private static (char, ConsoleColor) GetCellCharacter(Rule30State state)
//        {
//            switch (state)
//            {
//                case Rule30State.Off:
//                    return (' ', ConsoleColor.Black);
//                case Rule30State.On:
//                    return ('#', ConsoleColor.White);
//                default:
//                    return (' ', ConsoleColor.Black);
//            }
//        }
//    }

//}
