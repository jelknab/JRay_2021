using JRay_2021.primitives;
using System.Collections.Generic;

namespace JRay_2021.materials
{
    public interface IMaterial
    {
        public SampledColor Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample);
    }
}