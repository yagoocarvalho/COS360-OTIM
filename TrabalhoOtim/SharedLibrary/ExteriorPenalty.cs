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
        public static Matrix<double> Execute (Matrix<double> x, Matrix<double> d, double p0, double B = 3.0)
        {

            double         t     = 1.0;
            double tol = Double.MaxValue;

            bool   stablized   = false;
            double p           = p0;
            Matrix<double> min = DenseMatrix.OfArray (new Double[,] {
            {0.0},
            {0.0},
            {0.0},
            {0.0}
            });
            for (int k = 0; k < 1000; k++)
            {
                // Gradiente da função penalizada
                min = Gradient.Execute (x, d);
                if (stablized)
                {
                    break;
                }
                else
                {
                    p = B * p;
                }
            }

            Console.WriteLine ("p = " + p.ToString ());
            Console.WriteLine (String.Format ("Ponto mínimo = [{0}, {1}, {2}, {3}]", min.At (0, 0), min.At (1, 0), min.At (2, 0), min.At (3, 0)));

            return x;
        }
    }
}
