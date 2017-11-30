using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class ExteriorPenalty
    {
        public static Matrix<double> Execute (Matrix<double> x0, double rho0, double beta = 3.0, bool armijo = true)
        {
            double         tol  = Double.MaxValue;
                           
            double         rho  = rho0;
            Matrix<double> xK   = x0;
            Matrix<double> xK_1 = x0;
            for (int k = 0; k < 150; k++)
            {
                // Gradiente da função penalizada
                xK_1 = xK;
                xK = Gradient.Execute (xK, rho, armijo);  // min phi(x, rho), xk = x
                if (!functionStabilized(xK, xK_1, tol, k, rho))    
                {
                    rho = beta * rho;
                }
                else
                {
                    break;
                }
            }

            using (StreamWriter sw = new StreamWriter(@"C:\Users\yagom\Desktop\trab-otim.txt", true))
            {
                sw.WriteLine (armijo ? "Usando Armijo" : "Usando Seção Áurea");
                sw.WriteLine ("rho = " + rho.ToString ());
                sw.WriteLine (String.Format ("Ponto mínimo = [{0}, {1}, {2}, {3}]", xK.At (0, 0), xK.At (1, 0), xK.At (2, 0), xK.At (3, 0)));
                sw.WriteLine ("_______________________________________________________________________________");
                sw.Flush ();
            }

            Console.WriteLine (armijo ? "Usando Armijo" : "Usando Seção Áurea");
            Console.WriteLine ("rho = " + rho.ToString ());
            Console.WriteLine (String.Format ("Ponto mínimo = [{0}, {1}, {2}, {3}]", xK.At (0, 0), xK.At (1, 0), xK.At (2, 0), xK.At (3, 0)));
            Console.WriteLine ("__________________________________________________________________________________");

            return xK;
        }

        private static bool functionStabilized (Matrix<double> xK, Matrix<double> xK_1, double tol, int k, double rho)
        {
            double x1 = xK.At (0,0);
            double x2 = xK.At (1,0);
            double x3 = xK.At (2,0);
            double x4 = xK.At (3,0);

            double g1 = 33.0*x1 + 14.0*x2 + 47.0*x3+ 11*x4 - 59;
            double g2 = x1 - 1;
            double g3 = x2 - 1;
            double g4 = x3 - 1;
            double g5 = x4 - 1;

            if (xK.Subtract(xK_1).Column(0).Norm(2) <= tol)  // || delta x || < tol
            {
                return true;
            }
            if (k >= 150)
            {
                return true;
            }
            if (xK.Equals(xK_1))
            {
                return true;
            }
            if (Functions.f_pen_ext (xK, rho) == Functions.f_pen_ext (xK_1, rho))
            {
                return true;
            }

            return false;
        }
    }
}
