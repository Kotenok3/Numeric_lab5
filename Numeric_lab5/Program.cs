using System;

namespace Numeric_lab5
{
    class Program
    {

        public static double Fun(double x, double t) => Math.Pow((1 + t), 3) + 2 * x;
        public static double Fi(double x) => 2 * x + 1;
        public static double M1(double t) => Math.Pow((1 + t), 3);
        public static double M2(double t) => Math.Pow((1 + t), 3) + 2;

        public static void RightProgonk(int N, int M, double G)
        {
            double h = 1.0 / M;
            double ty = 1.0 / N;
            double a, c, b;
            double[] Y = new double[M], A = new double[M], B = new double[M];

            a = c = G;
            b = 2 * G + (h * h) / ty;
            
            for (int n = 0; n < N; n++)
            {
                A[0] = 0;
                B[0] = M1(ty*n);

                for (int m = 1; m < M-1; m++)
                {
                    A[m] = c / (b - A[m - 1] * a);
                    B[m] = (B[m - 1] * a - Fun(n * ty, m * h)) / (b - a * A[m - 1]);
                }

                Y[M-1] = M2(ty * n);
                double Ux0 = Fun(1, 1);
                Console.WriteLine($"m={M}, U[m]={Ux0:F4}, y[m]={Y[M-1]:f4}");
                for (int m = M-1; m >= 1; m--)
                {
                    Y[m - 1] = A[m] * Y[m] + B[m];
                    Ux0 = Fun(m*h, 1);
                    Console.WriteLine($"m={m}, U[m]={Ux0:f4}, y[m]={Y[m-1]:F4}");
                }
                Console.WriteLine("\n\n");
            }
            
            Console.WriteLine("well done");
            
        }
        
        static void Main(string[] args)
        {
            RightProgonk(10,25,0.2);
        }
    }
}