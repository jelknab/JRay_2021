using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using JRay_2021.primitives;
using JRay_2021.renderObjects;

namespace JRay_2021
{
    public class Scene
    {
        public Camera Camera { get; set; }
        
        public List<IRenderObject> RenderObjects { get; set; }

        public void Render(Image image)
        {
            foreach (var (x, y, _) in image.PixelEnumerator())
            {
                var primaryRay = Camera.RasterToPrimaryRay(image, x, y);

                var closestIntersection = Intersect(primaryRay);

                if (closestIntersection != null)
                {
                    var color = closestIntersection.RenderObject.Material.Render(closestIntersection);

                    image.PixelGrid[y, x] = new Pixel(color);
                }
                else
                {
                    image.PixelGrid[y, x] = Pixel.Black;
                }
            }
        }

        public Intersection Intersect(Ray ray)
        {
            var t = float.MaxValue;
            IRenderObject closestObject = null;
            
            foreach (var renderObject in RenderObjects)
            {
                var intersectT = renderObject.Intersect(ray);

                if (intersectT == 0 || !(intersectT < t)) continue;
                
                t = intersectT;
                closestObject = renderObject;
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