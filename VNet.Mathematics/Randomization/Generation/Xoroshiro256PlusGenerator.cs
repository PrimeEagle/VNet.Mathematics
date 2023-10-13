﻿namespace VNet.Mathematics.Randomization.Generation;

public class Xoroshiro256PlusGenerator : RandomGenerationBase
{
    private readonly List<ulong> _state;


    public Xoroshiro256PlusGenerator()
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>();
        this.GetSeedsFromTime(4);
    }

    public Xoroshiro256PlusGenerator(double seed1, double seed2, double seed3, double seed4)
    {
        _state = new List<ulong>();
        this.Seeds = new List<double>()
        {
            seed1,
            seed2,
            seed3,
            seed4
        };
    }

    public override int Next()
    {
        var result = _state[0] + _state[3];

        var t = _state[1] << 17;

        _state[2] ^= _state[0];
        _state[3] ^= _state[1];
        _state[1] ^= _state[2];
        _state[0] ^= _state[3];

        _state[2] ^= t;

        _state[3] = RotateLeft(_state[3], 45);

        return (int)result;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}