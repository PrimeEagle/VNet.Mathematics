using System.Numerics;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public class DecomposedMatrix<T> where T : notnull, INumber<T>
    {
        public Matrix<T> Matrix1 { get; init; }
        public Matrix<T> Matrix2 { get; init; }

        public DecomposedMatrix(Matrix<T> matrix1, Matrix<T> matrix2)
        {
            Matrix1 = matrix1;
            Matrix2 = matrix2;
        }
    }
}