using System.Numerics;
using VNet.System.Conversion;

namespace VNet.Mathematics.LinearAlgebra.Matrix.Algorithms
{
    public class CholeskyDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            if (!matrix.IsSymmetric || !matrix.IsPositiveDefinite) throw new Exception("Matrix must be symmetric and not positive-definite.");


            for (var i = 0; i < matrix.N; i++)
            {
                for (var j = i + 1; j < matrix.M; j++)
                {
                    matrix[i, j] = GenericNumber<T>.FromDouble(0d);
                }
            }

            return new DecomposedMatrix<T>("Cholesky", new Matrix<T>(matrix));
        }
    }
}