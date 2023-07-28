using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JRay_2021
{
    public struct PerformanceMetrics
    {
        public int TotalSamples { get; set; }

        public long AverageRunningTimeMS { get; set; }
    }

    public class PerformanceTesting
    {
        public static async Task<PerformanceMetrics> TestPerformanceAsync(int totalSamples, Func<Task> executionFunction)
        {
            Stopwatch sw = new();

            sw.Start();
            for (int i = 0; i < totalSamples; i++)
            {
                await executionFunction();
            }
            sw.Stop();

            return new PerformanceMetrics { TotalSamples = totalSamples, AverageRunningTimeMS = sw.ElapsedMilliseconds / totalSamples };
        }
    }
}
