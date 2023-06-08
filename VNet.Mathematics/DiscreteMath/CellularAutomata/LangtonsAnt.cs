//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.DiscreteMath.CellularAutomata
//{
//    using System;

//    public enum LangtonAntState
//    {
//        White,
//        Black
//    }

//    public class LangtonAntCell : ICell<LangtonAntState>
//    {
//        public LangtonAntState State { get; set; }
//    }

//    public class LangtonAntRule : IRule<LangtonAntState>
//    {
//        public LangtonAntState GetNextState(LangtonAntState[] neighborStates)
//        {
//            return neighborStates[0] == LangtonAntState.Black ? LangtonAntState.White : LangtonAntState.Black;
//        }
//    }

//    public class LangtonAntAutomaton : IAutomaton<LangtonAntCell, LangtonAntState, LangtonAntRule>
//    {
//        public LangtonAntCell[,] Grid { get; private set; }
//        private LangtonAntRule rule;
//        private int width;
//        private int height;
//        private int antX;
//        private int antY;
//        private int antDirection; // 0: North, 1: East, 2: South, 3: West

//        public LangtonAntAutomaton(int width, int height, LangtonAntRule rule)
//        {
//            this.width = width;
//            this.height = height;
//            this.rule = rule;
//            Grid = new LangtonAntCell[width, height];
//            antX = width / 2;
//            antY = height / 2;
//            antDirection = 0;
//        }

//        public void InitializeGrid()
//        {
//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    Grid[x, y] = new LangtonAntCell { State = LangtonAntState.White };
//                }
//            }
//        }

//        public void UpdateAutomaton()
//        {
//            LangtonAntState currentState = Grid[antX, antY].State;
//            Grid[antX, antY].State = rule.GetNextState(new[] { currentState });

//            // Turn the ant
//            if (currentState == LangtonAntState.Black)
//                antDirection = (antDirection + 1) % 4;
//            else
//                antDirection = (antDirection - 1 + 4) % 4;

//            // Move the ant forward
//            switch (antDirection)
//            {
//                case 0: // North
//                    antY = (antY - 1 + height) % height;
//                    break;
//                case 1: // East
//                    antX = (antX + 1) % width;
//                    break;
//                case 2: // South
//                    antY = (antY + 1) % height;
//                    break;
//                case 3: // West
//                    antX = (antX - 1 + width) % width;
//                    break;
//            }
//        }
//    }

//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            int width = 40;
//            int height = 40;
//            int iterations = 500;
//            int updateDelay = 100; // Delay between updates in milliseconds

//            // Create the Langton's Ant automaton
//            var rule = new LangtonAntRule();
//            var automaton = new LangtonAntAutomaton(width, height, rule);

//            // Create the Cellular Automaton Manager
//            var automatonManager = new CellularAutomatonManager<LangtonAntCell, LangtonAntState, LangtonAntRule>(automaton, updateDelay, GetCellCharacter);

//            // Initialize the automaton grid
//            automaton.InitializeGrid();

//            // Run the automaton for the specified number of iterations
//            for (int i = 0; i < iterations; i++)
//            {
//                automatonManager.DisplayAutomaton();
//                automatonManager.UpdateAutomaton();
//            }
//        }

//        private static (char, ConsoleColor) GetCellCharacter(LangtonAntState state)
//        {
//            switch (state)
//            {
//                case LangtonAntState.White:
//                    return (' ', ConsoleColor.White);
//                case LangtonAntState.Black:
//                    return ('█', ConsoleColor.Black);
//                default:
//                    return (' ', ConsoleColor.White);
//            }
//        }
//    }

//}
