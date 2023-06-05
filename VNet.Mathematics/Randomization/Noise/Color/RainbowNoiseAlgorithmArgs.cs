﻿using VNet.Mathematics.Filter;
using VNet.Mathematics.Randomization.Distribution;

namespace VNet.Mathematics.Randomization.Noise.Color
{
    public class RainbowNoiseAlgorithmArgs : IRainbowNoiseAlgorithmArgs
    {
        public double SamplingRate { get; set; }
        public int Octaves { get; set; }
        public required int Width { get; set; }
        public required int Height { get; set; }
        public int QuantizeLevels { get; set; }
        public double Scale { get; set; }
        public required IRandomDistributionAlgorithm RandomDistributionAlgorithm { get; set; }
        public IFilter? OutputFilter { get; set; }


        public INoiseAlgorithmArgs Clone()
        {
            var result = new RainbowNoiseAlgorithmArgs()
            {
                Width = Width,
                Height = Height,
                QuantizeLevels = QuantizeLevels,
                RandomDistributionAlgorithm = RandomDistributionAlgorithm,
                Scale = Scale,
                OutputFilter = OutputFilter,
                SamplingRate = SamplingRate,
                Octaves = Octaves
            };

            return result;
        }
    }
}