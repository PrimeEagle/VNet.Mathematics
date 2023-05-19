//using System.Numerics;
//using MathNet.Numerics.LinearAlgebra.Complex;
//using VNet.Mathematics.Randomization.Generation;
//using VNet.System;

//namespace VNet.Mathematics.LinearAlgebra.Matrix
//{
//    public class Matrix<T> where T : notnull, INumber<T>
//    {
//        // ReSharper disable once MemberCanBePrivate.Global
//        public int N => Data.GetLength(0);

//        // ReSharper disable once MemberCanBePrivate.Global
//        public int M => Data.GetLength(1);

//        // ReSharper disable once MemberCanBePrivate.Global
//        public int NumRows => N;

//        // ReSharper disable once MemberCanBePrivate.Global
//        public int NumColumns => M;
//        public bool IsSquare => N == M;
//        public bool IsSymmetric => throw new NotImplementedException();
//        public bool IsOrthogonal => throw new NotImplementedException();
//        public bool IsHermitian => throw new NotImplementedException();
//        public bool IsSkewHermitian => throw new NotImplementedException();
//        public bool IsToeplitz => throw new NotImplementedException();
//        public bool IsHankel => throw new NotImplementedException();
//        public bool IsDiagonal => throw new NotImplementedException();
//        public bool IsScalar => throw new NotImplementedException();
//        public bool IsZero => throw new NotImplementedException();
//        public bool IsIdentity => throw new NotImplementedException();
//        public bool IsUnit => throw new NotImplementedException();
//        internal T[,] Data { get; }

//        public T this[int index1, int index2]
//        {
//            get => Data[index1, index2];
//            set => Data[index1, index2] = value;
//        }


//        public Matrix(T[,] data)
//        {
//            Data = data;
//        }

//        public Matrix(int n, int m)
//        {
//            Data = new T[n, m];
//        }

//        public Matrix(Matrix<T> source)
//        {
//            Data = new T[source.N, source.M];

//            for (var i = 0; i < source.N; i++)
//            {
//                for (var j = 0; j < source.M; j++)
//                {
//                    Data[i, j] = source[i, j];
//                }
//            }
//        }

//        public Matrix<T> Clone()
//        {
//            var newMatrixData = new T[N, M];

//            for (var i = 0; i < N; i++)
//            {
//                for (var j = 0; j < M; j++)
//                {
//                    newMatrixData[i, j] = Data[i, j];
//                }
//            }

//            return new Matrix<T>(newMatrixData);
//        }

//        public Matrix<T> Transpose()
//        {

//        }

//        public T Determinant()
//        {

//        }

//        public Matrix<T> Inverse()
//        {

//        }

//        public Matrix<T> Random()
//        {

//        }

//        public static Matrix<T> Random(int n, int m, IRandomGenerationAlgorithm randomGeneration)
//        {

//        }

//        public Matrix<T> Random(IRandomGenerationAlgorithm randomGeneration)
//        {

//        }

//        public static Matrix<T> Random(int n, int m)
//        {

//        }

//        public Matrix<T> Identity()
//        {

//        }

//        public static Matrix<T> Identity(int n, int m)
//        {

//        }

//        public Matrix<T> Unit()
//        {

//        }

//        public static Matrix<T> Unit(int n, int m)
//        {

//        }

//        public Matrix<T> Zero()
//        {

//        }

//        public static Matrix<T> Zero(int n, int m)
//        {

//        }

//        public Matrix<T> HadamardProduct(Matrix<T> matrix)
//        {

//        }

//        public Matrix<T> SchurProduct(Matrix<T> matrix)
//        {
//            return HadamardProduct(matrix);
//        }

//        public Matrix<T> ElementWiseMultiply(Matrix<T> matrix)
//        {
//            return HadamardProduct(matrix);
//        }

//        public Matrix<T> FrobeniusInnerProduct(Matrix<T> matrix)
//        {

//        }

//        public Matrix<T> MatrixMultiply(Matrix<T> matrix)
//        {

//        }

//        public Matrix<T> KroneckerProduct(Matrix<T> matrix)
//        {

//        }

//        public Matrix<T> TensorProduct(Matrix<T> matrix)
//        {

//        }

//        public Matrix<T> ExteriorProduct(Matrix<T> matrix)
//        {

//        }

//        public DecomposedMatrix<T> Decompose(IMatrixDecompositionAlgorithm<T> decompositionAlgorithm, bool useFullPivot = false)
//        {
//            return decompositionAlgorithm.Decompose(Clone(), useFullPivot);
//        }

//        public Matrix<T> RowEchelonForm()
//        {

//        }

//        public Matrix<T> ReducedRowEchelonForm()
//        {

//        }

//        public Matrix<T> UpperTriangularForm()
//        {

//        }

//        public Matrix<T> LowerTriangularForm()
//        {

//        }

//        public Matrix<T> DiagonalForm()
//        {

//        }

//        public Matrix<T> JordanNormalForm()
//        {

//        }

//        public T FrobeniusNorm()
//        {

//        }

//        public T PNorm()
//        {

//        }

//        public T LpNorm()
//        {
//            return PNorm();
//        }

//        public T HolderNorm()
//        {
//            return PNorm();
//        }

