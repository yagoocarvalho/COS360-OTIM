using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public class GetMinimum
    {
        public static void Execute()
        {
            //Pontos críticos: ∇f(x1, x2, x3, x4) = 0
            //-30   -10x2   -2x3    -3x4 = 0
            //-10x1 -10     -10x3   -10x4 = 0
            // -2x1 -10x2   -40     -x4 = 0
            // -3x1 -10x2   -x3     -12 = 0

            Console.WriteLine("Verificação de pontos mínimos:");

            //Termos independentes do gradiente
            Matrix<double> solution = DenseMatrix.OfArray(new Double[,] { { 30 }, { 10 }, { 40 }, { 12 } });

            // "Solve" é uma função para resolução de sistema linear, AX=B
            var criticPoint = Functions.matrixCoeficientsGradient().Solve(solution);
            Console.WriteLine("Ponto crítico da função: x* = [{0}, {1}, {2}, {3}]\n", criticPoint.At(0, 0),
                                                    criticPoint.At(1, 0), criticPoint.At(2, 0), criticPoint.At(3, 0));


            //Retorna os valores da diagonal principal da matiz de autovalores
            var autovalores = Functions.d2f().Evd().D.Diagonal();
            Console.WriteLine("Hessiana:\n" + Functions.d2f().ToString());
            Console.WriteLine("Autovalores: [{0}, {1}, {2}, {3}]\n", autovalores.At(0).ToString("0.00"),
                                                                    autovalores.At(1).ToString("0.00"),
                                                                    autovalores.At(2).ToString("0.00"),
                                                                    autovalores.At(3).ToString("0.00"));
            bool isMinimumPoint = true;
            int countNeg = 0;
            int countPos = 0;
            foreach (Double autovalor in autovalores)
            {
                if (autovalor < 0)
                {
                    countNeg++;
                }
                else if (autovalor > 0)
                {
                    countPos++;
                }
            }

            if (countPos == 0 && countNeg > 0)
            {
                Console.WriteLine ("Ponto de máximo encontrado: x* = [{0}, {1}, {2}, {3}]", criticPoint.At (0, 0),
                                                    criticPoint.At (1, 0), criticPoint.At (2, 0), criticPoint.At (3, 0));
            }
            else if (countNeg == 0 && countPos > 0)
            {
                Console.WriteLine ("Ponto de mínimo encontrado: x* = [{0}, {1}, {2}, {3}]", criticPoint.At (0, 0),
                                                    criticPoint.At (1, 0), criticPoint.At (2, 0), criticPoint.At (3, 0));
            }
            else if (countPos > 0 && countNeg > 0)
            {
                Console.WriteLine ("Ponto de sela encontrado: x* = [{0}, {1}, {2}, {3}]", criticPoint.At (0, 0),
                                                    criticPoint.At (1, 0), criticPoint.At (2, 0), criticPoint.At (3, 0));
            }
            else
            {
                Console.WriteLine("Não foi possível classificar o ponto crítico.");
            }
        }
    }


}
