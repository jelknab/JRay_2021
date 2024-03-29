using JRay_2021.primitives;
using System.Numerics;

namespace JRay_2021.renderObjects
{
    public class SphereHollow : Sphere
    {
        public override float Intersect(Ray ray) // https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection
        {
            var l = ray.Origin - Center;
            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = 2 * Vector3.Dot(ray.Direction, l);
            var c = Vector3.Dot(l, l) - Radius2;

            if (!solveQuadratic(a, b, c, out float t0, out float t1)) return 0;

            if (t0 > t1) (t0, t1) = (t1, t0);

            if (t0 < 0)
            {
                t0 = t1;
                if (t0 < 0) return 0;
            }

            return (float)t0;
        }

        private bool solveQuadratic(float a, float b, float c, out float x0, out float x1)
        {
            float discr = b * b - 4 * a * c;
            x0 = 0;
            x1 = 0;

            if (discr < 0) return false;

            if (discr == 0)
            {
                x0 = x1 = -0.5f * b / a;
            }
            else
            {
                float sqrt = FastNumerics.Sqrt(discr);
                float q = (b > 0) ?
                    -0.5f * (b + sqrt) :
                    -0.5f * (b - sqrt);
                x0 = q / a;
                x1 = c / q;
            }

            return true;
        }

        public override Vector3 HitNormal(Intersection intersection)
        {
            return Vector3.Normalize(intersection.Position - Center);
        }
    }
}