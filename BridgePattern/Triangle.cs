namespace BridgePattern
{
    public class Triangle : Shape
    {
        public string Name => "Triangle";

        public Triangle(IRenderer renderer) : base(renderer)
        {
            renderer.WhatToRenderAs = Name;
        }

        public override string ToString()
        {
            return _renderer.ToString();
        }
    }
}
