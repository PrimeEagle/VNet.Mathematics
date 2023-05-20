using System.Numerics;
using VNet.System;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public class QrHouseholderDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            var Q = new Matrix<T>(matrix.N, matrix.N);
            var R = new Matrix<T>(matrix.N, matrix.N);

            var A = matrix.Clone();

            for (var i = 0; i < matrix.M; i++)
                for (var j = 0; j < matrix.M; j++)
                    Q[i, j] = GenericNumber<T>.FromDouble((i == j) ? 1.0 : 0.0);

            for (var k = 0; k < matrix.N; k++)
            {
                double norm = 0;
                for (var j = k; j < matrix.M; j++)
                    norm += GenericNumber<T>.ToDouble(A[j, k] * A[j, k]);
                norm = Math.Sqrt(norm);

                double s = GenericNumber<T>.ToDouble(A[k, k]) > 0 ? -1 : 1;
                double u1 = GenericNumber<T>.ToDouble(A[k, k]) - s * norm;
                var h = Math.Sqrt(2.0) / Math.Abs(u1);

                var w = new double[matrix.M];
                w[k] = u1;
                for (var i = k + 1; i < matrix.M; i++)
                    w[i] = h * GenericNumber<T>.ToDouble(A[i, k]);

                var P = new Matrix<T>(matrix.M, matrix.M);
                for (var i = 0; i < matrix.M; i++)
                    for (var j = 0; j < matrix.M; j++)
                        P[i, j] = GenericNumber<T>.FromDouble(i == j ? 1.0 : 0.0);

                for (var i = 0; i < matrix.M; i++)
                    for (var j = 0; j < matrix.M; j++)
                        P[i, j] -= GenericNumber<T>.FromDouble(h * w[i] * w[j]);

                A = P.MatrixMultiply(A);
                Q = Q.MatrixMultiply(P);
            }

            for (var i = 0; i < matrix.M; i++)
                for (var j = 0; j < matrix.N; j++)
                    R[i, j] = A[i, j];

            return new DecomposedMatrix<T>("Q", Q, "R", R);
        }
    }
}