using JRay_2021.primitives;
using System.Collections.Generic;

namespace JRay_2021.materials
{
    public class HitNormalMaterial : IMaterial
    {
        public SampledColor Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var n = intersection.HitNormal;
            return new SampledColor
            {
                R = (1 + n.X) / 2,
                G = (1 + n.Y) / 2,
                B = (1 + n.Z) / 2
            };
        }
    }
}