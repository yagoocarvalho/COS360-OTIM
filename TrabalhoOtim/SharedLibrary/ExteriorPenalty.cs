using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    class ExteriorPenalty
    {
        //min(x+2)²/20
        //g1= -x+1<=0
        //g2= x-2<=0
        static double f1(Matrix<double> x, double p)
        {
            return (Math.Pow(x.At(0, 0) + 2, 2)) / 20 +
                p * ((Math.Max(0, 1 - x.At(0, 0))) + (Math.Max(0,  x.At(0, 0)-2)));
        }

        public static MathNet.Numerics.LinearAlgebra.Matrix<double> Execute()
        {

            double t = 1.0;
            Matrix<double> x = DenseMatrix.OfArray(new Double[,] { { 8 }, { 7 } });
            Matrix<double> xNext = DenseMatrix.OfArray(new Double[,] { { 0 }, { 0 } });

            double k = 0;
            double beta = 3;
            double tol = Double.MaxValue;

            double p = 0;
        }
    }
}
