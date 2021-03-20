using System.Drawing;

namespace JRay_2021.primitives
{
    public class Pixel
    {
        public static readonly Pixel Black = new Pixel();
        
        public Pixel(Color color)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
        }

        private Pixel()
        {
            
        }

        public float R { get; set; } = 0;

        public float G { get; set; } = 0;

        public float B { get; set; } = 0;
    }
}