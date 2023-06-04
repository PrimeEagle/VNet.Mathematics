using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.GameTheory
{
    public class Negamax
    {
        public delegate List<object> GetMovesDelegate(object state);
        public delegate int EvaluateDelegate(object state);
        public delegate object MakeMoveDelegate(object state, object move);
        public delegate bool GameOverDelegate(object state);

        private GetMovesDelegate getMoves;
        private EvaluateDelegate evaluate;
        private MakeMoveDelegate makeMove;
        private GameOverDelegate gameOver;

        public Negamax(GetMovesDelegate getMoves, EvaluateDelegate evaluate, MakeMoveDelegate makeMove, GameOverDelegate gameOver)
        {
            this.getMoves = getMoves;
            this.evaluate = evaluate;
            this.makeMove = makeMove;
            this.gameOver = gameOver;
        }

        public object BestMove(object state, int depth)
        {
            object bestMove = null;
            int bestScore = int.MinValue;

            foreach (object move in getMoves(state))
            {
                object newState = makeMove(state, move);
                int score = -NegamaxCore(newState, depth - 1);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        private int NegamaxCore(object state, int depth)
        {
            if (depth == 0 || gameOver(state))
            {
                return evaluate(state);
            }

            int maxScore = int.MinValue;

            foreach (object move in getMoves(state))
            {
                object newState = makeMove(state, move);
                int score = -NegamaxCore(newState, depth - 1);
                maxScore = Math.Max(maxScore, score);
            }

            return maxScore;
        }
    }

}
