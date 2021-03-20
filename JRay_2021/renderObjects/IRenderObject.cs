using System.Numerics;
using JRay_2021.materials;
using JRay_2021.primitives;

namespace JRay_2021.renderObjects
{
    public interface IRenderObject
    {
        IMaterial Material { get; set; }
        
        float Intersect(Ray ray);

        Vector3 HitNormal(Intersection intersection);
    }
}