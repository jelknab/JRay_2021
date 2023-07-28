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

        public async Task Render(Image image)
        {
            foreach (var block in image.PixelEnumerator().Batch(16))
            {
                var tasks = block.Select(pixel => Task.Run(() => RenderPixel(image, pixel.x, pixel.y)));
                await Task.WhenAll(tasks);
            }
        }

        public void RenderPixel(Image image, int x, int y)
        {
            var primaryRay = Camera.RasterToPrimaryRay(image, x, y);
            var sampleStack = new Stack<Sample>();

            var initialSample = new Sample
            {
                Effect = 1f,
                Ray = primaryRay,
                Parent = null,
                SampledColor = SampledColor.Black
            };

            sampleStack.Push(initialSample);

            do
            {
                var lastItem = sampleStack.Pop();
                var closestIntersection = FindClosestIntersection(lastItem.Ray);

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

            image.PixelGrid[y, x] = initialSample.SampledColor;
        }

        public Intersection? FindClosestIntersection(Ray ray)
        {
            var t = float.MaxValue;
            IRenderObject? closestObject = null;

            foreach (var renderObject in RenderObjects)
            {
                var intersectT = renderObject.Intersect(ray);

                if (intersectT == 0 || !(intersectT < t)) continue;

                t = intersectT;
                closestObject = renderObject;
            }

            if (closestObject is null)
            {
                return null;
            }

            return new Intersection
            {
                Distance = t,
                Ray = ray,
                RenderObject = closestObject
            };
        }
    }
}