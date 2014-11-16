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
            throw new NotImplementedException();
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            throw new NotImplementedException();
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            throw new NotImplementedException();
        }

        public static Polynomial operator /(Polynomial p, double value)
        {
            throw new NotImplementedException();
        }

        public static Polynomial operator +(Polynomial p)
        {
            return p;
        }

        public static Polynomial operator -(Polynomial p)
        {
            return -1 * p;
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
