//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.DiscreteMath.CellularAutomata
//{
//    using System;

//    public enum WireworldState
//    {
//        Empty,
//        ElectronHead,
//        ElectronTail,
//        Conductor
//    }

//    public class WireworldCell : ICell<WireworldState>
//    {
//        public WireworldState State { get; set; }
//    }

//    public class WireworldRule : IRule<WireworldState>
//    {
//        public WireworldState GetNextState(WireworldState[] neighborStates)
//        {
//            WireworldState currentState = neighborStates[1];
//            int electronHeadCount = CountElectronHead(neighborStates);

//            switch (currentState)
//            {
//                case WireworldState.Empty:
//                    return WireworldState.Empty;
//                case WireworldState.ElectronHead:
//                    return WireworldState.ElectronTail;
//                case WireworldState.ElectronTail:
//                    return WireworldState.Conductor;
//                case WireworldState.Conductor:
//                    return electronHeadCount == 1 || electronHeadCount == 2 ? WireworldState.ElectronHead : WireworldState.Conductor;
//                default:
//                    return WireworldState.Empty;
//            }
//        }

//        private int CountElectronHead(WireworldState[] neighborStates)
//        {
//            int count = 0;
//            foreach (WireworldState state in neighborStates)
//            {
//                if (state == WireworldState.ElectronHead)
//                    count++;
//            }
//            return count;
//        }
//    }

//    public class WireworldAutomaton : IAutomaton<WireworldCell, WireworldState, WireworldRule>
//    {
//        public WireworldCell[,] Grid { get; private set; }
//        private WireworldRule rule;
//        private int width;
//        private int height;

//        public WireworldAutomaton(int width, int height, WireworldRule rule)
//        {
//            this.width = width;
//            this.height = height;
//            this.rule = rule;
//            Grid = new WireworldCell[width, height];
//        }

//        public void InitializeGrid()
//        {
//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    Grid[x, y] = new WireworldCell { State = WireworldState.Empty };
//                }
//            }
//        }

//        public void UpdateAutomaton()
//        {
//            WireworldCell[,] newGrid = new WireworldCell[width, height];

//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    WireworldState[] neighborStates = GetNeighborStates(x, y);
//                    WireworldState nextState = rule.GetNextState(neighborStates);

//                    newGrid[x, y] = new WireworldCell { State = nextState };
//                }
//            }

//            Grid = newGrid;
//        }

//        private WireworldState[] GetNeighborStates(int x, int y)
//        {
//            WireworldState[] neighborStates = new WireworldState[8];

//            for (int dx = -1; dx <= 1; dx++)
//            {
//                for (int dy = -1; dy <= 1; dy++)
//                {
//                    if (dx == 0 && dy == 0)
//                        continue;

//                    int neighborX = (x + dx + width) % width;
//                    int neighborY = (y + dy + height) % height;

//                    neighborStates[(dx + 1) * 3 + (dy + 1)] = Grid[neighborX, neighborY].State;
//                }
//            }

//            return neighborStates;
//        }
//    }

//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            int width = 40;
//            int height = 20;
//            int iterations = 50;
//            int updateDelay = 200; // Delay between updates in milliseconds

//            // Create the Wireworld automaton
//            var rule = new WireworldRule();
//            var automaton = new WireworldAutomaton(width, height, rule);

//            // Create the Cellular Automaton Manager
//            var automatonManager = new CellularAutomatonManager<WireworldCell, WireworldState, WireworldRule>(automaton, updateDelay, GetCellCharacter);

//            // Initialize the automaton grid
//            automaton.InitializeGrid();

//            // Set up initial Wireworld elements (example: AND gate)
//            automaton.Grid[5, 5].State = WireworldState.ElectronHead;
//            automaton.Grid[5, 6].State = WireworldState.Conductor;
//            automaton.Grid[5, 7].State = WireworldState.ElectronHead;
//            automaton.Grid[6, 4].State = WireworldState.ElectronHead;
//            automaton.Grid[6, 8].State = WireworldState.ElectronHead;
//            automaton.Grid[7, 3].State = WireworldState.ElectronHead;
//            automaton.Grid[7, 9].State = WireworldState.ElectronHead;
//            automaton.Grid[8, 3].State = WireworldState.Conductor;
//            automaton.Grid[8, 9].State = WireworldState.Conductor;
//            automaton.Grid[9, 6].State = WireworldState.ElectronHead;
//            automaton.Grid[10, 4].State = WireworldState.Conductor;
//            automaton.Grid[10, 8].State = WireworldState.Conductor;
//            automaton.Grid[11, 5].State = WireworldState.Conductor;
//            automaton.Grid[11, 6].State = WireworldState.Conductor;
//            automaton.Grid[11, 7].State = WireworldState.Conductor;

//            // Run the automaton for the specified number of iterations
//            for (int i = 0; i < iterations; i++)
//            {
//                automatonManager.DisplayAutomaton();
//                automatonManager.UpdateAutomaton();
//            }
//        }

//        private static (char, ConsoleColor) GetCellCharacter(WireworldState state)
//        {
//            switch (state)
//            {
//                case WireworldState.Empty:
//                    return (' ', ConsoleColor.Black);
//                case WireworldState.ElectronHead:
//                    return ('O', ConsoleColor.Yellow);
//                case WireworldState.ElectronTail:
//                    return ('.', ConsoleColor.DarkYellow);
//                case WireworldState.Conductor:
//                    return ('=', ConsoleColor.Blue);
//                default:
//                    return (' ', ConsoleColor.Black);
//            }
//        }
//    }

//}
