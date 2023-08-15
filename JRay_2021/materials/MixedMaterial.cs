using JRay_2021.primitives;
using System.Collections.Generic;

namespace JRay_2021.materials
{
    public struct MixedMaterialMaterial
    {
        public required IMaterial Material { get; set; }
        public required float Effect { get; set; }
    }

    public class MixedMaterial : IMaterial
    {
        public required List<MixedMaterialMaterial> Materials { get; set; }


        public Color Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var result = new Color();

            foreach (var material in Materials)
            {
                var childSample = new Sample
                {
                    Effect = sample.Effect * material.Effect,
                    Direction = sample.Direction,
                    Origin = sample.Origin,
                    Depth = sample.Depth + 1,
                };

                var sampled = material.Material.Render(intersection, sampleStack, childSample);

                result.R += sampled.R * childSample.Effect;
                result.G += sampled.G * childSample.Effect;
                result.B += sampled.B * childSample.Effect;
            }

            return result;
        }
    }
}
