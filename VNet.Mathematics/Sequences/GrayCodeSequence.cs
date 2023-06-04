﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNet.Mathematics.Sequences
{
    public List<string> GenerateGrayCodeSequence(int length)
    {
        if (length <= 0)
        {
            return new List<string>();
        }

        // 'arr' will store all generated codes
        List<string> arr = new List<string>();

        // start with one-bit pattern
        arr.Add("0");
        arr.Add("1");

        // Every iteration of this loop generates 2*i codes from previously
        // generated i codes.
        for (int i = 2; i < (1 << length); i = i << 1)
        {
            // Enter the prviously generated codes again in arr[] in reverse
            // order. Nor arr[] has double number of codes.
            for (int j = i - 1; j >= 0; j--)
                arr.Add(arr[j]);

            // append 0 to the first half
            for (int j = 0; j < i; j++)
                arr[j] = "0" + arr[j];

            // append 1 to the second half
            for (int j = i; j < 2 * i; j++)
                arr[j] = "1" + arr[j];
        }

        // print contents of arr[]
        return arr.Take((1 << length)).ToList();
    }

}
