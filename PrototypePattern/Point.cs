using System;

namespace PrototypePattern
{
    public class Point
    {
        public int X, Y;

        public Point()
        {
            
        }

        public override string ToString() => $"{nameof(X)}: {X}\n{nameof(Y)}: {Y}";

        public Point DeepCopy()
        {
            return this.DeepCopy<Point>();
        }
    }
}
