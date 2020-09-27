using System;

namespace PrototypePattern
{
    public class Line : IPrototype<Line>
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}\n{nameof(End)}: {End}";
        }

        public Line DeepCopy()
        {
            return new Line(Start.DeepCopy(), End.DeepCopy());
        }
    }
}
