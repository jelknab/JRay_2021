using JRay_2021.primitives;
using System.Collections.Generic;
using System.Numerics;

namespace JRay_2021.materials
{
    public class FullSpecular : IMaterial
    {
        public required Scene Scene { get; set; }

        public SampledColor Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var reflect = Vector3.Reflect(intersection.Ray.Direction, intersection.HitNormal);

            sampleStack.Push(new Sample
            {
                Effect = sample.Effect,
                Origin = intersection.Position,
                Direction = reflect
            });

            return SampledColor.Black;
        }
    }
}