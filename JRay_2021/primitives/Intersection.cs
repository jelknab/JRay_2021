using System;
using System.Numerics;
using JRay_2021.renderObjects;

namespace JRay_2021.primitives
{
    public class Intersection : IComparable
    {
        public IRenderObject RenderObject { get; set; }
        
        public Ray Ray { get; set; }
        
        public float Distance { get; set; }

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

        public int CompareTo(object obj)
        {
            return obj == null ? 1 : Distance.CompareTo(((Intersection) obj).Distance);
        }
    }
}