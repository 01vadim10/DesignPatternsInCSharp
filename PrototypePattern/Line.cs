using System;

namespace PrototypePattern
{
    public class Line
    {
        public Point Start, End;

        public Line()
        {
            
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}\n{nameof(End)}: {End}";
        }

        public Line DeepCopy()
        {
            return this.DeepCopy<Line>();
        }
    }
}
