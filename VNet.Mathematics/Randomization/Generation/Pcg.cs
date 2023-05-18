﻿namespace VNet.Mathematics.Randomization.Generation;

public class Pcg : RandomGenerationBase<ulong, ulong>
{
    private const ulong Increment = 1442695040888963407;
    private ulong _state;


    public Pcg()
    {
        _state = Seeds[0];
    }

    public Pcg(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds[0];
    }

    public Pcg(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds[0];
    }

    public Pcg(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public Pcg(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public Pcg(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public override ulong Next()
    {
        var oldState = _state;
        _state = oldState * 6364136223846793005UL + Increment;
        var xorShifted = ((oldState >> 18) ^ oldState) >> 27;
        var rotate = oldState >> 59;
        var randomValue = (xorShifted >> (int) rotate) | (xorShifted << (int) (rotate ^ 31));

        return randomValue % (MaxValue - MinValue + 1) + MinValue;
    }
}