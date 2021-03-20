using System.Drawing;
using JRay_2021.primitives;

namespace JRay_2021.materials
{
    public interface IMaterial
    {
        public Color Render(Intersection intersection);
    }
}