namespace JRay_2021.primitives
{
    public struct Color
    {
        public static readonly Color Black = new();

        public Color(System.Drawing.Color color)
        {
            R = color.R / 255.0f;
            G = color.G / 255.0f;
            B = color.B / 255.0f;
        }

        public float R { get; set; } = 0;

        public float G { get; set; } = 0;

        public float B { get; set; } = 0;
    }
}