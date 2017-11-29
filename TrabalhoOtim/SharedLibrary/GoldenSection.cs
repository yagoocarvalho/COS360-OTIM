using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    class GoldenSection
    {

        // phi(t) = f(x' + td), onde x' seria o ponto inicial
        public static void phi(Matrix<double> x, Matrix<double> d, double t) {
            //supondo x' = [1,1]


        }
        public static double Execute()
        {

            double teta1 = (3 - Math.Sqrt(5)) / 2;
            double teta2 = 1 - teta1;

            Matrix<double> a = DenseMatrix.OfArray(
                                        new Double[,] { { 1 }, { 1 } });

            double t = 1.0;
            Matrix<double> x = DenseMatrix.OfArray(new Double[,] { { 1 }, { 0 } });
            Matrix<double> d = DenseMatrix.OfArray(new Double[,] { { 3 }, { 1 } });

            double n = 0.25;
            double y = 0.8;

            int i = 0;
            return -1;
        }
    }
}
