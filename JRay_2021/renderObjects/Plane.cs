using System.Numerics;
using JRay_2021.materials;
using JRay_2021.primitives;

namespace JRay_2021.renderObjects
{
    public class Plane : IRenderObject
    {
        private Vector3 _normal;
        
        public Vector3 Position { get; set; }

        public Vector3 Normal
        {
            get => _normal;
            set => _normal = Vector3.Normalize(value);
        }

        public IMaterial Material { get; set; }
        
        public float Intersect(Ray ray)
        {
            var denominator = Vector3.Dot(Normal, ray.Direction);

            if (denominator < 1e-6) return 0;

            return Vector3.Dot(Position - ray.Origin, Normal) / denominator;
        }

        public Vector3 HitNormal(Intersection intersection)
        {
            return Normal;
        }
    }
}