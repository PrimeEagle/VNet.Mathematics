using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.GameTheory
{
    public class AlphaBeta
    {
        private Minimax minimax;
        private int alpha;
        private int beta;

        public AlphaBeta(Minimax minimax)
        {
            this.minimax = minimax;
        }

        public object BestMove(object state, int depth, bool maximizingPlayer)
        {
            alpha = int.MinValue;
            beta = int.MaxValue;
            return AlphaBetaCore(state, depth, maximizingPlayer);
        }

        private object AlphaBetaCore(object state, int depth, bool maximizingPlayer)
        {
            int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;
            object bestMove = null;

            foreach (object move in minimax.GetMoves(state))
            {
                object newState = minimax.MakeMove(state, move);
                int score = minimax.MinimaxCore(newState, depth - 1, !maximizingPlayer);

                if (maximizingPlayer)
                {
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                    alpha = Math.Max(alpha, score);

                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                else
                {
                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestMove = move;
                    }
                    beta = Math.Min(beta, score);

                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }

            return bestMove;
        }
    }

}
