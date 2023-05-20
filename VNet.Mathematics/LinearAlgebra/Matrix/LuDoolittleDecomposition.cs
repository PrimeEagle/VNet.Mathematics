using System.Numerics;
using VNet.System;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public class DoolittleDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            var lowerMatrixData = new T[matrix.N, matrix.M];
            var upperMatrixData = new T[matrix.N, matrix.M];

            //matrix = pivotType switch
            //{
            //    MatrixPivotType.Full => matrix.FullPivot(),
            //    MatrixPivotType.Partial => matrix.PartialPivot(),
            //    _ => matrix
            //};

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
                            upperMatrixData[i, j] = GenericNumber<T>.FromDouble(1);
                        }
                        else
                        {
                            upperMatrixData[i, j] = GenericNumber<T>.FromDouble(0);
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

            return new DecomposedMatrix<T>("Upper", upperMatrix, "Lower", lowerMatrix);
        }
    }
}