namespace VNet.Mathematics.Arithmetic;

public static class GreatestCommonFactor
{
    public static int Find(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}