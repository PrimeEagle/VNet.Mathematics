using System.Numerics;
using VNet.System.Conversion;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public class QrGivensDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            var Q = new Matrix<T>(matrix.N, matrix.N);
            var R = new Matrix<T>(matrix.N, matrix.N);


            for (var i = 0; i < matrix.M; i++)
                for (var j = 0; j < matrix.M; j++)
                    Q[i, j] = GenericNumber<T>.FromDouble((i == j) ? 1.0 : 0.0);

            for (var i = 0; i < matrix.M; i++)
                for (var j = 0; j < matrix.N; j++)
                    R[i, j] = matrix[i, j];

            for (var j = 0; j < matrix.N; j++)
            {
                for (var i = matrix.M - 1; i > j; i--)
                {
                    var a = R[i - 1, j];
                    var b = R[i, j];
                    var r = Math.Sqrt(GenericNumber<T>.ToDouble(a * a + b * b));
                    var c = GenericNumber<T>.ToDouble(a) / r;
                    var s = GenericNumber<T>.ToDouble(-b) / r;

                    for (var k = 0; k < matrix.N; k++)
                    {
                        var t1 = R[i - 1, k];
                        var t2 = R[i, k];
                        R[i - 1, k] = GenericNumber<T>.FromDouble(c) * t1 - GenericNumber<T>.FromDouble(s) * t2;
                        R[i, k] = GenericNumber<T>.FromDouble(s) * t1 + GenericNumber<T>.FromDouble(c) * t2;
                    }

                    for (var k = 0; k < matrix.M; k++)
                    {
                        var t1 = Q[k, i - 1];
                        var t2 = Q[k, i];
                        Q[k, i - 1] = GenericNumber<T>.FromDouble(c) * t1 - GenericNumber<T>.FromDouble(s) * t2;
                        Q[k, i] = GenericNumber<T>.FromDouble(s) * t1 + GenericNumber<T>.FromDouble(c) * t2;
                    }
                }
            }

            return new DecomposedMatrix<T>("Q", Q, "R", R);
        }
    }
}