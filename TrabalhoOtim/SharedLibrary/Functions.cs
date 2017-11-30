using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class Functions
    {
        // -30x1 -10x1x2 - 2x1x3 - 3 x1x4 - 10x2 - 10x2x3 - 10x2x4 - 40 x3 - x3x4 - 12x4
        public static double f (Matrix<double> x)
        {
            double x1 = x.At (0, 0);
            double x2 = x.At (1, 0);
            double x3 = x.At (2, 0);
            double x4 = x.At (3, 0);
            return (-30.0*x1 - 10.0*x1*x2 - 2.0*x1*x3 - 3.0*x1*x4 - 10.0*x2 - 10.0*x2*x3 - 10.0*x2*x4 - 40.0*x3 - x3*x4 - 12.0*x4);
        }

        public static Matrix<double> df (Matrix<double> x)
        {
            double x1 = x.At (0,0);
            double x2 = x.At (1,0);
            double x3 = x.At (2,0);
            double x4 = x.At (3,0);

            Matrix <double> returnObj = DenseMatrix.OfArray (new Double[,] {            
                {-30.0    - 10.0*x2 - 2.0*x3  - 3.0*x4},
                {-10.0*x1 - 10.0    - 10.0*x3 - 10.0*x4},
                {-2.0*x1  - 10.0*x2 - 40.0    - x4},
                {-3.0*x1  - 10.0*x2 - x3      - 12.0}
            });

            return returnObj;
        }

        public static Matrix<double> matrixCoeficientsGradient() {
            Matrix<double> matrixCoeficientsGradient =
                                DenseMatrix.OfArray(new Double[,] {
                                    {0.0,   -10.0, -2.0,  -3.0},
                                    {-10.0,     0, -10.0,  -10.0},
                                    {-2.0,  -10.0,   0,    -1},
                                    {-3.0,  -10.0,  -1,      0}
                                });

            return matrixCoeficientsGradient;
        }

        public static Matrix<double> d2f ()
        {
            return DenseMatrix.OfArray (new Double[,] {
                {  0.0, -10.0,  -2.0,  -3.0},
                {-10.0,   0.0, -10.0, -10.0},
                { -2.0, -10.0,   0.0,  -1.0},
                { -3.0, -10.0,  -1.0,   0.0}
            });
        }

        public static double f_pen_ext(Matrix <double> x, double rho)
        {
            double x1 = x.At (0,0);
            double x2 = x.At (1,0);
            double x3 = x.At (2,0);
            double x4 = x.At (3,0);

            double g1 = 33.0*x1 + 14.0*x2 + 47.0*x3+ 11*x4 - 59;
            double g2 = x1 - 1;
            double g3 = x2 - 1;
            double g4 = x3 - 1;
            double g5 = x4 - 1;

            // f(x) + p*E(max(0, g)^2)
            double returnValue = Functions.f (x) + (rho * (Math.Pow (Math.Max (0.0, g1), 2.0) + Math.Pow (Math.Max (0.0, g2), 2.0) + Math.Pow (Math.Max (0.0, g3), 2.0) + Math.Pow (Math.Max (0.0, g4), 2.0) + Math.Pow (Math.Max (0.0, g5), 2.0)));

            return returnValue;
        }

        public static Matrix<double> df_pen_ext (Matrix<double> x, double rho)
        {
            double x1 = x.At (0, 0);
            double x2 = x.At (1, 0);
            double x3 = x.At (2, 0);
            double x4 = x.At (3, 0);

            // Grad phi = grad f + p * grad Pen
            Matrix<double> gradF = Functions.df (x);
            Matrix<double> gradPen = GetXPen (x);

            gradF = gradF.Add (gradPen.Multiply(rho));

            return gradF;
        }

        /// <summary>
        /// Método para pegar o gradiente da função penalidade
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static Matrix<double> GetXPen (Matrix<double> x)
        {
            double x1 = x.At (0, 0);
            double x2 = x.At (1, 0);
            double x3 = x.At (2, 0);
            double x4 = x.At (3, 0);

            double g1 = 33.0*x1 + 14.0*x2 + 47.0*x3+ 11*x4 - 59;
            double g2 = x1 - 1;
            double g3 = x2 - 1;
            double g4 = x3 - 1;
            double g5 = x4 - 1;

            Matrix<double> returnMatrix = DenseMatrix.OfArray (new Double[,] {
            {0},
            {0},
            {0},
            {0}
            });

            if (g1 <= 0)
            {
                if (g2 > 0)
                {
                    returnMatrix.At (0, 0, 2 * (x1 - 1));
                }

                if (g3 > 0)
                {
                    returnMatrix.At (1, 0, 2 * (x2 - 1));
                }
                
                if (g4 > 0)
                {
                    returnMatrix.At (2, 0, 2 * (x3 - 1));
                }
            
                if (g5 > 0)
                {
                   returnMatrix.At (3, 0, 2 * (x4 - 1));
                }
            }
            else if (g1 > 0)
            {
                if (g2 <= 0)
                {
                    returnMatrix.At (0, 0, 66 * (g1));
                }
                else if (g2 > 0)
                {
                    returnMatrix.At (0, 0, 2180 * x1 + 924 * x2 + 3102 * x3 + 726 * x4 - 3896);
                }

                if (g3 <= 0)
                {
                    returnMatrix.At (1, 0, 28 * (g1));
                }
                else if (g3 > 0)
                {
                    returnMatrix.At (1, 0, 2*(462*x1 + 197*x2 + 658*x3 + 154*x4 - 827));
                }

                if (g4 <= 0)
                {
                    returnMatrix.At (2, 0, 94 * (g1));
                }
                else if (g4 > 0)
                {
                    returnMatrix.At (2, 0, 2*(1551*x1 + 658*x2 + 2210*x3 + 517*x4 - 2774));
                }

                if (g5 <= 0)
                {
                    returnMatrix.At (3, 0, 22 * (g1));
                }
                else if (g5 > 0)
                {
                    returnMatrix.At (3, 0, 2*(363*x1 + 154*x2 + 517*x3 + 122*x4 - 650));
                }
            }

            return returnMatrix;
        }
    }
}
