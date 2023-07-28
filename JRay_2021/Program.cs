using JRay_2021;
using JRay_2021.testScenes;
using System;
using System.IO;
using System.Numerics;

var image = new Image(128, 128, 1);
var scene = new Scene1
{
    Camera = new Camera(20, 0, 0)
    {
        Position = new Vector3 { X = 0, Y = 0, Z = 0 },
        Direction = Vector3.UnitZ,
        FieldOfView = 90
    }
};

var performance = await PerformanceTesting.TestPerformanceAsync(10, () => scene.Render(image));

Console.WriteLine($"Render finished, avg execution time {performance.AverageRunningTimeMS} ms");

var pictureDirectory = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
    "JRay"
);

Directory.CreateDirectory(pictureDirectory);

image.SaveImage(Path.Combine(pictureDirectory, $"{DateTime.Now:yyyyMMdd hhmmss}"), SkiaSharp.SKEncodedImageFormat.Jpeg);
