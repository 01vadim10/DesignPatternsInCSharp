namespace BridgePattern
{
    public class Square : Shape
    {
        public string Name => "Square";

        public Square(IRenderer renderer) : base(renderer)
        {
            _renderer.WhatToRenderAs = Name;
        }

        public override string ToString()
        {
            return _renderer.ToString();
        }
    }
}
