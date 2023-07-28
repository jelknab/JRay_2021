using JRay_2021.materials;
using JRay_2021.renderObjects;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Plane = JRay_2021.renderObjects.Plane;

namespace JRay_2021.testScenes;

public class Scene1 : Scene
{
    public Scene1()
    {
        var sphereRed = new Sphere
        {
            Center = new Vector3(0, -2, -10),
            Radius = 2,
            Material = new BrdfMaterial
            {
                Scene = this
            }
        };

        var rightSphere = new Sphere
        {
            Center = new Vector3(4, 0, -10),
            Radius = .5f,
            Material = new HitNormalMaterial()
        };

        var encapsulatingSphere = new SphereHollow
        {
            Center = Vector3.Zero,
            Radius = 12f,
            Material = new BrdfMaterial
            {
                Scene = this
            }
        };

        var floorPlane = new Plane
        {
            Position = new Vector3(0, -2f, 0),
            Normal = new Vector3(0, -1, 0),
            Material = new FullBright { Color = Color.FromArgb(100, 100, 100) }
        };

        var leftPlane = new Plane
        {
            Position = new Vector3(-4, 0, 0),
            Normal = new Vector3(-1, 0, 0),
            Material = new FullBright { Color = Color.FromArgb(255, 0, 0) }
        };

        var rightPlane = new Plane
        {
            Position = new Vector3(4, 0, 0),
            Normal = new Vector3(1, 0, 0),
            Material = new FullBright { Color = Color.FromArgb(0, 0, 255) }
        };

        this.RenderObjects = new List<IRenderObject>()
        {
            sphereRed,
            rightSphere,
            encapsulatingSphere,
            floorPlane,
            leftPlane,
            rightPlane
        };
    }
}