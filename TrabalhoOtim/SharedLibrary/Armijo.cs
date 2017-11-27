using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace SharedLibrary
{
    public class Armijo
    {
        /// <summary>
        /// Método para achar o passo usando a técnica de Armijo
        /// </summary>

        static double f1(Matrix<double> x)
        {
            //return (Math.Pow(x.At(0,0) - 2, 2))/2 + Math.Pow(x.At(1,0)-1,2);
            return (Math.Pow(x.At(0, 0), 2)) - 24 * x.At(0, 0) + Math.Pow(x.At(1, 0), 2) - 10 * x.At(1, 0);

        }

        static Matrix<double> df1(Matrix<double> x)
        {
            //double[] gradient = new double[2];
            //gradient[0] = x.At(0,0) - 2;
            //gradient[1] = 2 * x.At(1,0) - 2;

            //Matrix<double> df = DenseMatrix.OfArray(new Double[,] { { gradient[0] }, { gradient[1] } });

            //return df;
            double[] gradient = new double[2];
            gradient[0] = 2 * x.At(0, 0) - 24;
            gradient[1] = 2 * x.At(1, 0) - 10;

            Matrix<double> df = DenseMatrix.OfArray(new Double[,] { { gradient[0] }, { gradient[1] } });

            return df;

        }

        /// <returns></returns>
        public static double Execute( Matrix<double> x, Matrix<double> d)
        {
            double t = 1.0;
            double n = 0.25;
            double y = 0.8;

            int i = 0;

            while (f1(x.Add(d.Multiply(t))) > (f1(x) + n * t * (df1(x).TransposeAndMultiply(d).At(0, 0))))
            {
                t = y * t;
                i++;

                //Limite de iterações = 1000
                if (i >= 1000) {
                    t = -1;
                    break;
                }
            }
            return t;
        }
    }
}
