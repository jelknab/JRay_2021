using JRay_2021.primitives;
using JRay_2021.renderObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JRay_2021
{
    public class Scene
    {
        public required Camera Camera { get; set; }

        public List<IRenderObject> RenderObjects { get; set; } = new List<IRenderObject>();

        private static readonly int _maxDepth = 4;

        private static readonly int _samplesPerPixel = 4;

        public async Task Render(Image image)
        {
            var blockTasks = image
                .PixelEnumerator()
                .Batch(128)
                .Select(pixelBlock => Task.Run(() => RenderPixelBlock(image, pixelBlock)));

            await Task.WhenAll(blockTasks);
        }

        public void RenderPixelBlock(Image image, IEnumerable<Pixel> pixels)
        {
            var sampleStack = new Stack<Sample>(512);

            foreach (var pixel in pixels)
            {
                RenderPixel(image, pixel.X, pixel.Y, sampleStack);
            }
        }

        public void RenderPixel(Image image, int x, int y, Stack<Sample> sampleStack)
        {
            for (int sample = 0; sample < _samplesPerPixel; sample++)
            {
                var primaryRay = Camera.RasterToPrimaryRay(image, x, y, sample, _samplesPerPixel);

                var initialSample = new Sample
                {
                    Effect = 1f,
                    Origin = primaryRay.Origin,
                    Direction = primaryRay.Direction,
                    Depth = 0
                };

                sampleStack.Push(initialSample);

                Color result = new();

                do
                {
                    var lastItem = sampleStack.Pop();
                    var closestIntersection = FindClosestIntersection(lastItem);

                    if (closestIntersection is null || lastItem.Depth > _maxDepth)
                    {
                        continue;
                    }

                    var sampled = closestIntersection.RenderObject.Material.Render(closestIntersection, sampleStack, lastItem);

                    result.R += sampled.R * lastItem.Effect;
                    result.G += sampled.G * lastItem.Effect;
                    result.B += sampled.B * lastItem.Effect;
                } while (sampleStack.Count > 0);

                image.PixelGrid[y, x] = new Color
                {
                    R = image.PixelGrid[y, x].R + result.R / _samplesPerPixel,
                    G = image.PixelGrid[y, x].G + result.G / _samplesPerPixel,
                    B = image.PixelGrid[y, x].B + result.B / _samplesPerPixel
                };
            }
        }

        public Intersection? FindClosestIntersection(Ray ray)
        {
            var maxDistance = float.MaxValue;
            IRenderObject? closestObject = null;

            foreach (var renderObject in RenderObjects)
            {
                var intersectT = renderObject.Intersect(ray);

                if (intersectT > 0 && intersectT < maxDistance)
                {
                    maxDistance = intersectT;
                    closestObject = renderObject;
                }
            }

            if (closestObject is null)
            {
                return null;
            }

            return new Intersection
            {
                Distance = maxDistance - 0.00001f, //minus a small value to get it off the surface.
                Ray = ray,
                RenderObject = closestObject
            };
        }
    }
}