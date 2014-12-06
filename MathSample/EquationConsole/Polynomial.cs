using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationConsole
{
    public struct Polynomial
    {
        public static readonly Polynomial X = new Polynomial(new Dictionary<int, double> { { 1, 1 } });

        static readonly IDictionary<int, double> _coefficients_empty = new Dictionary<int, double>();
        IDictionary<int, double> _coefficients;

        IDictionary<int, double> Coefficients
        {
            get { return _coefficients == null ? _coefficients_empty : _coefficients; }
        }

        public int Degree
        {
            get { return Coefficients.Count == 0 ? 0 : Coefficients.Max(c => c.Key); }
        }

        // Substitution
        public double this[double value]
        {
            get { return Coefficients.Sum(c => c.Value * Math.Pow(value, c.Key)); }
        }

        // The dictionary represents index/coefficient pairs.
        public Polynomial(IDictionary<int, double> coefficients)
        {
            _coefficients = coefficients;
        }

        public static implicit operator Polynomial(double value)
        {
            return value == 0 ? default(Polynomial) : new Polynomial(new Dictionary<int, double> { { 0, value } });
        }

        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            var coefficients = new Dictionary<int, double>(p1.Coefficients);

            foreach (var item2 in p2.Coefficients)
            {
                AddMonomial(coefficients, item2.Key, item2.Value);
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            var coefficients = new Dictionary<int, double>(p1.Coefficients);

            foreach (var item2 in p2.Coefficients)
            {
                AddMonomial(coefficients, item2.Key, -item2.Value);
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            var coefficients = new Dictionary<int, double>();

            foreach (var item1 in p1.Coefficients)
            {
                foreach (var item2 in p2.Coefficients)
                {
                    AddMonomial(coefficients, item1.Key + item2.Key, item1.Value * item2.Value);
                }
            }
            return new Polynomial(coefficients);
        }

        public static Polynomial operator /(Polynomial p, double value)
        {
            var coefficients = new Dictionary<int, double>();

            foreach (var item in p.Coefficients)
            {
                AddMonomial(coefficients, item.Key, item.Value / value);
            }
            return new Polynomial(coefficients);
        }

        // Power
        public static Polynomial operator ^(Polynomial p, int power)
        {
            if (power < 0) throw new ArgumentOutOfRangeException("power", "The value must be non-negative.");

            Polynomial result = 1;
            for (var i = 0; i < power; i++)
            {
                result *= p;
            }
            return result;
        }

        public static Polynomial operator +(Polynomial p)
        {
            return p;
        }

        public static Polynomial operator -(Polynomial p)
        {
            return -1 * p;
        }

        static void AddMonomial(Dictionary<int, double> coefficients, int index, double coefficient)
        {
            if (coefficients.ContainsKey(index))
            {
                coefficient += coefficients[index];
            }

            if (coefficient != 0)
            {
                coefficients[index] = coefficient;
            }
            else
            {
                coefficients.Remove(index);
            }
        }

        // Solve the equation whose right operand is 0. 
        public double SolveLinearEquation()
        {
            if (Degree != 1) throw new InvalidOperationException("The degree must be 1.");

            // ax + b = 0
            var a = GetCoefficient(1);
            var b = GetCoefficient(0);

            return -b / a;
        }

        // Solve the equation whose right operand is 0. 
        public double[] SolveQuadraticEquation()
        {
            if (Degree != 2) throw new InvalidOperationException("The degree must be 2.");

            // ax^2 + bx + c = 0
            var a = GetCoefficient(2);
            var b = GetCoefficient(1);
            var c = GetCoefficient(0);
            var d = b * b - 4 * a * c;

            return d > 0 ? new[] { (-b - Math.Sqrt(d)) / (2 * a), (-b + Math.Sqrt(d)) / (2 * a) }
                : d == 0 ? new[] { -b / (2 * a) }
                : new double[0];
        }

        double GetCoefficient(int index)
        {
            return Coefficients.ContainsKey(index) ? Coefficients[index] : 0;
        }
    }
}
