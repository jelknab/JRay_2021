using System.Collections.Generic;
using System.Drawing;
using JRay_2021.primitives;

namespace JRay_2021.materials
{
    public interface IMaterial
    {
        public void Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample);
    }
}