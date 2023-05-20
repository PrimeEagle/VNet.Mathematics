using System.Numerics;
using VNet.Mathematics.Randomization.Generation;
using VNet.System;

namespace VNet.Mathematics.LinearAlgebra.Matrix;

public class Matrix<T> : IEquatable<Matrix<T>> where T : notnull, INumber<T>
{
    public Matrix(T[,] data)
    {
        Data = data;
    }

    public Matrix(int n, int m)
    {
        Data = new T[n, m];
    }

    public Matrix(Matrix<T> source)
    {
        Data = new T[source.N, source.M];

        for (var i = 0; i < source.N; i++)
            for (var j = 0; j < source.M; j++)
                Data[i, j] = source[i, j];
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public int N => Data.GetLength(0);

    // ReSharper disable once MemberCanBePrivate.Global
    public int M => Data.GetLength(1);

    // ReSharper disable once MemberCanBePrivate.Global
    public int NumRows => N;

    // ReSharper disable once MemberCanBePrivate.Global
    public int NumColumns => M;
    public bool IsSquare => N == M;

    public bool IsSymmetric
    {
        get
        {
            if (!IsSquare) return false;

            for (var i = 0; i < N; i++)
                for (var j = i + 1; j < M; j++) // We start from j = i + 1 because we've already checked previous pairs
                    if (Data[i, j] != Data[j, i])
                        return false;

            return true;
        }
    }

    public bool IsOrthogonal
    {
        get
        {
            if (!IsSquare) return false;

            var transpose = Transpose();
            var product = MatrixMultiply(transpose);

            return product.IsIdentity;
        }
    }

    public bool IsToeplitz
    {
        get
        {
            for (var i = 0; i < N - 1; i++)
                for (var j = 0; j < M - 1; j++)
                    if (Data[i, j] != Data[i + 1, j + 1])
                        return false;

            return true;
        }
    }

    public bool IsHankel
    {
        get
        {
            if (!IsSquare) return false;

            for (var i = 0; i < N - 1; i++)
                for (var j = 0; j < M - i - 1; j++)
                    if (Data[i, j] != Data[i + 1, j + 1])
                        return false;

            for (var j = 1; j < M; j++)
                for (var i = 0; i < N - j - 1; i++)
                    if (Data[i, j] != Data[i + 1, j + 1])
                        return false;

            return true;
        }
    }

    public bool IsCatalecticant => IsHankel;

    public bool IsDiagonal
    {
        get
        {
            for (var i = 0; i < N; i++)
                for (var j = 0; j < M; j++)
                    if (i != j && Math.Abs(GenericNumber<T>.ToDouble(Data[i, j])) > 1e-9) // Off-diagonal elements should be 0
                        return false;

            return true;
        }
    }

    public bool IsScalar => N == 1 && M == 1;

    public bool IsZero
    {
        get
        {
            for (var i = 0; i < N; i++)
                for (var j = 0; j < M; j++)
                    if (Math.Abs(GenericNumber<T>.ToDouble(Data[i, j])) > 1e-9d)
                        return false;

            return true;
        }
    }

    public bool IsIdentity
    {
        get
        {
            if (!IsSquare) return false;


            for (var i = 0; i < N; i++)
                for (var j = 0; j < M; j++)
                {
                    if (i == j && Math.Abs(GenericNumber<T>.ToDouble(Data[i, j]) - 1) > 1e-9d) // The diagonal should be 1
                        return false;
                    if (i != j && Math.Abs(GenericNumber<T>.ToDouble(Data[i, j])) > 1e-9d) // Off-diagonal elements should be 0
                        return false;
                }

            return true;
        }
    }

    public bool IsUnit
    {
        get
        {
            for (var i = 0; i < N; i++)
                for (var j = 0; j < M; j++)
                    if (Math.Abs(GenericNumber<T>.ToDouble(Data[i, j]) - 1) > 1e-9d)
                        return false;

            return true;
        }
    }

    public bool IsSingular => Determinant() == GenericNumber<T>.FromInt(0);

    private T[,] Data { get; }

    public T this[int index1, int index2]
    {
        get => Data[index1, index2];
        set => Data[index1, index2] = value;
    }

    public bool Equals(Matrix<T>? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;

        if (N != other.N || M != other.M)
            return false;

        const bool result = true;

        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                if (Data[i, j] != other.Data[i, j])
                    return false;

        return result;
    }

    #region Matrix Creation
    public Matrix<T> Random(IRandomGenerationAlgorithm randomAlgorithm)
    {
        var result = Clone();

        for (var i = 0; i < N; ++i)
            for (var j = 0; j < M; j++)
                result[i, j] = GenericNumber<T>.FromDouble(randomAlgorithm.NextDouble());

        return result;
    }

    public Matrix<T> Random(int n, int m, IRandomGenerationAlgorithm randomGeneration)
    {
        var result = new Matrix<T>(n, m);

        return result.Random(randomGeneration);
    }

    public Matrix<T> Random(int n, int m)
    {
        var result = new Matrix<T>(n, m);
        IRandomGenerationAlgorithm randomGeneration = new DotNet();

        return result.Random(randomGeneration);
    }

    public Matrix<T> Random()
    {
        var result = Clone();
        IRandomGenerationAlgorithm randomGeneration = new DotNet();

        return result.Random(randomGeneration);
    }

    public Matrix<T> Identity()
    {
        if (!IsSquare) throw new ArgumentException("An identity matrix must be square.");
        var result = Clone();

        for (var i = 0; i < N; ++i)
            for (var j = 0; j < M; j++)
                if (i == j)
                    result[i, j] = GenericNumber<T>.FromInt(1);
                else
                    result[i, j] = GenericNumber<T>.FromInt(0);

        return result;
    }

    public static Matrix<T> Identity(int n)
    {
        var result = new Matrix<T>(n, n);

        for (var i = 0; i < n; ++i)
            for (var j = 0; j < n; j++)
                if (i == j)
                    result[i, j] = GenericNumber<T>.FromInt(1);
                else
                    result[i, j] = GenericNumber<T>.FromInt(0);

        return result;
    }

    public Matrix<T> Unit()
    {
        var result = Clone();

        for (var i = 0; i < N; ++i)
            for (var j = 0; j < M; j++)
                result[i, j] = GenericNumber<T>.FromInt(1);

        return result;
    }

    public static Matrix<T> Unit(int n, int m)
    {
        var result = new Matrix<T>(n, m);

        for (var i = 0; i < n; ++i)
            for (var j = 0; j < m; j++)
                result[i, j] = GenericNumber<T>.FromInt(1);

        return result;
    }

    public Matrix<T> Zero()
    {
        var result = Clone();

        for (var i = 0; i < N; ++i)
            for (var j = 0; j < M; j++)
                result[i, j] = GenericNumber<T>.FromInt(0);

        return result;
    }

    public static Matrix<T> Zero(int n, int m)
    {
        var result = new Matrix<T>(n, m);

        for (var i = 0; i < n; ++i)
            for (var j = 0; j < m; j++)
                result[i, j] = GenericNumber<T>.FromInt(0);

        return result;
    }
    #endregion Matrix Creation

    #region Scalar Operations
    public T Determinant()
    {
        switch (N)
        {
            case 1:
                return Data[0, 0];
            case 2:
                return Data[0, 0] * Data[1, 1] - Data[0, 1] * Data[1, 0];
        }

        var det = GenericNumber<T>.FromDouble(0.0d);
        for (var j = 0; j < N; j++)
        {
            var minor = Minor(0, j);
            det += GenericNumber<T>.FromDouble(j % 2 == 1 ? -1.0 : 1.0) * Data[0, j] * minor.Determinant();
        }

        return det;
    }
    public int Rank(MatrixPivotType pivotType)
    {
        var matrix = ReducedRowEchelonForm(pivotType);
        var rank = 0;

        for (var i = 0; i < N; i++)
        {
            for (var j = 0; j < M; j++)
            {
                if (GenericNumber<T>.ToDouble(matrix[i, j]) == 0d) continue;
                rank++;
                break;
            }
        }

        return rank;
    }

    public T Trace()
    {
        if (!IsSquare)
        {
            throw new ArgumentException("Matrix must be square.");
        }

        var trace = GenericNumber<T>.FromDouble(0);

        for (var i = 0; i < N; i++)
        {
            trace += Data[i, i];
        }

        return trace;
    }

    public List<T> Eigenvalue()
    {
        if (!IsSquare)
        {
            throw new ArgumentException("Matrix must be square.");
        }

        var matrix = Clone();

        var tempData = new double[N, M];
        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                tempData[i, j] = GenericNumber<T>.ToDouble(matrix[i, j]);

        //*0, eigenvectors are not returned;
        //*1, right eigenvectors are returned;
        //*2, left eigenvectors are returned;
        //*3, both left and right eigenvectors are returned.
        const int vectorsNeeded = 0;

        alglib.rmatrixevd(tempData, N, vectorsNeeded, out var eigenvalues, out _, out _, out _);

        return eigenvalues.Select(GenericNumber<T>.FromDouble).ToList<T>();
    }
    #endregion Scalar Operations

    #region Matrix Operations
    public Matrix<T> Clone()
    {
        var newMatrixData = new T[N, M];

        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                newMatrixData[i, j] = Data[i, j];

        return new Matrix<T>(newMatrixData);
    }

    public Matrix<T> Transpose()
    {
        var transpose = new Matrix<T>(M, N);

        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                transpose[j, i] = Data[i, j];

        return transpose;
    }

    public Matrix<T> Majors()
    {
        var majors = new Matrix<T>(N, N);

        for (var i = 0; i < N; i++)
            for (var j = 0; j < N; j++)
            {
                var minor = Minor(i, j);
                majors[i, j] = minor.Determinant();
            }

        return majors;
    }

    public Matrix<T> Minor(int row, int column)
    {
        var minor = new Matrix<T>(N - 1, N - 1);
        int minorRow = 0, minorCol = 0;

        for (var i = 0; i < N; i++)
        {
            if (i == row) continue;
            for (var j = 0; j < N; j++)
            {
                if (j == column) continue;
                minor[minorRow, minorCol] = Data[i, j];
                minorCol++;

                if (minorCol < N - 1) continue;
                minorCol = 0;
                minorRow++;
            }
        }

        return minor;
    }

    public Matrix<T> Eigenvector()
    {
        if (!IsSquare)
        {
            throw new ArgumentException("Matrix must be square.");
        }

        var matrix = Clone();

        double[,] tempData = new double[N, M];
        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                tempData[i, j] = GenericNumber<T>.ToDouble(matrix[i, j]);

        //*0, eigenvectors are not returned;
        //*1, right eigenvectors are returned;
        //*2, left eigenvectors are returned;
        //*3, both left and right eigenvectors are returned.
        const int vectorsNeeded = 3;

        alglib.rmatrixevd(tempData, N, vectorsNeeded, out _, out _, out var eigenvectors, out _);

        var result = new Matrix<T>(eigenvectors.GetLength(0), eigenvectors.GetLength(1));
        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                result[i, j] = GenericNumber<T>.FromDouble(eigenvectors[i, j]);

        return result;
    }

    public Matrix<T> HadamardProduct(Matrix<T> matrix)
    {
        if (N != matrix.N || M != matrix.M) throw new ArgumentException("Matrices must be of the same dimensions for the Hadamard product.");

        var product = new Matrix<T>(N, M);

        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                product[i, j] = matrix[i, j] * Data[i, j];

        return product;
    }

    public Matrix<T> SchurProduct(Matrix<T> matrix)
    {
        return HadamardProduct(matrix);
    }

    public Matrix<T> ElementWiseMultiply(Matrix<T> matrix)
    {
        return HadamardProduct(matrix);
    }

    public T FrobeniusInnerProduct(Matrix<T> matrix)
    {
        if (N != matrix.N || M != matrix.M) throw new ArgumentException("Matrices must be of the same dimensions for the Frobenius inner product.");

        var innerProduct = GenericNumber<T>.FromDouble(0.0d);

        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                innerProduct += Data[i, j] * matrix[i, j];

        return innerProduct;
    }

    public Matrix<T> MatrixMultiply(Matrix<T> matrix)
    {
        if (M != matrix.N) throw new ArgumentException("Number of columns in the first matrix must be equal to the number of rows in the second matrix for multiplication.");

        var result = new Matrix<T>(N, matrix.M);

        for (var i = 0; i < N; i++)
            for (var j = 0; j < matrix.M; j++)
            {
                result[i, j] = GenericNumber<T>.FromDouble(0);
                for (var k = 0; k < M; k++) result[i, j] += Data[i, k] * matrix[k, j];
            }

        return result;
    }

    public Matrix<T> KroneckerProduct(Matrix<T> matrix)
    {
        var result = new Matrix<T>(N * matrix.N, M * matrix.M);

        for (var i = 0; i < N; i++)
            for (var j = 0; j < M; j++)
                for (var k = 0; k < matrix.N; k++)
                    for (var l = 0; l < matrix.M; l++)
                        result[i * matrix.N + k, j * matrix.M + l] = Data[i, j] * matrix[k, l];

        return result;
    }

    public Matrix<T> TensorProduct(Matrix<T> matrix)
    {
        return KroneckerProduct(matrix);
    }
    #endregion Matrix Operations

    #region Decompositions
    public DecomposedMatrix<T> Decompose(IMatrixDecompositionAlgorithm<T> decompositionAlgorithm, bool useFullPivot = false)
    {
        return decompositionAlgorithm.Decompose(Clone(), useFullPivot);
    }
    #endregion Decompositions

    #region Matrix Forms
    public Matrix<T> Inverse()
    {
        var result = Identity();
        var copy = Clone();

        for (var i = 0; i < N; i++)
        {
            var diagonalValue = copy[i, i];

            var tempRow = copy.DivideRow(i, diagonalValue);
            copy.ReplaceRow(i, tempRow);

            tempRow = result.DivideRow(i, diagonalValue);
            result.ReplaceRow(i, tempRow);

            for (var j = 0; j < N; j++)
            {
                if (i == j) continue;

                var otherRowValue = copy[j, i];
                var fromRow = copy.MultiplyRow(i, otherRowValue);
                var toRow = copy.MultiplyRow(j, otherRowValue);

                for (var c = 0; c < fromRow.Count; c++) toRow[c] = toRow[c] - fromRow[c];
                copy.ReplaceRow(j, toRow);

                fromRow = result.MultiplyRow(i, otherRowValue);
                toRow = result.MultiplyRow(j, otherRowValue);
                for (var c = 0; c < fromRow.Count; c++) toRow[c] = toRow[c] - fromRow[c];
                result.ReplaceRow(j, toRow);
            }
        }

        return result;
    }

    public Matrix<T> RowEchelonForm(MatrixPivotType pivotType = MatrixPivotType.None)
    {
        return GaussianElimination(pivotType);
    }

    public Matrix<T> ReducedRowEchelonForm(MatrixPivotType pivotType = MatrixPivotType.None)
    {
        return GaussJordanElimination(pivotType);
    }

    public Matrix<T> UpperTriangularForm(MatrixPivotType pivotType = MatrixPivotType.None)
    {
        return RowEchelonForm(pivotType);
    }

    public Matrix<T> LowerTriangularForm(MatrixPivotType pivotType = MatrixPivotType.None)
    {
        var matrix = Clone();

        var colOrder = Enumerable.Range(0, M).ToArray();

        for (int i = 0; i < Math.Min(N, M); i++)
        {
            // Pivot
            if (pivotType == MatrixPivotType.Full)
            {
                var pivotIndices = matrix.FullPivot(i, i);
                matrix = matrix.SwapRows(i, pivotIndices.Item1);
                matrix = matrix.SwapColumnsWithOrderTracking(i, pivotIndices.Item2, colOrder);
            }
            else if (pivotType == MatrixPivotType.Partial)
            {
                var maxRowIndex = matrix.PartialPivot(i, i);
                matrix = matrix.SwapRows(i, maxRowIndex);
            }

            // If pivot is zero, no elimination is possible for this column
            if (matrix[i, i] == GenericNumber<T>.FromDouble(0d))
            {
                continue;
            }

            // Zero out elements above the pivot
            for (var j = 0; j < i; j++)
            {
                var factor = matrix[j, i] / matrix[i, i];
                for (var k = i; k < M; k++)
                {
                    matrix[j, k] -= factor * matrix[i, k];
                }
            }
        }

        // If full pivoting was used, reorder the columns to their original order
        if (pivotType == MatrixPivotType.Full)
        {
            var originalOrderMatrix = new Matrix<T>(N, M);
            for (var j = 0; j < M; j++)
            {
                var originalColIndex = Array.IndexOf(colOrder, j);
                for (var i = 0; i < N; i++)
                {
                    originalOrderMatrix[i, j] = matrix[i, originalColIndex];
                }
            }
            matrix = originalOrderMatrix;
        }

        return matrix;
    }

    public Matrix<T> DiagonalForm()
    {
        var matrix = UpperTriangularForm();

        for (var i = 0; i < matrix.N; i++)
        {
            for (var j = 0; j < i; j++)
            {
                var factor = matrix[i, j] / matrix[j, j];
                for (var k = 0; k < matrix.M; k++)
                {
                    matrix[i, k] -= factor * matrix[j, k];
                }
            }
        }

        return matrix;
    }

    public Matrix<T> JordanNormalForm()
    {
    }
    #endregion Matrix Forms

    #region Row Operations
    public Matrix<T> NormalizeRow(int row1Index)
    {
        var newMatrix = Clone();

        for (var j = 0; j < M; j++) newMatrix[row1Index, j] = newMatrix[row1Index, j] / newMatrix[row1Index, 0];

        return newMatrix;
    }

    public Matrix<T> SwapRows(int row1Index, int row2Index)
    {
        var newMatrix = Clone();

        for (var j = 0; j < M; j++) (newMatrix[row1Index, j], newMatrix[row2Index, j]) = (newMatrix[row2Index, j], newMatrix[row1Index, j]);

        return newMatrix;
    }

    public List<T> AddRows(int rowIndex1, int rowIndex2)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex1, j] + Data[rowIndex2, j]);

        return result;
    }

    public List<T> SubtractRows(int rowIndex1, int rowIndex2)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex1, j] - Data[rowIndex2, j]);

        return result;
    }

    public List<T> AddRow(int rowIndex1, T scalar)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex1, j] + scalar);

        return result;
    }

    public List<T> SubtractRow(int rowIndex1, T scalar)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex1, j] - scalar);

        return result;
    }

    public List<T> MultiplyRow(int rowIndex, T scalar)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex, j] * scalar);

        return result;
    }

    public List<T> DivideRow(int rowIndex, T scalar)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex, j] / scalar);

        return result;
    }

    public List<T> MultiplyRows(int rowIndex1, int rowIndex2)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex1, j] * Data[rowIndex2, j]);

        return result;
    }

    public List<T> DivideRows(int rowIndex1, int rowIndex2)
    {
        var result = new List<T>();

        for (var j = 0; j < M; j++) result.Add(Data[rowIndex1, j] / Data[rowIndex2, j]);

        return result;
    }

    public Matrix<T> ReplaceRow(int rowIndex, List<T> row)
    {
        if (M != row.Count) throw new InvalidOperationException("New row must match number of columns in the matrix.");

        var result = Clone();

        for (var j = 0; j < M; j++) result[rowIndex, j] = row[j];

        return result;
    }
    #endregion Row Operations

    #region Column Operations
    public Matrix<T> SwapColumns(int col1Index, int col2Index)
    {
        var newMatrix = Clone();

        for (var i = 0; i < N; i++) (newMatrix[i, col1Index], newMatrix[i, col2Index]) = (newMatrix[i, col2Index], newMatrix[i, col1Index]);

        return newMatrix;
    }

    public List<T> AddColumns(int colIndex1, int colIndex2)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex1] + Data[i, colIndex2]);

        return result;
    }

    public List<T> SubtractColumns(int colIndex1, int colIndex2)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex1] - Data[i, colIndex2]);

        return result;
    }

    public List<T> AddColumn(int colIndex1, T scalar)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex1] + scalar);

        return result;
    }

    public List<T> SubtractColumn(int colIndex1, T scalar)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex1] - scalar);

        return result;
    }

    public List<T> MultiplyColumn(int colIndex, T scalar)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex] * scalar);

        return result;
    }

    public List<T> DivideColumn(int colIndex, T scalar)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex] / scalar);

        return result;
    }

    public List<T> MultiplyColumns(int colIndex1, int colIndex2)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex1] * Data[i, colIndex2]);

        return result;
    }

    public List<T> DivideColumns(int colIndex1, int colIndex2)
    {
        var result = new List<T>();

        for (var i = 0; i < N; i++) result.Add(Data[i, colIndex1] / Data[i, colIndex2]);

        return result;
    }

    public Matrix<T> ReplaceColumn(int colIndex, List<T> column)
    {
        if (N != column.Count) throw new InvalidOperationException("New column must match number of rows in the matrix.");

        var result = Clone();

        for (var i = 0; i < N; i++) result[i, colIndex] = column[i];

        return result;
    }
    #endregion Column Operations

    #region Support Methods
    public int PartialPivot(int startRow, int col)
    {
        var maxRowIndex = startRow;
        var maxVal = Math.Abs(GenericNumber<T>.ToDouble(Data[startRow, col]));

        for (var i = startRow + 1; i < N; i++)
        {
            if (!(Math.Abs(GenericNumber<T>.ToDouble(Data[i, col])) > maxVal)) continue;

            maxVal = Math.Abs(GenericNumber<T>.ToDouble(Data[i, col]));
            maxRowIndex = i;
        }

        return maxRowIndex;
    }

    public Tuple<int, int> FullPivot(int startRow, int startCol)
    {
        var pivotRow = startRow;
        var pivotCol = startCol;
        var max = Data[startRow, startCol];

        for (var i = startRow; i < N; i++)
        {
            for (var j = startCol; j < M; j++)
            {
                if (!(Math.Abs(GenericNumber<T>.ToDouble(Data[i, j])) > Math.Abs(GenericNumber<T>.ToDouble(max)))) continue;

                max = Data[i, j];
                pivotRow = i;
                pivotCol = j;
            }
        }

        return new Tuple<int, int>(pivotRow, pivotCol);
    }

    public Matrix<T> GaussianElimination(MatrixPivotType pivotType = MatrixPivotType.None)
    {
        var matrix = Clone();

        var colOrder = Enumerable.Range(0, M).ToArray();

        for (var i = 0; i < Math.Min(N, M); i++)
        {
            // Pivot
            if (pivotType == MatrixPivotType.Full)
            {
                var pivotIndices = matrix.FullPivot(i, i);
                matrix = matrix.SwapRows(i, pivotIndices.Item1);
                matrix = matrix.SwapColumnsWithOrderTracking(i, pivotIndices.Item2, colOrder);
            }
            else if (pivotType == MatrixPivotType.Partial)
            {
                var maxRowIndex = matrix.PartialPivot(i, i);
                matrix = matrix.SwapRows(i, maxRowIndex);
            }

            // If pivot is zero, no elimination is possible for this column
            if (matrix[i, i] == GenericNumber<T>.FromDouble(0d))
            {
                continue;
            }

            // Zero out below the pivot
            for (var j = i + 1; j < N; j++)
            {
                var factor = matrix[j, i] / matrix[i, i];
                for (var k = i; k < M; k++)
                {
                    matrix[j, k] -= factor * matrix[i, k];
                }
            }
        }

        // If full pivoting was used, reorder the columns to their original order
        if (pivotType == MatrixPivotType.Full)
        {
            var originalOrderMatrix = new Matrix<T>(N, M);
            for (var j = 0; j < M; j++)
            {
                var originalColIndex = Array.IndexOf(colOrder, j);
                for (var i = 0; i < N; i++)
                {
                    originalOrderMatrix[i, j] = matrix[i, originalColIndex];
                }
            }
            matrix = originalOrderMatrix;
        }

        return matrix;
    }

    public Matrix<T> GaussJordanElimination(MatrixPivotType pivotType = MatrixPivotType.None)
    {
        var matrix = Clone();

        int[] colOrder = Enumerable.Range(0, M).ToArray();

        for (int i = 0; i < Math.Min(N, M); i++)
        {
            // Pivot
            if (pivotType == MatrixPivotType.Full)
            {
                Tuple<int, int> pivotIndices = matrix.FullPivot(i, i);
                matrix = matrix.SwapRows(i, pivotIndices.Item1);
                matrix = matrix.SwapColumnsWithOrderTracking(i, pivotIndices.Item2, colOrder);
            }
            else if (pivotType == MatrixPivotType.Partial)
            {
                int maxRowIndex = matrix.PartialPivot(i, i);
                matrix = matrix.SwapRows(i, maxRowIndex);
            }

            // If pivot is zero, no elimination is possible for this column
            if (matrix[i, i] == GenericNumber<T>.FromDouble(0))
            {
                continue;
            }

            // Scale the pivot row so that the pivot element becomes 1
            T pivotElement = matrix[i, i];
            for (int k = i; k < M; k++)
            {
                matrix[i, k] /= pivotElement;
            }

            // Zero out above and below the pivot
            for (int j = 0; j < N; j++)
            {
                if (j != i) // Skip the pivot row itself
                {
                    T factor = matrix[j, i];
                    for (int k = i; k < M; k++)
                    {
                        matrix[j, k] -= factor * matrix[i, k];
                    }
                }
            }
        }

        // If full pivoting was used, reorder the columns to their original order
        if (pivotType == MatrixPivotType.Full)
        {
            var originalOrderMatrix = new Matrix<T>(N, M);

            for (int j = 0; j < M; j++)
            {
                int originalColIndex = Array.IndexOf(colOrder, j);
                for (int i = 0; i < N; i++)
                {
                    originalOrderMatrix[i, j] = matrix[i, originalColIndex];
                }
            }
            matrix = originalOrderMatrix;
        }

        return matrix;
    }

    private Matrix<T> SwapColumnsWithOrderTracking(int col1, int col2, int[] colOrder)
    {
        var matrix = Clone();
        for (int i = 0; i < N; i++)
        {
            (matrix[i, col1], matrix[i, col2]) = (matrix[i, col2], matrix[i, col1]);
        }
        // Swap in column order tracking array as well
        (colOrder[col1], colOrder[col2]) = (colOrder[col2], colOrder[col1]);

        return matrix;
    }
    #endregion Support Methods

    #region Norms

    public T FrobeniusNorm()
    {
    }

    public T PNorm()
    {
    }

    public T LpNorm()
    {
        return PNorm();
    }

    public T HolderNorm()
    {
        return PNorm();
    }

    public T SchattenPNorm()
    {
        return PNorm();
    }

    public T InfinityNorm()
    {
    }

    public T MaximumAbsoluteRowSumNorm()
    {
        return InfinityNorm();
    }

    public T OneNorm()
    {
    }

    public T MaximumAbsoluteColumnSumNorm()
    {
        return OneNorm();
    }

    public T TwoNorm()
    {
    }

    public T SpectralNorm()
    {
        return TwoNorm();
    }

    public T NuclearNorm()
    {
    }

    #endregion Norms

    #region Operator Overloads

    public static bool operator ==(Matrix<T> a, Matrix<T> b)
    {
        if (a.N != b.N || a.M != b.M) return false;

        var result = true;

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                if (a[i, j] != b[i, j])
                    return false;

        return result;
    }

    public static bool operator !=(Matrix<T> a, Matrix<T> b)
    {
        return !(a == b);
    }

    public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
    {
        if (a.N != b.N || a.M != b.M) throw new ArgumentException("Both matrices must be the same size.");

        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = a[i, j] + b[i, j];

        return result;
    }

    public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
    {
        if (a.N != b.N || a.M != b.M) throw new ArgumentException("Both matrices must be the same size.");

        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = a[i, j] - b[i, j];

        return result;
    }

    public static Matrix<T> operator *(int a, Matrix<T> b)
    {
        var result = new Matrix<T>(b.N, b.M);

        for (var i = 0; i < b.N; i++)
            for (var j = 0; j < b.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(a) * b[i, j];

        return result;
    }

    public static Matrix<T> operator *(uint a, Matrix<T> b)
    {
        var result = new Matrix<T>(b.N, b.M);

        for (var i = 0; i < b.N; i++)
            for (var j = 0; j < b.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(a) * b[i, j];

        return result;
    }

    public static Matrix<T> operator *(long a, Matrix<T> b)
    {
        var result = new Matrix<T>(b.N, b.M);

        for (var i = 0; i < b.N; i++)
            for (var j = 0; j < b.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(a) * b[i, j];

        return result;
    }

    public static Matrix<T> operator *(ulong a, Matrix<T> b)
    {
        var result = new Matrix<T>(b.N, b.M);

        for (var i = 0; i < b.N; i++)
            for (var j = 0; j < b.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(a) * b[i, j];

        return result;
    }

    public static Matrix<T> operator *(float a, Matrix<T> b)
    {
        var result = new Matrix<T>(b.N, b.M);

        for (var i = 0; i < b.N; i++)
            for (var j = 0; j < b.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(a) * b[i, j];

        return result;
    }

    public static Matrix<T> operator *(double a, Matrix<T> b)
    {
        var result = new Matrix<T>(b.N, b.M);

        for (var i = 0; i < b.N; i++)
            for (var j = 0; j < b.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(a) * b[i, j];

        return result;
    }

    public static Matrix<T> operator *(decimal a, Matrix<T> b)
    {
        var result = new Matrix<T>(b.N, b.M);

        for (var i = 0; i < b.N; i++)
            for (var j = 0; j < b.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(a) * b[i, j];

        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, int b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) * a[i, j];

        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, uint b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) * a[i, j];

        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, long b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) * a[i, j];

        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, ulong b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) * a[i, j];

        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, float b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) * a[i, j];

        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, double b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) * a[i, j];

        return result;
    }

    public static Matrix<T> operator *(Matrix<T> a, decimal b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) * a[i, j];

        return result;
    }

    public static Matrix<T> operator /(Matrix<T> a, int b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) / a[i, j];

        return result;
    }

    public static Matrix<T> operator /(Matrix<T> a, uint b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) / a[i, j];

        return result;
    }

    public static Matrix<T> operator /(Matrix<T> a, long b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) / a[i, j];

        return result;
    }

    public static Matrix<T> operator /(Matrix<T> a, ulong b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) / a[i, j];

        return result;
    }

    public static Matrix<T> operator /(Matrix<T> a, float b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) / a[i, j];

        return result;
    }

    public static Matrix<T> operator /(Matrix<T> a, double b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) / a[i, j];

        return result;
    }

    public static Matrix<T> operator /(Matrix<T> a, decimal b)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = Generic.ConvertFromObject<T>(b) / a[i, j];

        return result;
    }

    public static Matrix<T> operator -(Matrix<T> a)
    {
        var result = new Matrix<T>(a.N, a.M);

        for (var i = 0; i < a.N; i++)
            for (var j = 0; j < a.M; j++)
                result[i, j] = -a[i, j];

        return result;
    }

    #endregion Operator Overloads
}