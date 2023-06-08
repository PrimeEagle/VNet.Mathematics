﻿namespace VNet.Mathematics.Randomization.Generation;

public class Xoshiro256PlusGenerator : RandomGenerationBase<ulong, ulong>
{
    private readonly List<ulong> _state;
    protected new uint NumberOfSeeds = 4;


    public Xoshiro256PlusGenerator()
    {
        _state = Seeds;
    }

    public Xoshiro256PlusGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoshiro256PlusGenerator(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds;
    }

    public Xoshiro256PlusGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoshiro256PlusGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds;
    }

    public Xoshiro256PlusGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds;
    }

    public override ulong Next()
    {
        var result = _state[0] + _state[3];
        var t = _state[1] << 17;

        _state[2] ^= _state[0];
        _state[3] ^= _state[1];
        _state[1] ^= _state[2];
        _state[0] ^= _state[3];

        _state[2] ^= t;
        _state[3] = RotateLeft(_state[3], 45);

        return result;
    }

    private static ulong RotateLeft(ulong x, int k)
    {
        return (x << k) | (x >> (64 - k));
    }
}