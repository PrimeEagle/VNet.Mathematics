using System.Numerics;
using VNet.System;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public class DoolittleDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, bool fullPivot = false)
        {
            var lowerMatrixData = new T[matrix.N, matrix.M];
            var upperMatrixData = new T[matrix.N, matrix.M];

            matrix = fullPivot ? matrix.Pivot(MatrixPivotType.Full) : matrix.Pivot(MatrixPivotType.Full);
            
            for (var i = 0; i < matrix.N; i++)
            {
                for (var j = 0; j < matrix.N; j++)
                {
                    if (j <= i)
                    {
                        lowerMatrixData[i, j] = matrix[i, j];

                        for (var k = 0; k < j; k++)
                        {
                            lowerMatrixData[i, j] -= lowerMatrixData[i, k] * upperMatrixData[k, j];
                        }
                        if (i == j)
                        {
                            upperMatrixData[i, j] = Generic.ConvertFromObject<T>(1);
                        }
                        else
                        {
                            upperMatrixData[i, j] = Generic.ConvertFromObject<T>(0);
                        }
                    }
                    else
                    {
                        upperMatrixData[i, j] = matrix[i, j];

                        for (var k = 0; k < i; k++)
                        {
                            upperMatrixData[i, j] -= lowerMatrixData[i, k] * upperMatrixData[k, j];
                        }

                        upperMatrixData[i, j] = upperMatrixData[i, j] / lowerMatrixData[i, i];
                    }
                }
            }

            var upperMatrix = new Matrix<T>(upperMatrixData);
            var lowerMatrix = new Matrix<T>(upperMatrixData);

            return new DecomposedMatrix<T>(upperMatrix, lowerMatrix);
        }
    }
}