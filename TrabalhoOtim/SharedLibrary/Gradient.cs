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
    public class Gradient
    {
        public static bool isVectorZero(Matrix<double> x)
        {
            return (x.At (0, 0) == 0 && x.At (1, 0) == 0 && x.At (2, 0) == 0 && x.At (3, 0) == 0);
        }

        public static Matrix<double> Execute(Matrix<double> x, double rho, bool armijo, out int kBusca)
        {
            double t = 1.0;
            double k = 0.0;
            Matrix<double> d;

            kBusca = 0;

            while (!isVectorZero(Functions.df_pen_ext(x, rho))) // Enquanto grad f_ext_pen != 0
            {
                d = Functions.df_pen_ext(x, rho).Negate(); // Seleciona uma direção
                if (armijo)
                    t = Armijo.Execute (x, d, rho, out kBusca);   // calcula o passo por armijo
                else
                    t = GoldenSection.Execute (x, d, rho, out kBusca); // calcula o passo por seção aurea
                x = x.Add(d.Multiply(t)); // xk+1 = xk + d*t
                k = k + 1;

                //Limite de iterações = 150
                if (k >= 500)
                {
                    break;
                }
            }

            using (StreamWriter sw = new StreamWriter (@"C:\Users\yagom\Desktop\trab-otim.txt", true))
            {
                sw.WriteLine (String.Format ("Número de iterações do gradiente: {0}", k));
                sw.Flush ();
            }

            Console.WriteLine (String.Format ("Número de iterações do gradiente: {0}", k));
            return x;
        }
    }
}
