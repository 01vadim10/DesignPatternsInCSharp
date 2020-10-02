namespace BridgePattern
{
    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs { get; set; }

        public VectorRenderer()
        {
            // WhatToRenderAs = whatToRenderAs;
        }

        public override string ToString()
        {
            return $"Drawing {WhatToRenderAs} as lines";
        }
    }
}