//        public T SchattenPNorm()
//        {
//            return PNorm();
//        }

//        public T InfinityNorm()
//        {

//        }

//        public T MaximumAbsoluteRowSumNorm()
//        {
//            return InfinityNorm();
//        }

//        public T OneNorm()
//        {

//        }

//        public T MaximumAbsoluteColumnSumNorm()
//        {
//            return OneNorm();
//        }

//        public T TwoNorm()
//        {

//        }

//        public T SpectralNorm()
//        {
//            return TwoNorm();
//        }

//        public T NuclearNorm()
//        {

//        }

//        public T Trace()
//        {

//        }

//        public T Rank()
//        {

//        }

//        public T Eigenvalue()
//        {

//        }

//        public Matrix<T> Eigenvecor()
//        {

//        }

//        public Matrix<T> SwapRows(int row1Index, int row2Index)
//        {
//            var newMatrix = Clone();

//            for (var j = 0; j < M; j++)
//            {
//                (newMatrix[row1Index, j], newMatrix[row2Index, j]) = (newMatrix[row2Index, j], newMatrix[row1Index, j]);
//            }

//            return newMatrix;
//        }

//        public Matrix<T> SwapColumns(int col1Index, int col2Index)
//        {
//            var newMatrix = Clone();

//            for (var i = 0; i < N; i++)
//            {
//                (newMatrix[i, col1Index], newMatrix[i, col2Index]) = (newMatrix[i, col2Index], newMatrix[i, col1Index]);
//            }

//            return newMatrix;
//        }

//        public Matrix<T> PartialPivot()
//        {
//            var newMatrix = Clone();

//            for (var i = 0; i < newMatrix.N; i++)
//            {
//                var maxIndex = i;
//                var maxValue = Data[i, i];

//                for (var j = i + 1; j < newMatrix.N; j++)
//                {
//                    if (Math.Abs(Generic.ConvertType<T, double>(Data[j, i])) <= Math.Abs(Generic.ConvertType<T, double>(maxValue))) continue;
//                    maxIndex = j;
//                    maxValue = Data[j, i];
//                }

//                if (maxIndex != i)
//                {
//                    newMatrix = newMatrix.SwapRows(i, maxIndex);
//                }
//            }

//            return newMatrix;
//        }

//        public Matrix<T> FullPivot()
//        {
//            var newMatrix = Clone();

//            var rowPermutation = Enumerable.Range(0, newMatrix.N).Select(Generic.ConvertType<int, T>).ToArray<T>();
//            var colPermutation = Enumerable.Range(0, newMatrix.N).Select(Generic.ConvertType<int, T>).ToArray<T>();

//            for (var i = 0; i < newMatrix.N; i++)
//            {
//                var maxRowIndex = i;
//                var maxColIndex = i;
//                var maxValue = newMatrix[i, i];

//                for (var j = i; j < newMatrix.N; j++)
//                {
//                    for (var k = i; k < newMatrix.N; k++)
//                    {
//                        if (!(Math.Abs(Generic.ConvertType<T, double>(newMatrix[j, k])) > Math.Abs(Generic.ConvertType<T, double>(maxValue)))) continue;
//                        maxRowIndex = j;
//                        maxColIndex = k;
//                        maxValue = newMatrix[j, k];
//                    }
//                }

//                newMatrix = newMatrix.SwapRows(i, maxRowIndex);
//                newMatrix = newMatrix.SwapColumns(i, maxColIndex);
//                SwapInArray(rowPermutation, i, maxRowIndex);
//                SwapInArray(colPermutation, i, maxColIndex);
//            }

//            return newMatrix;
//        }

//        private void SwapInArray(IList<T> array, int index1, int index2)
//        {
//            (array[index1], array[index2]) = (array[index2], array[index1]);
//        }

//        public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(int a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(uint a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(long a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(ulong a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(float a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(double a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(decimal a, Matrix<T> b)
//        {
//        }

//        public static Matrix<T> operator *(Matrix<T> a, int b)
//        {
//        }

//        public static Matrix<T> operator *(Matrix<T> a, uint b)
//        {
//        }

//        public static Matrix<T> operator *(Matrix<T> a, long b)
//        {
//        }

//        public static Matrix<T> operator *(Matrix<T> a, ulong b)
//        {
//        }

//        public static Matrix<T> operator *(Matrix<T> a, float b)
//        {
//        }

//        public static Matrix<T> operator *(Matrix<T> a, double b)
//        {
//        }

//        public static Matrix<T> operator *(Matrix<T> a, decimal b)
//        {
//        }

//        public static Matrix<T> operator /(Matrix<T> a, int b)
//        {
//        }

//        public static Matrix<T> operator /(Matrix<T> a, uint b)
//        {
//        }

//        public static Matrix<T> operator /(Matrix<T> a, long b)
//        {
//        }

//        public static Matrix<T> operator /(Matrix<T> a, ulong b)
//        {
//        }

//        public static Matrix<T> operator /(Matrix<T> a, float b)
//        {
//        }

//        public static Matrix<T> operator /(Matrix<T> a, double b)
//        {
//        }

//        public static Matrix<T> operator /(Matrix<T> a, decimal b)
//        {
//        }

//        public static Matrix<T> operator -(Matrix<T> a)
//        {
//        }
//    }
//}