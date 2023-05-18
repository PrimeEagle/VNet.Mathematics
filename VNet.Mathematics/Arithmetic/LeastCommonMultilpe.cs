namespace VNet.Mathematics.Arithmetic;

public static class LeastCommonMultiple
{
    private static int lcm(int a, int b)
    {
        return a / GreatestCommonFactor.Find(a, b) * b;
    }
}