using System.Drawing;
using JRay_2021.primitives;

namespace JRay_2021.materials
{
    public class HitNormalMaterial : IMaterial
    {
        public Color Render(Intersection intersection)
        {
            var n = intersection.HitNormal;
            return Color.FromArgb(
                (int) ((1 + n.X) / 2 * 255),
                (int) ((1 + n.Y) / 2 * 255),
                (int) ((1 + n.Z) / 2 * 255)
            );
        }
    }
}