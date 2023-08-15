using JRay_2021.primitives;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;

namespace JRay_2021
{
    public struct Pixel
    {
        public required int X { get; set; }
        public required int Y { get; set; }
        public required Color Color { get; set; }
    }

    public class Image
    {
        public Color[,] PixelGrid { get; }

        public float AspectRatio { get; }

        public double Exposure { get; set; }

        public int Width => PixelGrid.GetLength(1);

        public int Height => PixelGrid.GetLength(0);

        public Image(int width, int height, float exposure)
        {
            PixelGrid = new Color[height, width];
            AspectRatio = (float)width / height;
            Exposure = exposure;
        }

        public IEnumerable<Pixel> PixelEnumerator()
        {
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    yield return new Pixel
                    {
                        Color = PixelGrid[y, x],
                        X = x,
                        Y = y
                    };
        }

        public void SaveImage(string destination, SKEncodedImageFormat format)
        {
            var imageInfo = new SKImageInfo(Width, Height);
            using var surface = SKSurface.Create(imageInfo);

            var canvas = surface.Canvas;

            foreach (var pixel in PixelEnumerator())
            {
                var color = new SKColor(
                        (byte)(pixel.Color.R * 255),
                        (byte)(pixel.Color.G * 255),
                        (byte)(pixel.Color.B * 255)
                    );
                canvas.DrawPoint(pixel.X, pixel.Y, color);
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(format, 95);
            using var stream = File.OpenWrite($"{destination}.{format}");
            data.SaveTo(stream);
        }
    }
}