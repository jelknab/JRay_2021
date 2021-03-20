using System.Drawing;
using JRay_2021.primitives;

namespace JRay_2021.materials
{
    public class FullBright : IMaterial
    {
        public Color Color { get; set; }
        
        public Color Render(Intersection intersection)
        {
            return Color;
        }
    }
}