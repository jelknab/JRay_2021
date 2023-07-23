using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using JRay_2021.primitives;
using SkiaSharp;

namespace JRay_2021
{
    public class Image
    {
        public Pixel[,] PixelGrid { get; }

        public double AspectRatio { get; }

        public double Exposure { get; set; }

        public int Width => PixelGrid.GetLength(1);

        public int Height => PixelGrid.GetLength(0);

        public Image(int width, int height, float exposure)
        {
            PixelGrid = new Pixel[height, width];
            AspectRatio = (double)width / height;
            Exposure = exposure;
        }

        public IEnumerable<(int x, int y, Pixel pixel)> PixelEnumerator()
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
            using var data = image.Encode(format, 80);
            using var stream = File.OpenWrite($"{destination}.{format}");
            data.SaveTo(stream);
        }
    }
}