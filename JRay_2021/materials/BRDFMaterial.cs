using JRay_2021.primitives;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JRay_2021.materials
{
    public class BrdfMaterial : IMaterial
    {
        public required Scene Scene { get; set; }

        private readonly int _sampleCount = 10;
        private static readonly Random _random = new();

        public void Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var incidentDirection = Vector3.Reflect(intersection.Ray.Direction, intersection.HitNormal);

            if (sample.Effect < 0.005)
            {
                return;
            }

            for (int i = 0; i < _sampleCount; i++)
            {
                var reflectedRay = new Ray
                {
                    Origin = intersection.Position + intersection.HitNormal,
                    Direction = CalculateDiffuseReflectionDirection(incidentDirection, intersection.HitNormal)
                };

                sampleStack.Push(new Sample
                {
                    Effect = sample.Effect / (float)_sampleCount,
                    Ray = reflectedRay,
                    Parent = sample,
                });
            }
        }

        public static Vector3 CalculateDiffuseReflectionDirection(Vector3 surfaceNormal, Vector3 incidentDirection)
        {
            float u = (float)_random.NextDouble();
            float v = (float)_random.NextDouble();
            float theta = 2f * MathF.PI * u;
            float phi = MathF.Acos(2 * v - 1);

            float x = MathF.Sin(phi) * MathF.Cos(theta);
            float y = MathF.Sin(phi) * MathF.Sin(theta);
            float z = MathF.Cos(phi);

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