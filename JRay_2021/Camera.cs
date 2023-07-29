using JRay_2021.primitives;
using System;
using System.Numerics;

namespace JRay_2021
{
    public class Camera
    {
        private float _fieldOfView;

        public Vector3 Position { get; set; }

        public Vector3 Direction { get; set; }

        public float FieldOfView
        {
            get => _fieldOfView;
            set
            {
                _fieldOfView = value;
                Scale = (float)Math.Tan(DegreeToRadian(FieldOfView * 0.5));
            }
        }

        public float Scale { get; private set; }

        public Matrix4x4 CameraToWorld { get; }

        public readonly Random _random = new();

        public Camera(float pitch, float yaw, float roll)
        {
            CameraToWorld = Matrix4x4.CreateFromYawPitchRoll(
                DegreeToRadian(yaw),
                DegreeToRadian(pitch),
                DegreeToRadian(roll)
            );
        }

        private float DegreeToRadian(double degree)
        {
            return (float)(Math.PI / 180 * degree);
        }

        public Ray RasterToPrimaryRay(Image image, int x, int y, int sample, int _samplesPerPixel)
        {
            var s = 1.0f / _samplesPerPixel;
            var randomX = -0.5f + s * sample - s * (float) _random.NextDouble();
            var randomY = -0.5f + s * sample - s * (float) _random.NextDouble();

            var primaryX = (2f * (x + 0.5f + randomX) / image.Width - 1f) * image.AspectRatio * Scale;
            var primaryY = (1f - 2f * (y + 0.5f + randomY) / image.Height) * Scale;

            return new Ray
            {
                Origin = Position,
                Direction = Vector3.Normalize(
                    CameraToWorld.MultDirMatrix(
                        new Vector3(
                            primaryX,
                            primaryY,
                            -1
                        )
                    )
                )
            };
        }
    }
}