using System;
using System.Linq;

namespace Numeric_lab5
{
    class Program
    {
        // u = (1 + t)^3 + 2x
        // u = t^3 + x 
        /*public static double Uxt(double x, double t) => Math.Pow((1 + t), 3) + 2 * x;
        public static double Fi(double x) => 2 * x;
        public static double M1(double t) => Math.Pow((1 + t), 3);
        public static double M2(double t) => Math.Pow((1 + t), 3) + 2;*/
        public static double Uxt(double x, double t) => t*t*t + x;
        public static double Fi(double x) => x;
        public static double M1(double t) => t*t*t;
        public static double M2(double t) => t*t*t+1;

        public static void RightProgonk(int N, int M, double G)
        {
            double h = 1.0 / M;
            double ty = 1.0 / N;
            double a, c, b, f;
            double[] Y = new double[M+1], A = new double[M+1], B = new double[M+1];
            
            a = c = G;
            b = 2 * G + (h * h) / ty;

            for (int m = 0; m <= M; m++)
                Y[m] = Fi(m * h);

            for (int n = 0; n <= N; n++)
            {
                A[0] = 0;
                B[0] = M1(ty*n);

                for (int m = 1; m <= M-1; m++)
                {
                    f = a * Y[m - 1] - b * Y[m] + c * Y[m + 1];
                    
                    A[m+1] = c / (b - A[m] * a);
                    B[m+1] = (B[m] * a - f) / (b - a * A[m]);
                }

                Y[M] = M2(ty * n);
                for (int m = M; m >= 1; m--)
                {
                    Y[m - 1] = A[m] * Y[m] + B[m];
                }
                
                Console.WriteLine("  m |   U[m]  |    y[m]   | esp");
                for (int i = 0; i <= M; i++)
                {
                    double Ux0 = Uxt(i*h, 1);
                    Console.WriteLine($" {i,2} |{Ux0,8:f4} | {Y[i],8:f4}  |{Math.Abs(Ux0-Y[i]):f4}");
                }
                Console.WriteLine("\n\n");
            }
            
            Console.WriteLine("  m |   U[m]  |    y[m]   | esp");
            for (int i = 0; i <= M; i++)
            {
                double Ux0 = Uxt(i*h, 1);
                Console.WriteLine($" {i,2} |{Ux0,8:f4} | {Y[i],8:f4}  |{Math.Abs(Ux0-Y[i]):f4}");
            }

        }
        
        static void Main(string[] args)
        {
            RightProgonk(10,10,1);
        }
    }
}