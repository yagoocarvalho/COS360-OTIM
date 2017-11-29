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

        public static double phi(Matrix<double> x, Matrix<double> d, double t, double p) {
            double x0 = x.At(0, 0) + t * d.At(0, 0);
            double x1 = x.At(1, 0) + t * d.At(1, 0);
            double x2 = x.At(2, 0) + t * d.At(2, 0);
            double x3 = x.At(3, 0) + t * d.At(3, 0);

            Matrix<double> tempX = DenseMatrix.OfArray( new Double[,] {
                                                                           {x0},
                                                                           {x1},
                                                                           {x2},
                                                                           {x3}});
            return Functions.f_pen_ext(x, p);

        }


        public static double Execute(Matrix<double> x, Matrix<double> d, double p)
        {
            double tol = 0.0001;
            double teta1 = (3 - Math.Sqrt(5)) / 2;
            double teta2 = 1 - teta1;
            double b = 0;
            double rho = 0.5;
            
            double a = 0;
            double s = b;
            b = 2 * rho;
            //Obtenção do intervalo [a,b]   
            while (phi(x, d, b, p) < phi(x, d, s, p)) {
                a = s;
                s = b;
                b = 2*b;
            }

            //Obtenção de t*
            double u = a + teta1 * (b - a);
            double v = a + teta2 * (b - a);
            //Nº max de iterações
            double k = 0;

            while ((b - a) > tol) {
                if (phi(x, d, u, p) < phi(x, d, v, p)){
                    b = v;  v = u; u = a + teta1 * (b - a);
                }
                else {
                    a = u; u = v; v = a + teta2 * (b - a);
                }
                k++;
                if (k >= 1000)
                    break;
            }

            double t = (u + v) / 2;
            return t;
        }
    }
}
