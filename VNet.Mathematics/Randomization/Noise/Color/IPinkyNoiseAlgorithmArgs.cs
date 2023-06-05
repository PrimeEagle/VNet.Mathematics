﻿namespace VNet.Mathematics.Randomization.Noise.Color
{
    public interface IPinkyNoiseAlgorithmArgs : INoiseAlgorithmArgs
    {
        public double StepSize { get; set; }
        public int NumSteps { get; set; }
    }
}