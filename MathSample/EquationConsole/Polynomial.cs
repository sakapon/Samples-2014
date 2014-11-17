using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EquationConsole
{
    public struct Polynomial
    {
        public static readonly Polynomial X = new Polynomial(new Dictionary<int, double> { { 1, 1 } });

        static readonly Dictionary<int, double> _coefficients_empty = new Dictionary<int, double>();
        Dictionary<int, double> _coefficients;

        Dictionary<int, double> Coefficients
        {
            get { return _coefficients == null ? _coefficients_empty : _coefficients; }
        }

        public int Dimension
        {
            get { return Coefficients.Count == 0 ? 0 : Coefficients.Max(d => d.Key); }
        }

        public Polynomial(Dictionary<int, double> coefficients)
        {
            _coefficients = coefficients;
        }

        public static implicit operator Polynomial(double value)
        {
            return value == 0
                ? default(Polynomial)
                : new Polynomial(new Dictionary<int, double> { { 0, value } });
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

        public static Polynomial operator +(Polynomial p)
        {
            return p;
        }

        public static Polynomial operator -(Polynomial p)
        {
            return -1 * p;
        }

        static void AddMonomial(Dictionary<int, double> coefficients, int degree, double coefficient)
        {
            if (coefficients.ContainsKey(degree))
            {
                coefficient += coefficients[degree];
            }

            if (coefficient != 0)
            {
                coefficients[degree] = coefficient;
            }
            else
            {
                coefficients.Remove(degree);
            }
        }

        public ReadOnlyDictionary<int, double> GetCoefficients()
        {
            return new ReadOnlyDictionary<int, double>(Coefficients);
        }

        public double Substitute(double value)
        {
            throw new NotImplementedException();
        }

        public double SolveEquation(double operand)
        {
            throw new NotImplementedException();
        }
    }
}
