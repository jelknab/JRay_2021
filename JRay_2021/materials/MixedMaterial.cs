using JRay_2021.primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRay_2021.materials
{
    public struct MixedMaterialMaterial
    {
        public required IMaterial Material { get; set; }
        public required float Effect { get; set; }
    }

    public class MixedMaterial: IMaterial
    {
        public required List<MixedMaterialMaterial> Materials { get; set; }


        public SampledColor Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var result = new SampledColor();

            foreach (var material in Materials)
            {
                var childSample = new Sample
                {
                    Effect = sample.Effect * material.Effect,
                    Direction = sample.Direction,
                    Origin = sample.Origin
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
