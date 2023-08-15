using JRay_2021.primitives;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace JRay_2021.materials
{
    public class BrdfMaterial : IMaterial
    {
        public required Scene Scene { get; set; }

        private readonly int _sampleCount = 16;
        private static readonly Random _random = new();

        public Color Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var incidentDirection = Vector3.Reflect(intersection.Ray.Direction, intersection.HitNormal);

            if (sample.Effect < 0.001)
            {
                return Color.Black;
            }

            var effect = sample.Effect / _sampleCount;

            for (int i = 0; i < _sampleCount; i++)
            {
                sampleStack.Push(new Sample
                {
                    Effect = effect,
                    Origin = intersection.Position,
                    Direction = CalculateDiffuseReflectionDirection(incidentDirection, intersection.HitNormal),
                    Depth = sample.Depth + 1,
                });
            }

            return Color.Black;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 CalculateDiffuseReflectionDirection(Vector3 surfaceNormal, Vector3 incidentDirection)
        {
            float u = 2f * (float)_random.NextDouble() - 1f; // Generate a random float between -1 and 1
            float v = (float)_random.NextDouble();
            float phi = MathF.Acos(u);

            float sinPhi = MathF.Sin(phi);
            float theta = 2f * MathF.PI * v;
            var (sinTheta, cosTheta) = MathF.SinCos(theta);

            float x = sinPhi * cosTheta;
            float y = sinPhi * sinTheta;
            float z = u;

            Vector3 randomDirection = new(x, y, z);

            // Ensure the random direction is in the hemisphere defined by the surface normal
            if (Vector3.Dot(randomDirection, surfaceNormal) < 0)
            {
                randomDirection = -randomDirection;
            }

            Vector3 reflectionDirection = incidentDirection - 2 * Vector3.Dot(incidentDirection, surfaceNormal) * surfaceNormal;
            Vector3 diffuseDirection = reflectionDirection + randomDirection;

            return Vector3.Normalize(diffuseDirection);
        }
    }
}