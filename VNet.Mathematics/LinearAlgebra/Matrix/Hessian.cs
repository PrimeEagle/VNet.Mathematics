using System.Numerics;

namespace VNet.Mathematics.LinearAlgebra.Matrix
{
    public static class Hessian
    {
        private static MathNet.Numerics.LinearAlgebra.Matrix<double> Compute(int i, int j, int k, double[,,] dataSet)
        {
            var dxx = dataSet[i + 1, j, k] - 2 * dataSet[i, j, k] + dataSet[i - 1, j, k];
            var dyy = dataSet[i, j + 1, k] - 2 * dataSet[i, j, k] + dataSet[i, j - 1, k];
            var dzz = dataSet[i, j, k + 1] - 2 * dataSet[i, j, k] + dataSet[i, j, k - 1];

            var dxy = (dataSet[i + 1, j + 1, k] - dataSet[i + 1, j - 1, k]
                                                - dataSet[i - 1, j + 1, k] + dataSet[i - 1, j - 1, k]) * 0.25;
            var dxz = (dataSet[i + 1, j, k + 1] - dataSet[i + 1, j, k - 1]
                                                - dataSet[i - 1, j, k + 1] + dataSet[i - 1, j, k - 1]) * 0.25;
            var dyz = (dataSet[i, j + 1, k + 1] - dataSet[i, j + 1, k - 1]
                                                - dataSet[i, j - 1, k + 1] + dataSet[i, j - 1, k - 1]) * 0.25;

            var hessian = MathNet.Numerics.LinearAlgebra.Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {dxx, dxy, dxz},
                {dxy, dyy, dyz},
                {dxz, dyz, dzz}
            });

            return hessian;
        }

        public static double[] EigenValues(int i, int j, int k, double[,,] dataSet)
        {
            var hessian = Hessian.Compute(i, j, k, dataSet);
            var evd = hessian.Evd();
            var eigenValues = evd.EigenValues;

            return eigenValues.Select(t => t.Real).ToArray();
        }
    }
}