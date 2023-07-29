using JRay_2021.primitives;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;

namespace JRay_2021
{
    public class Image
    {
        public SampledColor[,] PixelGrid { get; }

        public float AspectRatio { get; }

        public double Exposure { get; set; }

        public int Width => PixelGrid.GetLength(1);

        public int Height => PixelGrid.GetLength(0);

        public Image(int width, int height, float exposure)
        {
            PixelGrid = new SampledColor[height, width];
            AspectRatio = (float)width / height;
            Exposure = exposure;
        }

        public IEnumerable<(int x, int y, SampledColor pixel)> PixelEnumerator()
        {
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    yield return (x, y, PixelGrid[y, x]);
        }

        public void SaveImage(string destination, SKEncodedImageFormat format)
        {
            var imageInfo = new SKImageInfo(Width, Height);
            using var surface = SKSurface.Create(imageInfo);

            var canvas = surface.Canvas;

            foreach (var (x, y, pixel) in PixelEnumerator())
            {
                var color = new SKColor(
                        (byte)(pixel.R * 255),
                        (byte)(pixel.G * 255),
                        (byte)(pixel.B * 255)
                    );
                canvas.DrawPoint(x, y, color);
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(format, 95);
            using var stream = File.OpenWrite($"{destination}.{format}");
            data.SaveTo(stream);
        }
    }
}