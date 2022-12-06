using System;
using System.Linq;

namespace Numeric_lab5
{
    class Program
    {
        // u = (1 + t)^3 + 2x
        public static double Uxt(double x, double t) => Math.Pow((1 + t), 3) + 2 * x;
        public static double F(double x, double t) => 3 * Math.Pow((1 + t), 2);
        public static double Fi(double x) => 2 * x;
        public static double M1(double t) => Math.Pow((1 + t), 3);
        public static double M2(double t) => Math.Pow((1 + t), 3) + 2;
        
        // u = t^3 + x 
        /*public static double Uxt(double x, double t) => t*t*t + x;
        public static double Fi(double x) => x;
        public static double M1(double t) => t*t*t;
        public static double M2(double t) => t*t*t+1;*/

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
                A[1] = 0;
                B[1] = M1(ty*n);

                for (int m = 1; m <= M-1; m++)
                {
                    //f = a * Y[m-1] - b * Y[m] + c * Y[m + 1];

                    f = -(h * h / ty) - (1 - G) * (Y[m - 1] - 2 * Y[m] + Y[m + 1]) - h * h * F(m * h, ty * (n + 0.5));

                    A[m+1] = c / (b - A[m] * a);
                    B[m+1] = (B[m] * a - f) / (b - a * A[m]);
                }

                Y[M] = M2(ty * n);
                for (int m = M; m >= 1; m--)
                {
                    Y[m - 1] = A[m] * Y[m] + B[m];
                }
            }
            Console.WriteLine($"n = {N}, G = {G}");
            Console.WriteLine("  m |   U[m]  |    y[m]   | esp");
            for (int i = 0; i <= M; i++)
            {
                double Ux1 = Uxt(i*h, 1);
                Console.WriteLine($" {i,2} |{Ux1,8:f4} | {Y[i],8:f4}  |{Math.Abs(Ux1-Y[i]):f4}");
            }
            Console.WriteLine("\n\n");

        }
        
        static void Main(string[] args)
        {
            RightProgonk(10, 10, 0.2);
            RightProgonk(10, 10, 0.5);
            RightProgonk(10, 10, 1);
            RightProgonk(15, 10, 1);
            RightProgonk(20, 10, 1);
            RightProgonk(25, 10, 1);
            RightProgonk(35, 35, 1);
        }
    }
}