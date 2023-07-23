using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Reflection;
using JRay_2021.primitives;

namespace JRay_2021.materials
{
    public class BrdfMaterial : IMaterial
    {
        public Scene Scene { get; set; }

        private readonly int _sampleCount = 32;
        
        public void Render(Intersection intersection, Stack<Sample> sampleStack, Sample sample)
        {
            var incidentDirection = Vector3.Reflect(intersection.Ray.Direction, intersection.HitNormal);

            if (sample.Effect < 0.005)
            {
                return;
            }

            for (int i = 0; i < _sampleCount; i++) {
                var reflectedRay = new Ray
                {
                    Origin = intersection.Position + intersection.HitNormal,
                    Direction = CalculateDiffuseReflectionDirection(incidentDirection, intersection.HitNormal)
                };

                sampleStack.Push(new Sample
                {
                    Effect = sample.Effect / (float) _sampleCount,
                    Ray = reflectedRay,
                    Parent = sample,
                });
            }
        }

        public static Vector3 CalculateDiffuseReflectionDirection(Vector3 surfaceNormal, Vector3 incidentDirection)
        {
            Vector3 randomDirection = GenerateRandomDirectionInHemisphere(surfaceNormal);
            Vector3 reflectionDirection = incidentDirection - 2 * Vector3.Dot(incidentDirection, surfaceNormal) * surfaceNormal;
            Vector3 diffuseDirection = reflectionDirection + randomDirection;

            return Vector3.Normalize(diffuseDirection);
        }

        public static Vector3 GenerateRandomDirectionInHemisphere(Vector3 surfaceNormal)
        {
            Random random = new Random();
            float u = (float)random.NextDouble();
            float v = (float)random.NextDouble();
            float theta = 2f * (float)Math.PI * u;
            float phi = (float)Math.Acos(2 * v - 1);

            float x = (float)(Math.Sin(phi) * Math.Cos(theta));
            float y = (float)(Math.Sin(phi) * Math.Sin(theta));
            float z = (float)Math.Cos(phi);

            Vector3 randomDirection = new Vector3(x, y, z);

            // Ensure the random direction is in the hemisphere defined by the surface normal
            if (Vector3.Dot(randomDirection, surfaceNormal) < 0)
            {
                randomDirection = -randomDirection;
            }

            return randomDirection;
        }
    }
}