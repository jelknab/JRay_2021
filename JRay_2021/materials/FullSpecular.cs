using System.Drawing;
using System.Numerics;
using JRay_2021.primitives;

namespace JRay_2021.materials
{
    public class FullSpecular : IMaterial
    {
        public Scene Scene { get; set; }
        
        public Color Render(Intersection intersection)
        {
            var reflect = Vector3.Reflect(intersection.Ray.Direction, intersection.HitNormal);

            var reflectedRay = new Ray
            {
                Origin = intersection.Position + intersection.HitNormal * 0.01f,
                Direction = reflect
            };
            var reflectedIntersection = Scene.FindClosestIntersection(reflectedRay);

            return reflectedIntersection?.RenderObject.Material.Render(reflectedIntersection) ?? Color.Black;
        }
    }
}