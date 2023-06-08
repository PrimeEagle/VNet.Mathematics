using System.Numerics;
using VNet.System.Conversion;

namespace VNet.Mathematics.LinearAlgebra.Matrix.Algorithms
{
    public class SingularValueDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            var tempData = new double[matrix.N, matrix.M];
            for (var i = 0; i < matrix.N; i++)
                for (var j = 0; j < matrix.M; j++)
                    tempData[i, j] = GenericNumber<T>.ToDouble(matrix[i, j]);

            alglib.rmatrixsvd(tempData, matrix.M, matrix.N, 2, 2, 2, out var W, out var U, out var V);

            var wFinal = new Matrix<T>(matrix.N, matrix.M);
            for (var i = 0; i < matrix.N; i++)
                for (var j = 0; j < matrix.M; j++)
                {
                    if (i == j)
                        wFinal[i, j] = GenericNumber<T>.FromDouble(W[i]);
                    else
                        wFinal[i, j] = GenericNumber<T>.FromDouble(0);
                }

            var uFinal = new Matrix<T>(matrix.N, matrix.M);
            for (var i = 0; i < matrix.N; i++)
                for (var j = 0; j < matrix.M; j++)
                    uFinal[i, j] = GenericNumber<T>.FromDouble(U[i, j]);

            var vFinal = new Matrix<T>(matrix.N, matrix.M);
            for (var i = 0; i < matrix.N; i++)
                for (var j = 0; j < matrix.M; j++)
                    vFinal[i, j] = GenericNumber<T>.FromDouble(V[i, j]);

            return new DecomposedMatrix<T>("W", new Matrix<T>(wFinal), "U", new Matrix<T>(uFinal), "V", new Matrix<T>(vFinal));
        }
    }
}