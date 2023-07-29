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


        public void Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {

            foreach (var material in Materials)
            {
                var childSample = new Sample
                {
                    Effect = sample.Effect * material.Effect,
                    Direction = sample.Direction,
                    Origin = sample.Origin,
                    Parent = sample.Parent
                };

                material.Material.Render(intersection, sampleStack, childSample);

                sample.SampledColor = new SampledColor
                {
                    R = sample.SampledColor.R + childSample.SampledColor.R * material.Effect,
                    G = sample.SampledColor.G + childSample.SampledColor.G * material.Effect,
                    B = sample.SampledColor.B + childSample.SampledColor.B * material.Effect
                };
            }
        }
    }
}
