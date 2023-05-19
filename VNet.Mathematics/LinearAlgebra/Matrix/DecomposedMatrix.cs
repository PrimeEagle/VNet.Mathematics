//using System.Numerics;

//namespace VNet.Mathematics.LinearAlgebra.Matrix
//{
//    public class DecomposedMatrix<T> where T : notnull, INumber<T>
//    {
//        public KeyValuePair<string, Matrix<T>> MatrixInfo1 { get; init; }
//        public KeyValuePair<string, Matrix<T>> MatrixInfo2 { get; init; }

//        public DecomposedMatrix(string matrix1Name, Matrix<T> matrix1, string matrix2Name, Matrix<T> matrix2)
//        {
//            var kv1 = new KeyValuePair<string, Matrix<T>>(matrix1Name, matrix1);
//            MatrixInfo1 = kv1;

//            var kv2 = new KeyValuePair<string, Matrix<T>>(matrix2Name, matrix2);
//            MatrixInfo2 = kv2;
//        }
//    }
//}