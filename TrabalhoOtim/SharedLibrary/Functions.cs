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
        public static double f (double x1, double x2, double x3, double x4)
        {
            return (-30.0*x1 - 10.0*x1*x2 - 2.0*x1*x3 - 3.0*x1*x4 - 10.0*x2 - 10.0*x2*x3 - 10.0*x2*x4 - 40.0*x3 - x3*x4 - 12.0*x4);
        }

        public static Vector<double> df (double x1, double x2, double x3, double x4)
        {
            return DenseVector.OfArray (new Double[] {            
                -30.0    - 10.0*x2 - 2.0*x3  - 3.0*x4,
                -10.0*x1 - 10.0    - 10.0*x3 - 10.0*x4,
                -2.0*x1  - 10.0*x2 - 40.0    - x4,
                -3.0*x1  - 10.0*x2 - x3      - 12.0
            });
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
    }
}
