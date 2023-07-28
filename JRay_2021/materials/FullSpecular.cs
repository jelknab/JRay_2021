using JRay_2021.primitives;
using System.Collections.Generic;
using System.Numerics;

namespace JRay_2021.materials
{
    public class FullSpecular : IMaterial
    {
        public Scene Scene { get; set; }

        public void Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var reflect = Vector3.Reflect(intersection.Ray.Direction, intersection.HitNormal);

            var reflectedRay = new Ray
            {
                Origin = intersection.Position + intersection.HitNormal * 0.01f,
                Direction = reflect
            };

            sampleStack.Push(new Sample
            {
                Effect = 1,
                Ray = reflectedRay,
                Parent = sample
            });
        }
    }
}