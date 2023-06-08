using System.Numerics;
using VNet.System.Conversion;

namespace VNet.Mathematics.LinearAlgebra.Matrix.Algorithms
{
    public class EigenvalueDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            if (!matrix.IsSymmetric || !matrix.IsSquare) throw new ArgumentException("Matrix must be square and symmetric.");

            var tempData = new double[matrix.N, matrix.M];
            for (var i = 0; i < matrix.N; i++)
                for (var j = 0; j < matrix.M; j++)
                    tempData[i, j] = GenericNumber<T>.ToDouble(matrix[i, j]);

            alglib.smatrixevd(tempData, matrix.N, 1, false, out var d, out var Z);

            var zFinal = new Matrix<T>(matrix.N, matrix.M);
            for (var i = 0; i < matrix.N; i++)
                for (var j = 0; j < matrix.M; j++)
                    zFinal[i, j] = GenericNumber<T>.FromDouble(Z[i, j]);

            var dFinal = new Matrix<T>(matrix.N, matrix.M);
            for (var i = 0; i < matrix.N; i++)
                for (var j = 0; j < matrix.M; j++)
                {
                    if (i == j)
                        dFinal[i, j] = GenericNumber<T>.FromDouble(d[i]);
                    else
                        dFinal[i, j] = GenericNumber<T>.FromDouble(0);
                }

            return new DecomposedMatrix<T>("Z", new Matrix<T>(zFinal), "d", new Matrix<T>(dFinal));
        }
    }
}