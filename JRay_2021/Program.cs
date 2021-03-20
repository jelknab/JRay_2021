using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using JRay_2021.materials;
using JRay_2021.renderObjects;
using Plane = JRay_2021.renderObjects.Plane;

namespace JRay_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var image = new Image(1024, 1024);
            var scene = new Scene
            {
                Camera = new Camera(0, 0, 0)
                {
                    Position = new Vector3 {X = 0, Y = 0, Z = 0},
                    Direction = Vector3.UnitZ,
                    FieldOfView = 90
                }
            };

            var sphereRed = new Sphere
            {
                Center = new Vector3(-2, 0, -10),
                Radius = 2,
                Material = new BrdfMaterial
                {
                    Scene = scene
                }
            };

            var sphereBlue = new Sphere
            {
                Center = new Vector3(2, 0, -10),
                Radius = 2,
                Material = new HitNormalMaterial()
            };

            var encapsulatingSphere = new SphereHollow
            {
                Center = Vector3.Zero,
                Radius = 12f,
                Material = new FullBright {Color = Color.FromArgb(0, 255, 0)}
            };

            var floorPlane = new Plane
            {
                Position = new Vector3(0, -2f, 0),
                Normal = new Vector3(0, -1, 0),
                Material = new FullBright {Color = Color.FromArgb(100, 100, 100)}
            };
            
            var leftPlane = new Plane
            {
                Position = new Vector3(-4, 0, 0),
                Normal = new Vector3(-1, 0, 0),
                Material = new FullBright {Color = Color.FromArgb(255, 0, 0)}
            };
            
            var rightPlane = new Plane
            {
                Position = new Vector3(4, 0, 0),
                Normal = new Vector3(1, 0, 0),
                Material = new FullBright {Color = Color.FromArgb(0, 0, 255)}
            };

            scene.RenderObjects = new List<IRenderObject>()
            {
                sphereRed,
                sphereBlue,
                encapsulatingSphere,
                floorPlane,
                leftPlane,
                rightPlane
            };
            
            scene.Render(image);

            var pictureDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                "JRay"
            );

            Directory.CreateDirectory(pictureDirectory);

            image.SavePng(Path.Combine(pictureDirectory, $"{DateTime.Now:yyyyMMdd hhmmss}"));
        }
    }
}