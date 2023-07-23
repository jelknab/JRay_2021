using System;
using System.IO;
using System.Numerics;
using JRay_2021;
using JRay_2021.testScenes;

var image = new Image(1024, 1024, 1);
var scene = new Scene1
{
    Camera = new Camera(20, 0, 0)
    {
        Position = new Vector3 { X = 0, Y = 0, Z = 0 },
        Direction = Vector3.UnitZ,
        FieldOfView = 90
    }
};

scene.Render(image);

var pictureDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
    "JRay"
);

Directory.CreateDirectory(pictureDirectory);

image.SaveImage(Path.Combine(pictureDirectory, $"{DateTime.Now:yyyyMMdd hhmmss}"), SkiaSharp.SKEncodedImageFormat.Jpeg);
