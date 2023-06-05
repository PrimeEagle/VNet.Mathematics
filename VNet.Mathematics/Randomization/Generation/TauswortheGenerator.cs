﻿namespace VNet.Mathematics.Randomization.Generation;

public class TauswortheGenerator : RandomGenerationBase<ulong, ulong>
{
    private ulong _state;


    public TauswortheGenerator()
    {
        _state = Seeds[0];
    }

    public TauswortheGenerator(IEnumerable<ulong> seeds) : base(seeds)
    {
        _state = Seeds[0];
    }

    public TauswortheGenerator(IEnumerable<string> seeds) : base(seeds)
    {
        _state = Seeds[0];
    }

    public TauswortheGenerator(IEnumerable<ulong> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public TauswortheGenerator(IEnumerable<string> seeds, ulong minValue, ulong maxValue) : base(seeds, minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public TauswortheGenerator(ulong minValue, ulong maxValue) : base(minValue, maxValue)
    {
        _state = Seeds[0];
    }

    public override ulong Next()
    {
        var b = ((_state << 13) ^ _state) >> 19;
        var c = ((_state << 17) ^ _state) >> 22;
        var d = ((_state << 5) ^ _state) >> 24;

        _state = (b ^ c ^ d) % (MaxValue - MinValue + 1) + MinValue;

        return _state;
    }
}