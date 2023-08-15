using JRay_2021.primitives;
using System.Collections.Generic;
using System.Drawing;

namespace JRay_2021.materials
{
    public class FullBright : IMaterial
    {
        public Color Color { get; set; }

        public SampledColor Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            return new SampledColor(Color);
        }
    }
}