using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;

namespace TrabalhoOtim
{
    class Program
    {
        private static double[] _x0;
        private static double[] _d0;

        static void Main (string[] args)
        {
            if (!ParseOptions())
            {
                Console.WriteLine ("Error parsing options!");
                return;
            }


            //GetMinimum.Execute();

            //}
            //Matrix<double> d = DenseMatrix.OfArray(new Double[,] {
            //    {-1},
            //    {-1},
            //    {1},
            //    {1}
            //});

            //Console.WriteLine (String.Concat(">>>> ",d.Transpose().Multiply(Functions.d2f(0,0,0,0)).Multiply(d)));

            //// Iterando sobre o vetor inicial
            //for (int i = 0; i < _x0.Length; i++)
            //{
            //    Console.WriteLine (_x0[i].ToString("0.00"));
            //}

            //// Calculando o passo usando Armijo
            //double t = Armijo.Execute( DenseMatrix.OfArray(new Double[,] { { 1 }, { 0 } }),
            //                           DenseMatrix.OfArray(new Double[,] { { 3 }, { 1 } })
            //                          );


            Matrix<double> x = DenseMatrix.OfArray (new Double[,] { 
            { _x0[0] }, 
            { _x0[1] },
            { _x0[2] },
            { _x0[3] }
            });

            Console.WriteLine (Functions.f(x));

            for (double rho = 1.0; rho < 1000; rho = rho*10)
            {
                ExteriorPenalty.Execute (x, rho, 3.0, false);
                ExteriorPenalty.Execute (x, rho, 3.0, true);
            }

            Console.ReadKey ();
        }

        //private static void Execute(Matrix<double> x, Matrix<double> d, double p0, double B = 3.0)
        //{
        //    bool   stablized   = false;
        //    double p           = p0;
        //    Matrix<double> min = DenseMatrix.OfArray (new Double[,] {
        //    {0.0},
        //    {0.0},
        //    {0.0},
        //    {0.0}
        //    });
        //    for (int k = 0; k < 1000; k++)
        //    {
        //        // Gradiente da função penalizada
        //        min = Gradient.Execute (x, d);
        //        if (stablized)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            p = B * p;
        //        }
        //    }

        //    Console.WriteLine("p = " + p.ToString());
        //    Console.WriteLine(String.Format("Ponto mínimo = [{0}, {1}, {2}, {3}]", min.At(0,0), min.At(1,0), min.At(2,0), min.At(3,0)));
        //}

        private static bool ParseOptions ()
        {
            string x0 = ConfigurationManager.AppSettings.Get ("x0");

            try
            {
                string[] splittedArray = x0.Split (';');
                _x0 = new double[] { double.Parse (splittedArray[0]), double.Parse (splittedArray[1]), double.Parse (splittedArray[2]), double.Parse (splittedArray[3]) };
            }
            catch (Exception ex)
            {
                Console.WriteLine (ex.Message);
                return false;
            }

            return true;
        }
    }
}
