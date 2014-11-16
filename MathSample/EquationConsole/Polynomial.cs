using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationConsole
{
    public struct Polynomial
    {
        public static implicit operator Polynomial(double value)
        {
            throw new NotImplementedException();
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
