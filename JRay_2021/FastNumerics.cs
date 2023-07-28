﻿using System.Numerics;
using System.Runtime.InteropServices;

namespace JRay_2021
{
    //https://gist.github.com/SaffronCR/b0802d102dd7f262118ac853cd5b4901
    public static class FastNumerics
    {
        [StructLayout(LayoutKind.Explicit)]
        private struct FloatIntUnion
        {
            [FieldOffset(0)]
            public float f;

            [FieldOffset(0)]
            public int tmp;
        }

        public static float Sqrt(float z)
        {
            if (z == 0) return 0;
            FloatIntUnion u;
            u.tmp = 0;
            u.f = z;
            u.tmp -= 1 << 23; // Subtract 2^m.
            u.tmp >>= 1; // Divide by 2.
            u.tmp += 1 << 29; // Add ((b + 1) / 2) * 2^m.
            return u.f;
        }

        public static float InvSqrt(float z)
        {
            if (z == 0) return 0;
            FloatIntUnion u;
            u.tmp = 0;
            float xhalf = 0.5f * z;
            u.f = z;
            u.tmp = 0x5f375a86 - (u.tmp >> 1);
            u.f = u.f * (1.5f - xhalf * u.f * u.f);
            return u.f * z;
        }

        public static Vector3 Normalize(Vector3 vector)
        {
            var length = InvSqrt(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z);
            return vector / length;
        }
    }
}
