using System;

namespace PrototypePattern
{
    public class Point : IPrototype<Point>
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"{nameof(X)}: {X}\n{nameof(Y)}: {Y}";

        public Point DeepCopy()
        {
            return new Point(X, Y);
        }
    }
}
