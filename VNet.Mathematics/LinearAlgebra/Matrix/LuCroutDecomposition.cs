using System.Numerics;
using VNet.System.Conversion;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public class LuCroutDecomposition<T> : IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None)
        {
            var lower = new Matrix<T>(matrix.N, matrix.N);
            var upper = new Matrix<T>(matrix.N, matrix.N);

            for (var i = 0; i < matrix.N; i++)
            {
                switch (pivotType)
                {
                    case MatrixPivotType.Partial:
                    {
                        var pivotRow = matrix.PartialPivot(i, i);
                        if (pivotRow != i)
                            matrix = matrix.SwapRows(i, pivotRow);
                        break;
                    }
                    case MatrixPivotType.Full:
                    {
                        var pivotIndices = matrix.FullPivot(i, i);
                        if (pivotIndices.Item1 != i)
                            matrix = matrix.SwapRows(i, pivotIndices.Item1);
                        if (pivotIndices.Item2 != i)
                            matrix = matrix.SwapColumns(i, pivotIndices.Item2);
                        break;
                    }
                    case MatrixPivotType.None:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(pivotType), pivotType, null);
                }

                // Lower
                for (var k = i; k < matrix.N; k++)
                {
                    var sum = GenericNumber<T>.FromDouble(0d);
                    for (var j = 0; j < i; j++)
                        sum += (lower[k, j] * upper[j, i]);

                    lower[k, i] = matrix[k, i] - sum;
                }

                // Upper
                for (var k = i; k < matrix.N; k++)
                {
                    if (i == k)
                        upper[i, i] = GenericNumber<T>.FromDouble(1); // Diagonal values are 1 for upper
                    else
                    {
                        var sum = GenericNumber<T>.FromDouble(0d);
                        for (var j = 0; j < i; j++)
                            sum += (lower[i, j] * upper[j, k]);

                        upper[i, k] = (matrix[i, k] - sum) / lower[i, i];
                    }
                }
            }

            return new DecomposedMatrix<T>("Upper", upper, "Lower",  lower);
        }
    }
}