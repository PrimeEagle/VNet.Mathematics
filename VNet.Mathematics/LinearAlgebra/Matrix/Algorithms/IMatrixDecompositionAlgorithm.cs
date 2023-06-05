using System.Numerics;

namespace VNet.Mathematics.LinearAlgebra.Matrix.Algorithms
{
    public interface IMatrixDecompositionAlgorithm<T> where T : notnull, INumber<T>
    {
        public DecomposedMatrix<T> Decompose(Matrix<T> matrix, MatrixPivotType pivotType = MatrixPivotType.None);
    }
}