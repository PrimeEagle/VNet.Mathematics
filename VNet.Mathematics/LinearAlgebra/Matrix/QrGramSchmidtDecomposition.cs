using System.Numerics;
using VNet.System;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public class QrGramSchmidtDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            var Q = new Matrix<T>(matrix.N, matrix.N);
            var R = new Matrix<T>(matrix.N, matrix.N);

            for (var i = 0; i < matrix.N; i++)
            {
                // Perform pivoting if necessary
                if (pivotType != MatrixPivotType.None)
                {
                    var pivotColumn = matrix.FullPivotColumn(i);
                    if (pivotColumn != i)
                        matrix = matrix.SwapColumns(i, pivotColumn);
                }

                // Start with the i-th column of the original matrix
                var v = new T[matrix.N];

                for (var j = 0; j < matrix.N; j++)
                    v[j] = matrix[j, i];

                // Subtract projections onto the previous vectors
                for (var j = 0; j < i; j++)
                {
                    var dot = GenericNumber<T>.FromDouble(0);
                    for (var k = 0; k < matrix.N; k++)
                        dot += Q[k, j] * matrix[k, i];

                    for (var k = 0; k < matrix.N; k++)
                        v[k] -= dot * Q[k, j];

                    R[j, i] = dot;
                }

                // Normalize the result to get the i-th vector in Q
                var norm = 0d;
                for (var j = 0; j < matrix.N; j++)
                    norm += GenericNumber<T>.ToDouble(v[j] * v[j]);

                norm = Math.Sqrt(norm);
                for (var j = 0; j < matrix.N; j++)
                    Q[j, i] = v[j] / GenericNumber<T>.FromDouble(norm);

                R[i, i] = GenericNumber<T>.FromDouble(norm);
            }
            
            return new DecomposedMatrix<T>("Q", Q, "R",  R);
        }
    }
}