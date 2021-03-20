using System;
using System.Numerics;
using JRay_2021.primitives;

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
                Scale = (float) Math.Tan(DegreeToRadian(FieldOfView * 0.5));
            }
        }

        public float Scale { get; private set; }

        public Matrix4x4 CameraToWorld { get; }

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
            return (float) (Math.PI / 180 * degree);
        }

        public Ray RasterToPrimaryRay(Image image, int x, int y)
        {
            var primaryX = (2 * (x + 0.5) / image.Width - 1) * image.AspectRatio * Scale;
            var primaryY = (1 - 2 * (y + 0.5) / image.Height) * Scale;

            return new Ray
            {
                Origin = Position,
                Direction = Vector3.Normalize(
                    CameraToWorld.MultDirMatrix(
                        new Vector3(
                            (float) primaryX,
                            (float) primaryY,
                            -1
                        )
                    )
                )
            };
        }
    }
}