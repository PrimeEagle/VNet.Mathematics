//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.GameTheory
//{
//    public class Minimax
//    {
//        public delegate List<object> GetMovesDelegate(object state);
//        public delegate int EvaluateDelegate(object state);
//        public delegate object MakeMoveDelegate(object state, object move);
//        public delegate bool GameOverDelegate(object state);

//        private GetMovesDelegate getMoves;
//        private EvaluateDelegate evaluate;
//        private MakeMoveDelegate makeMove;
//        private GameOverDelegate gameOver;

//        public Minimax(GetMovesDelegate getMoves, EvaluateDelegate evaluate, MakeMoveDelegate makeMove, GameOverDelegate gameOver)
//        {
//            this.getMoves = getMoves;
//            this.evaluate = evaluate;
//            this.makeMove = makeMove;
//            this.gameOver = gameOver;
//        }

//        public object BestMove(object state, int depth, bool maximizingPlayer)
//        {
//            int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;
//            object bestMove = null;

//            foreach (object move in getMoves(state))
//            {
//                object newState = makeMove(state, move);
//                int score = MinimaxCore(newState, depth - 1, !maximizingPlayer);
//                if (maximizingPlayer && score > bestScore)
//                {
//                    bestScore = score;
//                    bestMove = move;
//                }
//                else if (!maximizingPlayer && score < bestScore)
//                {
//                    bestScore = score;
//                    bestMove = move;
//                }
//            }

//            return bestMove;
//        }

//        private int MinimaxCore(object state, int depth, bool maximizingPlayer)
//        {
//            if (depth == 0 || gameOver(state))
//            {
//                return evaluate(state);
//            }

//            int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;

//            foreach (object move in getMoves(state))
//            {
//                object newState = makeMove(state, move);
//                int score = MinimaxCore(newState, depth - 1, !maximizingPlayer);
//                if (maximizingPlayer && score > bestScore)
//                {
//                    bestScore = score;
//                }
//                else if (!maximizingPlayer && score < bestScore)
//                {
//                    bestScore = score;
//                }
//            }

//            return bestScore;
//        }
//    }

//}
