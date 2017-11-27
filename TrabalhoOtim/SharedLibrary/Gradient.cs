﻿using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class Gradient
    {
        //x1² - 24x1 + x2²-10x2
        static double f1(Matrix<double> x)
        {
            return (Math.Pow(x.At(0, 0), 2)) - 24 * x.At(0, 0) + Math.Pow(x.At(1, 0), 2) - 10 * x.At(1, 0);
        }


        static Matrix<double> df1(Matrix<double> x)
        {
            double[] gradient = new double[2];
            gradient[0] = 2*x.At(0, 0) - 24;
            gradient[1] = 2 * x.At(1, 0) - 10;

            Matrix<double> df = DenseMatrix.OfArray(new Double[,] { { gradient[0] }, { gradient[1] } });

            return df;
        }


        public static bool isVectorZero(Matrix<double> x)
        {
            return (x.At(0, 0) == 0 && x.At(0, 1) == 0);
        }

        public static Matrix<double> Execute()
        {
           
            double t = 1.0;
            Matrix<double> x = DenseMatrix.OfArray(new Double[,] { { 8 }, { 7 } });
            Matrix<double> d = DenseMatrix.OfArray(new Double[,] { { 3 }, { 1 } });

            double k = 0;

            while (!isVectorZero(x))
            {
                d = df1(x).Negate();
                t = Armijo.Execute(x,d);
                x = x.Add(d.Multiply(t));
                k = k + 1;

                Console.WriteLine(x);

                //Limite de iterações = 1000
                if (k >= 100){
                    t = -1;
                    break;
                }
                Console.WriteLine("ok");
            }
            return x;
        }
    }
}