using JRay_2021.primitives;
using System.Collections.Generic;
using Color = JRay_2021.primitives.Color;

namespace JRay_2021.materials
{
    public class FullBright : IMaterial
    {
        public System.Drawing.Color Color { get; set; }

        public Color Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            return new Color(Color);
        }
    }
}