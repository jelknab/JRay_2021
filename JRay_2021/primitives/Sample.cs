namespace JRay_2021.primitives
{
    public class Sample: Ray
    {
        public SampledColor SampledColor { get; set; } = SampledColor.Black;

        public required float Effect { get; set; }

        public Sample? Parent { get; set; }
    }
}
