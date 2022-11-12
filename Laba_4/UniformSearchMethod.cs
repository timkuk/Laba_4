using System;

namespace Laba_4
{
    class UniformSearchMethod
    {
        double a;
        double b;
        // Количество участков
        const double n = 1000;
        // Шаг
        double dx;

        public UniformSearchMethod(double A, double B)
        {
            a = A;
            b = B;
        }

        public double Start()
        {
            dx = (b - a) / n;
            double yMin = double.PositiveInfinity;
            double xMin = a;
            for (double x = a; x <= b; x += dx)
            {
                double y = Func(x);
                if (y < yMin)
                {
                    xMin = x;
                    yMin = y;
                }
            }
            return ((xMin + yMin) / 2);
        }

        private double Func(double x)
        {
            return Math.Sqrt(Math.Sin(x) + 2);
        }
    }
}