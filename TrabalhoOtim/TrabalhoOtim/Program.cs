using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary;
using MathNet.Numerics.LinearAlgebra.Double;

namespace TrabalhoOtim
{
    class Program
    {
        private static double[] _x0;

        static void Main (string[] args)
        {
            if (!ParseOptions())
            {
                Console.WriteLine ("Error parsing options!");
                return;
            }

            // Iterando sobre o vetor inicial
            for (int i = 0; i < _x0.Length; i++)
            {
                Console.WriteLine (_x0[i].ToString("0.00"));
            }

            // Calculando o passo usando Armijo
            double t = Armijo.Execute( DenseMatrix.OfArray(new Double[,] { { 1 }, { 0 } }),
                                       DenseMatrix.OfArray(new Double[,] { { 3 }, { 1 } })
                                      );


            Console.WriteLine(Gradient.Execute());

            Console.WriteLine ("Passo: " + t.ToString("0.00"));

            Console.ReadKey ();
        }

        private static bool ParseOptions ()
        {
            string x0 = ConfigurationManager.AppSettings.Get ("x0");

            try
            {
                string[] splittedArray = x0.Split (';');
                _x0 = new double[] { double.Parse(splittedArray[0]), double.Parse (splittedArray[1]) };
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
