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
            int            kBusca = 0;
            double         tol    = 0.00001;
            double         rho    = rho0;
            Matrix<double> xK     = x0;
            Matrix<double> xK_1   = x0;
            int k = 1;
            for (k = 1; k <= 500; k++)
            {
                // Gradiente da função penalizada
                xK_1 = xK;
                xK = Gradient.Execute (xK, rho, armijo, out kBusca);  // min phi(x, rho), xk = x
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
                sw.WriteLine (String.Format ("Ponto inicial = [{0}, {1}, {2}, {3}]", x0.At (0, 0), x0.At (1, 0), x0.At (2, 0), x0.At (3, 0)));
                sw.WriteLine ("Número de iterações da Penalidade Exterior: " + k);
                sw.WriteLine (armijo ? "Usando Armijo" : "Usando Seção Áurea");
                sw.WriteLine ("rho = " + rho.ToString ());
                sw.WriteLine (String.Format ("Ponto mínimo = [{0}, {1}, {2}, {3}]", xK.At (0, 0), xK.At (1, 0), xK.At (2, 0), xK.At (3, 0)));
                sw.WriteLine ("_______________________________________________________________________________");
                sw.Flush ();
            }

            Console.WriteLine (String.Format ("Ponto inicial = [{0}, {1}, {2}, {3}]", x0.At (0, 0), x0.At (1, 0), x0.At (2, 0), x0.At (3, 0)));
            Console.WriteLine ("Número de iterações da Penalidade Exterior: " + k);
            Console.WriteLine (armijo ? "Usando Armijo" : "Usando Seção Áurea");
            Console.WriteLine ("rho = " + rho.ToString ());
            Console.WriteLine (String.Format ("Ponto mínimo = [{0}, {1}, {2}, {3}]", xK.At (0, 0), xK.At (1, 0), xK.At (2, 0), xK.At (3, 0)));
            Console.WriteLine ("__________________________________________________________________________________");

            using (StreamWriter sw = new StreamWriter (@"C:\Users\yagom\Desktop\trab-otim.csv", true))
            {
                sw.WriteLine (String.Format ("{0};{1};{2};{3};{4};{5}", String.Format ("[{0}, {1}, {2}, {3}]", x0.At (0, 0), x0.At (1, 0), x0.At (2, 0), x0.At (3, 0)), k, armijo ? "Armijo" : "Seção Áurea", kBusca, String.Format ("[{0}, {1}, {2}, {3}]", xK.At (0, 0), xK.At (1, 0), xK.At (2, 0), xK.At (3, 0)), Functions.f(xK)));
                sw.Flush ();
            }

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
            if (k >= 500)     // Número max iterações
            {
                return true;
            }
            if (xK.Equals(xK_1))   // x(k) = x(k-1)
            {
                return true;
            }

            return false;
        }
    }
}
