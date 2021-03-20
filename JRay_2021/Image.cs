using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using JRay_2021.primitives;

namespace JRay_2021
{
    public class Image
    {
        public Pixel[,] PixelGrid { get; }
        
        public double AspectRatio { get; }

        public int Width => PixelGrid.GetLength(1);

        public int Height => PixelGrid.GetLength(0);

        public Image(int width, int height)
        {
            PixelGrid = new Pixel[height, width];
            AspectRatio = (double) width / height;
        }

        public IEnumerable<(int x, int y, Pixel pixel)> PixelEnumerator()
        {
            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                yield return (x, y, PixelGrid[y, x]);
        }

        private void SaveImage(string destination, ImageCodecInfo codec, EncoderParameters encoderParameters)
        {
            using var bmp = new Bitmap(Width, Height);

            foreach (var (x, y, pixel) in PixelEnumerator())
            {
                bmp.SetPixel(x, y, Color.FromArgb(
                        (int) (pixel.R * 255),
                        (int) (pixel.G * 255),
                        (int) (pixel.B * 255)
                    )
                );
            }
            
            bmp.Save(destination, codec, encoderParameters);
        }

        public void SaveJpeg(string destination, int quality)
        {
            var codecInfo = ImageCodecInfo.GetImageEncoders().First(info => info.FormatID == ImageFormat.Jpeg.Guid);
            var encodeParameters = new EncoderParameters(1)
            {
                Param = {[0] = new EncoderParameter(Encoder.Quality, quality)}
            };
            
            SaveImage(destination + ".jpg", codecInfo, encodeParameters);
        }

        public void SavePng(string destination)
        {
            var codecInfo = ImageCodecInfo.GetImageEncoders().First(info => info.FormatID == ImageFormat.Png.Guid);
            
            SaveImage(destination + ".png", codecInfo, null);
        }
    }
}