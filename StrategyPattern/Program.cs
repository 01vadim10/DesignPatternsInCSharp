using System;
using System.Numerics;
using NUnit.Framework;

namespace StrategyPattern
{
    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            return Math.Pow(b, 2) - 4 * a * c;
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var discriminant = Math.Pow(b, 2) - 4 * a * c;

            return discriminant < 0 ? double.NaN : discriminant;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var posDiscriminant = strategy.CalculateDiscriminant(a, b, c);
            var complexValue = posDiscriminant < 0
                ? (Complex)posDiscriminant
                : posDiscriminant;
            var c1 = (-b + Complex.Sqrt(complexValue)) / (2 * a);
            var c2 = (-b - Complex.Sqrt(complexValue)) / (2 * a);
            return new Tuple<Complex, Complex>(c1, c2);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void PositiveTestOrdinaryStrategy()
        {
            var strategy = new OrdinaryDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 10, 16);
            Assert.That(results.Item1, Is.EqualTo(new Complex(-2, 0)));
            Assert.That(results.Item2, Is.EqualTo(new Complex(-8, 0)));
        }

        [Test]
        public void PositiveTestRealStrategy()
        {
            var strategy = new RealDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 10, 16);
            Assert.That(results.Item1, Is.EqualTo(new Complex(-2, 0)));
            Assert.That(results.Item2, Is.EqualTo(new Complex(-8, 0)));
        }

        [Test]
        public void NegativeTestOrdinaryStrategy()
        {
            var strategy = new OrdinaryDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 4, 5);
            Assert.That(results.Item1, Is.EqualTo(new Complex(-2, 1)));
            Assert.That(results.Item2, Is.EqualTo(new Complex(-2, -1)));
        }

        [Test]
        public void NegativeTestRealStrategy()
        {
            var strategy = new RealDiscriminantStrategy();
            var solver = new QuadraticEquationSolver(strategy);
            var results = solver.Solve(1, 4, 5);
            var complexNaN = new Complex(double.NaN, double.NaN);

            Assert.That(results.Item1, Is.EqualTo(complexNaN));
            Assert.That(results.Item2, Is.EqualTo(complexNaN));

            Assert.IsTrue(double.IsNaN(results.Item1.Real));
            Assert.IsTrue(double.IsNaN(results.Item1.Imaginary));
            Assert.IsTrue(double.IsNaN(results.Item2.Real));
            Assert.IsTrue(double.IsNaN(results.Item2.Imaginary));
        }
    }
}
