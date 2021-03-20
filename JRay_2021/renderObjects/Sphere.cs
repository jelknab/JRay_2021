using System;
using System.Numerics;
using JRay_2021.materials;
using JRay_2021.primitives;

namespace JRay_2021.renderObjects
{
    public class Sphere : IRenderObject
    {
        private float _radius;
        public Vector3 Center { get; set; }

        public float Radius2 { get; private set; }

        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Radius2 = value * value;
            }
        }

        public IMaterial Material { get; set; }

        public virtual float Intersect(Ray ray) // https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection
        {
            var l = Center - ray.Origin;
            var tca = Vector3.Dot(l, ray.Direction);

            if (tca < 0) return 0;
            
            var d2 = Vector3.Dot(l, l) - tca * tca; 
            
            if (d2 > Radius2) return 0;
            
            var thc = Math.Sqrt(Radius2 - d2); 
            
            var t0 = tca - thc;
            
            // If t is negative, ray started inside sphere so clamp t to zero 
            if (t0 < 0.0f) return 0;

            return (float) t0;
        }

        public virtual Vector3 HitNormal(Intersection intersection)
        {
            return Vector3.Normalize(Center - intersection.Position);
        }
    }
}