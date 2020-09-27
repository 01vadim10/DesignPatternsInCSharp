using System;

namespace PrototypePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = new Point(1, 3);
            var end = new Point(2, 4);
            var line = new Line(start, end);
            var line2 = line.DeepCopy();
            line2.Start.X = 8;

            Console.WriteLine($"Line: {line}");
            Console.WriteLine($"Line2: {line2}");
            Console.ReadKey();
        }
    }
}
