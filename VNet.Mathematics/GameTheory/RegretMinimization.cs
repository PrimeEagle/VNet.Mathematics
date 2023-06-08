//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.GameTheory
//{
//    public class RegretMinimization
//    {
//        public delegate List<object> GetMovesDelegate(object state);
//        public delegate int EvaluateDelegate(object state);
//        public delegate object MakeMoveDelegate(object state, object move);
//        public delegate bool GameOverDelegate(object state);

//        private GetMovesDelegate getMoves;
//        private EvaluateDelegate evaluate;
//        private MakeMoveDelegate makeMove;
//        private GameOverDelegate gameOver;

//        private Dictionary<object, double> cumulativeRegrets;

//        public RegretMinimization(GetMovesDelegate getMoves, EvaluateDelegate evaluate, MakeMoveDelegate makeMove, GameOverDelegate gameOver)
//        {
//            this.getMoves = getMoves;
//            this.evaluate = evaluate;
//            this.makeMove = makeMove;
//            this.gameOver = gameOver;
//            this.cumulativeRegrets = new Dictionary<object, double>();
//        }

//        public object BestMove(object state)
//        {
//            object bestMove = null;
//            double leastRegret = double.MaxValue;

//            foreach (object move in getMoves(state))
//            {
//                object newState = makeMove(state, move);
//                double regret = EvaluateRegret(newState);

//                if (regret < leastRegret)
//                {
//                    leastRegret = regret;
//                    bestMove = move;
//                }
//            }

//            return bestMove;
//        }

//        private double EvaluateRegret(object state)
//        {
//            double regret = 0;

//            if (!gameOver(state))
//            {
//                regret = evaluate(state);

//                if (cumulativeRegrets.TryGetValue(state, out double previousRegret))
//                {
//                    regret += previousRegret;
//                }

//                cumulativeRegrets[state] = regret;
//            }

//            return regret;
//        }
//    }

//}
