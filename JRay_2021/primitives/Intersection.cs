using JRay_2021.renderObjects;
using System.Numerics;

namespace JRay_2021.primitives
{
    public class Intersection
    {
        public required IRenderObject RenderObject { get; set; }

        public required Ray Ray { get; set; }

        public required float Distance { get; set; }

        private Vector3? _position;
        public Vector3 Position
        {
            get
            {
                _position ??= Ray.Origin + Ray.Direction * Distance;
                return _position.Value;
            }
        }

        private Vector3? _hitNormal;
        public Vector3 HitNormal
        {
            get
            {
                _hitNormal ??= RenderObject.HitNormal(this);
                return _hitNormal.Value;
            }
        }
    }
}