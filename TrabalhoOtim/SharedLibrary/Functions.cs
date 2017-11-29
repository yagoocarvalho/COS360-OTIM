using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class Functions
    {

        // -30x1 -10x1x2 - 2x1x3 - 3 x1x4 - 10x2 - 10x2x3 - 10x2x4 - 40 x3 - x3x4 - 12x4
        public static double f (Matrix<double> x)
        {
            double x1 = x.At (0, 0);
            double x2 = x.At (1, 0);
            double x3 = x.At (2, 0);
            double x4 = x.At (3, 0);
            return (-30.0*x1 - 10.0*x1*x2 - 2.0*x1*x3 - 3.0*x1*x4 - 10.0*x2 - 10.0*x2*x3 - 10.0*x2*x4 - 40.0*x3 - x3*x4 - 12.0*x4);
        }

        public static Matrix<double> df (Matrix<double> x)
        {
            double x1 = x.At (0,0);
            double x2 = x.At (1,0);
            double x3 = x.At (2,0);
            double x4 = x.At (3,0);

            Matrix <double> returnObj = DenseMatrix.OfArray (new Double[,] {            
                {-30.0    - 10.0*x2 - 2.0*x3  - 3.0*x4},
                {-10.0*x1 - 10.0    - 10.0*x3 - 10.0*x4},
                {-2.0*x1  - 10.0*x2 - 40.0    - x4},
                {-3.0*x1  - 10.0*x2 - x3      - 12.0}
            });

            return returnObj;
        }

        public static Matrix<double> d2f (double x1, double x2, double x3, double x4)
        {
            return DenseMatrix.OfArray (new Double[,] {
                {  0.0, -10.0,  -2.0,  -3.0},
                {-10.0,   0.0, -10.0, -10.0},
                { -2.0, -10.0,   0.0,  -1.0},
                { -3.0, -10.0,  -1.0,   0.0}
            });
        }

        public static double f_pen_ext(Matrix <double> x, double p)
        {
            double x1 = x.At (0,0);
            double x2 = x.At (1,0);
            double x3 = x.At (2,0);
            double x4 = x.At (3,0);

            double g1 = 33.0*x1 + 14.0*x2 + 47.0*x3+ 11*x4 - 59;
            double g2 = x1 - 1;
            double g3 = x2 - 1;
            double g4 = x3 - 1;
            double g5 = x4 - 1;

            // f(x) + p*E(max(0, g)^2)
            double returnValue = Functions.f (x) + (p * (Math.Pow (Math.Max (0.0, g1), 2.0) + Math.Pow (Math.Max (0.0, g2), 2.0) + Math.Pow (Math.Max (0.0, g3), 2.0) + Math.Pow (Math.Max (0.0, g4), 2.0) + Math.Pow (Math.Max (0.0, g5), 2.0)));

            return returnValue;
        }
    }
}
