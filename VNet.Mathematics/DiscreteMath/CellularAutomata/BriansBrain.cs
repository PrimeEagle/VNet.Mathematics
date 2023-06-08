//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.DiscreteMath.CellularAutomata
//{
//    using System;

//    public enum BrianState
//    {
//        Dead,
//        Dying,
//        Alive
//    }

//    public class Cell<TState> : ICell<TState>
//    {
//        public TState State { get; set; }
//    }

//    public interface IRule<TState>
//    {
//        TState GetNextState(TState currentState, int aliveNeighbors);
//    }

//    public class BrianRule : IRule<BrianState>
//    {
//        public BrianState GetNextState(BrianState currentState, int aliveNeighbors)
//        {
//            if (currentState == BrianState.Dead && aliveNeighbors == 2)
//                return BrianState.Alive;
//            else if (currentState == BrianState.Alive || currentState == BrianState.Dying)
//                return BrianState.Dying;
//            else
//                return BrianState.Dead;
//        }
//    }

//    public class BriansBrainAutomaton : IAutomaton<Cell<BrianState>, BrianState, BrianRule>
//    {
//        public Cell<BrianState>[,] Grid { get; private set; }
//        private int width;
//        private int height;
//        private BrianRule rule;

//        public BriansBrainAutomaton(int width, int height, BrianRule rule)
//        {
//            this.width = width;
//            this.height = height;
//            this.rule = rule;
//            Grid = new Cell<BrianState>[width, height];
//        }

//        public void InitializeGrid()
//        {
//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    Grid[x, y] = new Cell<BrianState> { State = BrianState.Dead };
//                }
//            }
//        }

//        public void UpdateAutomaton()
//        {
//            var newGrid = new Cell<BrianState>[width, height];

//            for (int x = 0; x < width; x++)
//            {
//                for (int y = 0; y < height; y++)
//                {
//                    int aliveNeighbors = CountAliveNeighbors(x, y);
//                    BrianState currentState = Grid[x, y].State;
//                    BrianState nextState = rule.GetNextState(currentState, aliveNeighbors);

//                    newGrid[x, y] = new Cell<BrianState> { State = nextState };
//                }
//            }

//            Grid = newGrid;
//        }

//        private int CountAliveNeighbors(int x, int y)
//        {
//            int aliveNeighbors = 0;

//            for (int dx = -1; dx <= 1; dx++)
//            {
//                for (int dy = -1; dy <= 1; dy++)
//                {
//                    if (dx == 0 && dy == 0)
//                        continue;

//                    int neighborX = x + dx;
//                    int neighborY = y + dy;

//                    if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
//                    {
//                        if (Grid[neighborX, neighborY].State == BrianState.Alive)
//                            aliveNeighbors++;
//                    }
//                }
//            }

//            return aliveNeighbors;
//        }
//    }

//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            int width = 40;
//            int height = 20;
//            int updateDelay = 500; // 500ms delay between updates

//            // Create the Brian's Brain automaton
//            var rule = new BrianRule();
//            var automaton = new BriansBrainAutomaton(width, height, rule);

//            // Create the Cellular Automaton Manager
//            var automatonManager = new CellularAutomatonManager<Cell<BrianState>, BrianState, BrianRule>(automaton, updateDelay, GetCellCharacter);

//            // Start the automaton
//            automatonManager.Start();

//            // Wait for user input to stop the automaton
//            Console.WriteLine("Press any key to stop...");
//            Console.ReadKey();

//            // Stop the automaton
//            automatonManager.Stop();
//        }

//        private static (char, ConsoleColor) GetCellCharacter(BrianState state)
//        {
//            switch (state)
//            {
//                case BrianState.Dead:
//                    return (' ', ConsoleColor.Black);
//                case BrianState.Dying:
//                    return ('*', ConsoleColor.Red);
//                case BrianState.Alive:
//                    return ('*', ConsoleColor.Green);
//                default:
//                    return (' ', ConsoleColor.Black);
//            }
//        }
//    }

//}
