using VNet.Mathematics.Filter.Arguments;

namespace VNet.Mathematics.Filter.Algorithms
{
    public abstract class FilterAlgorithmBase : IFilterAlgorithm
    {
        protected readonly IFilterArgs Args;

        protected FilterAlgorithmBase(IFilterArgs args)
        {
            Args = args;
        }
        public abstract double[] Apply(double[] input);
        public abstract bool IsValid();
    }
}