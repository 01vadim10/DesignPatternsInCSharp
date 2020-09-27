using System;

namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = new Point {X = 1, Y = 3};
            var end = new Point {Y = 2, X = 4};
            var line = new Line {Start = start, End = end};
            var line2 = line.DeepCopy();
            line2.Start.X = 8;

            Console.WriteLine($"Line: {line}");
            Console.WriteLine($"Line2: {line2}");
            Console.ReadKey();
        }
    }
}
