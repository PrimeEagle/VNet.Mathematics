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

//        public Matrix(DenseMatrix denseMatrix)
//        {
//            var rows = denseMatrix.RowCount;
//            var cols = denseMatrix.ColumnCount;

//            var rectangularArray = new T[rows, cols];

//            for (var i = 0; i < rows; i++)
//            {
//                for (var j = 0; j < cols; j++)
//                {
//                    rectangularArray[i, j] = Generic.ConvertFromObject<T>(denseMatrix[i, j]);
//                }
//            }

//            Data = rectangularArray;
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

//        public Matrix<T> CrossProduct(Matrix<T> matrix)
//        {

//        }

//        public T DotProduct(Matrix<T> matrix)
//        {

//        }

//        public DecomposedMatrix<T> Decompose(IMatrixDecompositionAlgorithm<T> decompositionAlgorithm, bool useFullPivot = false)
//        {
//            return decompositionAlgorithm.Decompose(Clone(), useFullPivot);
//        }

//        internal DenseMatrix ToMathNetDenseMatrix()
//        {
//            var matrix = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.Dense(N, M);

//            // Fill the data array with some values
//            for (var i = 0; i < N; i++)
//            {
//                for (var j = 0; j < M; j++)
//                {
//                    // For this example, set each element to the sum of its indices.
//                    matrix[i, j] = Generic.ConvertType<T, double>(Data[i,j]);
//                }
//            }

//            return (DenseMatrix)matrix;
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

//        public Matrix<T> Pivot(MatrixPivotType pivotType)
//        {
//            switch (pivotType)
//            {
//                case MatrixPivotType.Partial:
//                    return PivotPartial();
//                    break;
//                case MatrixPivotType.Full:
//                    return PivotFull();
//                    break;
//                default:
//                    throw new NotImplementedException();
//            }
//        }

//        private Matrix<T> PivotPartial()
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

//        private Matrix<T> PivotFull()
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