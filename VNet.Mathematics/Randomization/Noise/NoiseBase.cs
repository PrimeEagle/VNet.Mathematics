namespace VNet.Mathematics.Randomization.Noise
{
    public abstract class NoiseBase : INoiseAlgorithm
    {
        protected readonly INoiseAlgorithmArgs Args;



        protected NoiseBase(INoiseAlgorithmArgs args)
        {
            Args = args;
        }

        public abstract double GenerateSingleSampleRaw();

        public virtual double GenerateSingleSample()
        {
            // get raw sample
            var sample = GenerateSingleSampleRaw();

            // filter
            if (Args.OutputFilter is not null && Args.OutputFilter.IsValid())
            {
                var filteredSamples = Args.OutputFilter.Filter(new double[] { sample });
                if (filteredSamples.Length > 0)
                {
                    sample = filteredSamples[0];
                }
            }

            // quantize
            var quantizationLevel = (int)(sample * Args.QuantizeLevels);
            sample = (double)quantizationLevel / Args.QuantizeLevels;

            // scale
            sample *= Args.Scale;

            return sample;
        }

        public virtual double[,] GenerateRaw()
        {
            var samples = new double[Args.Height, Args.Width];

            for (var i = 0; i < Args.Height; i++)
            {
                for (var j = 0; j < Args.Width; j++)
                {
                    var sample = GenerateSingleSampleRaw();
                    samples[i, j] = sample;
                }
            }

            return samples;
        }

        public virtual double[,] Generate()
        {
            var samples = new double[Args.Height, Args.Width];

            for (var i = 0; i < Args.Height; i++)
            {
                for (var j = 0; j < Args.Width; j++)
                {
                    var sample = GenerateSingleSample();
                    samples[i, j] = sample;
                }
            }

            return samples;
        }
    }
}