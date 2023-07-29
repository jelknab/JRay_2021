using JRay_2021.materials;
using JRay_2021.renderObjects;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Plane = JRay_2021.renderObjects.Plane;

namespace JRay_2021.testScenes;

public class Scene2 : Scene
{
    public Scene2()
    {
        var bottomSphere = new Sphere
        {
            Center = new Vector3(2, -3, -7),
            Radius = 1,
            Material = new HitNormalMaterial()
        };

        var middleSphere = new Sphere
        {
            Center = new Vector3(2, -1, -7),
            Radius = 1,
            Material = new MixedMaterial
            {
                Materials = new List<MixedMaterialMaterial>
                {
                    new MixedMaterialMaterial
                    {
                        Effect = 0.90f,
                        Material = new BrdfMaterial
                        {
                            Scene = this
                        }
                    },
                    new MixedMaterialMaterial
                    {
                        Effect = 0.10f,
                        Material = new FullBright
                        {
                            Color = Color.Yellow,
                        }
                    }
                }
            }
        };

        var topSphere = new Sphere
        {
            Center = new Vector3(2, 1, -7),
            Radius = 1,
            Material = new BrdfMaterial
            {
                Scene = this
            }
        };

        var leftSphere = new Sphere
        {
            Center = new Vector3(-2, 0, -7),
            Radius = 2,
            Material = new MixedMaterial
            {
                Materials = new List<MixedMaterialMaterial>
                {
                    new MixedMaterialMaterial
                    {
                        Effect = 0.25f,
                        Material = new FullSpecular {
                            Scene = this
                        }
                    },
                    new MixedMaterialMaterial
                    {
                        Effect = 0.75f,
                        Material = new HitNormalMaterial()
                    }
                }
            }
        };

        var floorPlane = new Plane
        {
            Position = new Vector3(0, -5f, 0),
            Normal = new Vector3(0, -1, 0),
            Material = new FullBright { Color = Color.FromArgb(100, 100, 100) }
        };

        var roofPlane = new Plane
        {
            Position = new Vector3(0, 5, 0),
            Normal = new Vector3(0, 1, 0),
            Material = new FullBright { Color = Color.FromArgb(100, 100, 100) }
        };

        var backPlane = new Plane
        {
            Position = new Vector3(0, 0, -10),
            Normal = new Vector3(0, 0, -1),
            Material = new FullBright { Color = Color.FromArgb(50, 50, 50) }
        };

        var behindCameraPlane = new Plane
        {
            Position = new Vector3(0, 0, 10),
            Normal = new Vector3(0, 0, 1),
            Material = new FullBright { Color = Color.FromArgb(0, 0, 0) }
        };

        var leftPlane = new Plane
        {
            Position = new Vector3(-5, 0, 0),
            Normal = new Vector3(-1, 0, 0),
            Material = new FullBright { Color = Color.FromArgb(255, 0, 0) }
        };

        var rightPlane = new Plane
        {
            Position = new Vector3(5, 0, 0),
            Normal = new Vector3(1, 0, 0),
            Material = new FullBright { Color = Color.FromArgb(0, 0, 255) }
        };

        this.RenderObjects = new List<IRenderObject>()
        {
            bottomSphere,
            middleSphere,
            topSphere,
            leftSphere,
            floorPlane,
            roofPlane,
            backPlane,
            behindCameraPlane,
            leftPlane,
            rightPlane
        };
    }
}