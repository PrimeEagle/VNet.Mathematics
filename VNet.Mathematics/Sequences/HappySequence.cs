using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GenerateHappyNumberSequence(int length)
    {
        List<int> sequence = new List<int>();
        int number = 1;

        while (sequence.Count < length)
        {
            if (IsHappy(number))
            {
                sequence.Add(number);
            }
            number++;
        }

        return sequence;
    }

    private bool IsHappy(int number)
    {
        var seen = new HashSet<int>();
        while (number != 1 && !seen.Contains(number))
        {
            seen.Add(number);
            number = SumOfSquareOfDigits(number);
        }
        return number == 1;
    }

    private int SumOfSquareOfDigits(int number)
    {
        int sum = 0;
        while (number != 0)
        {
            int digit = number % 10;
            sum += digit * digit;
            number /= 10;
        }
        return sum;
    }

}
