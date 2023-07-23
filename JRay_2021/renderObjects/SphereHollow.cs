using System;
using System.Numerics;
using JRay_2021.primitives;

namespace JRay_2021.renderObjects
{
    public class SphereHollow : Sphere
    {
        public override float Intersect(Ray ray) // https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection
        {
            double t0, t1 = 0;
            
            var l = ray.Origin - Center; 
            var a = Vector3.Dot(ray.Direction, ray.Direction); 
            var b = 2 * Vector3.Dot(ray.Direction, l); 
            var c = Vector3.Dot(l, l) - Radius2; 
            
            if (!solveQuadratic(a, b, c, out t0, out t1)) return 0;

            if (t0 > t1) (t0, t1) = (t1, t0);

            if (t0 < 0)
            {
                t0 = t1;
                if (t0 < 0) return 0;
            }

            return (float) t0;
        }

        private bool solveQuadratic(double a, double b, double c, out double x0, out double x1) 
        { 
            double discr = b * b - 4 * a * c;
            x0 = 0;
            x1 = 0;
            
            if (discr < 0) return false; 
            
            else if (discr == 0) { 
                x0 = x1 = - 0.5 * b / a; 
            } 
            else { 
                double q = (b > 0) ? 
                    -0.5 * (b + Math.Sqrt(discr)) : 
                    -0.5 * (b - Math.Sqrt(discr)); 
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