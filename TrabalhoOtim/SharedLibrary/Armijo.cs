using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.IO;

namespace SharedLibrary
{
    public class Armijo
    {
        /// <summary>
        /// Método para achar o passo usando a técnica de Armijo
        /// </summary>
        /// <returns></returns>
        public static double Execute(Matrix<double> x, Matrix<double> d, double rho, out int i)
        {
            double t = 1.0;
            double n = 0.25;
            double y = 0.8;

            i = 0;

            while (Functions.f_pen_ext(x.Add(d.Multiply(t)), rho) > (Functions.f_pen_ext(x, rho) + n * t * (Functions.df_pen_ext(x, rho).TransposeAndMultiply(d).At(0, 0))))
            {
                t = y * t;
                i++;

                //Limite de iterações = 150
                if (i >= 150) {
                    break;
                }
            }

            return t;
        }
    }
}
