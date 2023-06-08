//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VNet.Mathematics.GameTheory
//{
//    public class TDLearning
//    {
//        public delegate List<object> GetMovesDelegate(object state);
//        public delegate object MakeMoveDelegate(object state, object move);
//        public delegate bool GameOverDelegate(object state);
//        public delegate double GetRewardDelegate(object state);

//        private GetMovesDelegate getMoves;
//        private MakeMoveDelegate makeMove;
//        private GameOverDelegate gameOver;
//        private GetRewardDelegate getReward;

//        private Dictionary<object, double> stateValues;
//        private double learningRate;
//        private double discountFactor;

//        public TDLearning(GetMovesDelegate getMoves, MakeMoveDelegate makeMove, GameOverDelegate gameOver, GetRewardDelegate getReward, double learningRate, double discountFactor)
//        {
//            this.getMoves = getMoves;
//            this.makeMove = makeMove;
//            this.gameOver = gameOver;
//            this.getReward = getReward;
//            this.learningRate = learningRate;
//            this.discountFactor = discountFactor;
//            this.stateValues = new Dictionary<object, double>();
//        }

//        public object BestMove(object state)
//        {
//            object bestMove = null;
//            double maxValue = double.MinValue;

//            foreach (object move in getMoves(state))
//            {
//                object newState = makeMove(state, move);
//                double value = GetStateValue(newState);
//                if (value > maxValue)
//                {
//                    maxValue = value;
//                    bestMove = move;
//                }
//            }

//            return bestMove;
//        }

//        public void Learn(object state)
//        {
//            double oldValue = GetStateValue(state);
//            object action = BestMove(state);
//            object newState = makeMove(state, action);
//            double reward = getReward(newState);

//            double learnedValue = reward + discountFactor * GetStateValue(newState);
//            double newValue = oldValue + learningRate * (learnedValue - oldValue);

//            stateValues[state] = newValue;
//        }

//        private double GetStateValue(object state)
//        {
//            if (!stateValues.TryGetValue(state, out double value))
//            {
//                value = 0;
//            }

//            return value;
//        }
//    }

//}
