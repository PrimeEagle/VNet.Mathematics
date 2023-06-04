using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<int> GeneratePrimeNumberSequence(int length)
    {
        List<int> sequence = new List<int>();
        int number = 2;
        while (sequence.Count < length)
        {
            if (IsPrime(number))
            {
                sequence.Add(number);
            }
            number++;
        }
        return sequence;
    }

    private bool IsPrime(int number)
    {
        if (number <= 1)
        {
            return false;
        }
        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                return false;
            }
        }
        return true;
    }

}
