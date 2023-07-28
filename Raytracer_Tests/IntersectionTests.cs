using JRay_2021;
using JRay_2021.materials;
using JRay_2021.primitives;
using JRay_2021.renderObjects;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;
using Image = JRay_2021.Image;

namespace Raytracer_Tests
{
    public class IntersectionTests
    {
        [Fact]
        public void SphereIntersectionTest()
        {
            var sphere = new Sphere
            {
                Center = new Vector3(0, 0, 10),
                Radius = 1
            };
            var directRay = new Ray
            {
                Origin = Vector3.Zero,
                Direction = new Vector3(0, 0, 1)
            };
            var missingRay = new Ray
            {
                Origin = new Vector3(0, 2, 0),
                Direction = new Vector3(0, 0, 1)
            };

            Assert.Equal(9, sphere.Intersect(directRay));
            Assert.Equal(0, sphere.Intersect(missingRay));
        }


        [Fact]
        public async Task BRDFReflectionTest()
        {
            var image = new Image(100, 100, 1);
            var scene = new Scene
            {
                Camera = new Camera(0, 0, 0)
                {
                    Direction = Vector3.UnitZ,
                    FieldOfView = 90f,
                    Position = Vector3.Zero
                }
            };

            var sphere1 = new Sphere
            {
                Center = new Vector3(0, 0, -10),
                Radius = 2,
                Material = new BrdfMaterial
                {
                    Scene = scene
                }
            };

            var sphere2 = new SphereHollow
            {
                Center = sphere1.Center,
                Radius = 50,
                Material = new FullBright { Color = Color.FromArgb(255, 0, 0) }
            };

            scene.RenderObjects = new List<IRenderObject>()
            {
                sphere1,
                sphere2
            };

            await scene.Render(image);

            foreach (var (x, y, pixel) in image.PixelEnumerator())
            {
                Assert.True(pixel.R - 1.0f < 0.000001);
            }
        }
    }
}