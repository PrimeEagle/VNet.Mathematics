//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.GameTheory
//{
//    public class QLearning
//    {
//        public delegate List<object> GetMovesDelegate(object state);
//        public delegate object MakeMoveDelegate(object state, object move);
//        public delegate bool GameOverDelegate(object state);
//        public delegate double GetRewardDelegate(object state);

//        private GetMovesDelegate getMoves;
//        private MakeMoveDelegate makeMove;
//        private GameOverDelegate gameOver;
//        private GetRewardDelegate getReward;

//        private Dictionary<(object state, object action), double> qValues;
//        private double learningRate;
//        private double discountFactor;

//        public QLearning(GetMovesDelegate getMoves, MakeMoveDelegate makeMove, GameOverDelegate gameOver, GetRewardDelegate getReward, double learningRate, double discountFactor)
//        {
//            this.getMoves = getMoves;
//            this.makeMove = makeMove;
//            this.gameOver = gameOver;
//            this.getReward = getReward;
//            this.learningRate = learningRate;
//            this.discountFactor = discountFactor;
//            this.qValues = new Dictionary<(object state, object action), double>();
//        }

//        public object BestMove(object state)
//        {
//            object bestMove = null;
//            double maxQValue = double.MinValue;

//            foreach (object move in getMoves(state))
//            {
//                double qValue = GetQValue(state, move);
//                if (qValue > maxQValue)
//                {
//                    maxQValue = qValue;
//                    bestMove = move;
//                }
//            }

//            return bestMove;
//        }

//        public void Learn(object state, object action)
//        {
//            double oldQValue = GetQValue(state, action);
//            object newState = makeMove(state, action);
//            double reward = getReward(newState);

//            double maxFutureQValue = gameOver(newState) ? 0 : GetMaxQValue(newState);
//            double learnedValue = reward + discountFactor * maxFutureQValue;
//            double newQValue = oldQValue + learningRate * (learnedValue - oldQValue);

//            qValues[(state, action)] = newQValue;
//        }

//        private double GetQValue(object state, object action)
//        {
//            if (!qValues.TryGetValue((state, action), out double qValue))
//            {
//                qValue = 0;
//            }

//            return qValue;
//        }

//        private double GetMaxQValue(object state)
//        {
//            double maxQValue = double.MinValue;

//            foreach (object move in getMoves(state))
//            {
//                double qValue = GetQValue(state, move);
//                maxQValue = Math.Max(maxQValue, qValue);
//            }

//            return maxQValue;
//        }
//    }

//}
