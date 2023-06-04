using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.GameTheory
{
    public class FictitiousPlay
    {
        public delegate List<object> GetMovesDelegate(object state);
        public delegate int EvaluateDelegate(object state);
        public delegate object MakeMoveDelegate(object state, object move);
        public delegate bool GameOverDelegate(object state);

        private GetMovesDelegate getMoves;
        private EvaluateDelegate evaluate;
        private MakeMoveDelegate makeMove;
        private GameOverDelegate gameOver;

        private Dictionary<object, double> strategyFrequencies;

        public FictitiousPlay(GetMovesDelegate getMoves, EvaluateDelegate evaluate, MakeMoveDelegate makeMove, GameOverDelegate gameOver)
        {
            this.getMoves = getMoves;
            this.evaluate = evaluate;
            this.makeMove = makeMove;
            this.gameOver = gameOver;
            this.strategyFrequencies = new Dictionary<object, double>();
        }

        public object BestMove(object state)
        {
            object bestMove = null;
            double highestFrequency = double.MinValue;

            foreach (object move in getMoves(state))
            {
                double frequency = GetStrategyFrequency(move);

                if (frequency > highestFrequency)
                {
                    highestFrequency = frequency;
                    bestMove = move;
                }
            }

            UpdateStrategyFrequencies(state);

            return bestMove;
        }

        private double GetStrategyFrequency(object move)
        {
            if (!strategyFrequencies.ContainsKey(move))
            {
                return 0;
            }

            return strategyFrequencies[move];
        }

        private void UpdateStrategyFrequencies(object state)
        {
            foreach (object move in getMoves(state))
            {
                if (!strategyFrequencies.ContainsKey(move))
                {
                    strategyFrequencies[move] = 0;
                }

                object newState = makeMove(state, move);
                strategyFrequencies[move] += evaluate(newState);
            }
        }
    }

}
