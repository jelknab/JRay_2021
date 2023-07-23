using JRay_2021.primitives;

namespace JRay_2021
{
    public class Sample
    {
        public required Ray Ray { get; set; }

        public SampledColor SampledColor { get; set; } = SampledColor.Black;

        public required float Effect { get; set; }

        public Sample? Parent { get; set; }
    }
}
