using System.Numerics;

namespace JRay_2021.primitives
{
    public static class Matrix4X4Extension
    {
        public static Vector3 MultVecMatrix(this Matrix4x4 mat, Vector3 src)
        {
            var w = src.X * mat.M14 + src.Y * mat.M24 + src.Z * mat.M34 + mat.M44;
            return new Vector3
            {
                X = (src.X * mat.M11 + src.Y * mat.M21 + src.Z * mat.M31 + mat.M41) / w,
                Y = (src.X * mat.M12 + src.Y * mat.M22 + src.Z * mat.M32 + mat.M42) / w,
                Z = (src.X * mat.M13 + src.Y * mat.M23 + src.Z * mat.M33 + mat.M43) / w
            };
        }

        public static Vector3 MultDirMatrix(this Matrix4x4 mat, Vector3 src)
        {
            return new Vector3
            {
                X = src.X * mat.M11 + src.Y * mat.M21 + src.Z * mat.M31,
                Y = src.X * mat.M12 + src.Y * mat.M22 + src.Z * mat.M32,
                Z = src.X * mat.M13 + src.Y * mat.M23 + src.Z * mat.M33
            };
        }
    }
}