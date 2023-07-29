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

        public int _samplesPerPixel = 4;

        public async Task Render(Image image)
        {
            foreach (var block in image.PixelEnumerator().Batch(32))
            {
                var tasks = block.Select(pixel => Task.Run(() => RenderPixel(image, pixel.x, pixel.y)));
                await Task.WhenAll(tasks);
            }
        }

        public void RenderPixel(Image image, int x, int y)
        {
            for (int sample = 0; sample < _samplesPerPixel; sample++)
            {
                var primaryRay = Camera.RasterToPrimaryRay(image, x, y, sample, _samplesPerPixel);
                var sampleStack = new Stack<Sample>();

                var initialSample = new Sample
                {
                    Effect = 1f,
                    Parent = null,
                    Origin = primaryRay.Origin,
                    Direction = primaryRay.Direction,
                    SampledColor = SampledColor.Black
                };

                sampleStack.Push(initialSample);

                do
                {
                    var lastItem = sampleStack.Pop();
                    var closestIntersection = FindClosestIntersection(lastItem);

                    if (closestIntersection is null)
                    {
                        continue;
                    }

                    closestIntersection.RenderObject.Material.Render(closestIntersection, sampleStack, lastItem);

                    if (lastItem == initialSample)
                    {
                        continue;
                    }

                    initialSample.SampledColor = new SampledColor
                    {
                        R = initialSample.SampledColor.R + lastItem.SampledColor.R * lastItem.Effect,
                        G = initialSample.SampledColor.G + lastItem.SampledColor.G * lastItem.Effect,
                        B = initialSample.SampledColor.B + lastItem.SampledColor.B * lastItem.Effect,
                    };
                } while (sampleStack.Count > 0);

                image.PixelGrid[y, x] = new SampledColor
                {
                    R = (image.PixelGrid[y, x]?.R ?? 0) + initialSample.SampledColor.R / (float) _samplesPerPixel,
                    G = (image.PixelGrid[y, x]?.G ?? 0) + initialSample.SampledColor.G / (float) _samplesPerPixel,
                    B = (image.PixelGrid[y, x]?.B ?? 0) + initialSample.SampledColor.B / (float) _samplesPerPixel
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

                if (intersectT == 0 || !(intersectT < maxDistance)) continue;

                maxDistance = intersectT;
                closestObject = renderObject;
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