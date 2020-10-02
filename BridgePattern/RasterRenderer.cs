namespace BridgePattern
{
    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs { get; set; }

        public RasterRenderer()
        {
        }

        public override string ToString()
        {
            return $"Drawing {WhatToRenderAs} as pixels";
        }
    }
}
