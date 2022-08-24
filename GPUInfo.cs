using System.Diagnostics;
using System.Runtime.Versioning;

namespace MKVToMP4Converter
{
    [SupportedOSPlatform("windows")]
    public static class GPUInfo
    {
        public static void WaitForGPULoadToLighten()
        {
            float gpuUsage;

            do
            {
                Thread.Sleep(10000);
                float gpuUsage1 = GetGPUUsage();

                Thread.Sleep(10000);
                float gpuUsage2 = GetGPUUsage();

                Thread.Sleep(10000);
                float gpuUsage3 = GetGPUUsage();

                gpuUsage = (gpuUsage1 + gpuUsage2 + gpuUsage3) / 3;
            } while (gpuUsage > 30);

            Thread.Sleep(20000);
        }

        private static List<PerformanceCounter> GetGPUCounters()
        {
            var gpuCounters = new List<PerformanceCounter>();

            bool gotCounters = false;
            do
            {
                try
                {
                    var category = new PerformanceCounterCategory("GPU Engine");
                    string[] counterNames = category.GetInstanceNames();

                    gpuCounters = counterNames
                        .Where(counterName => counterName.EndsWith("engtype_3D"))
                        .SelectMany(counterName => category.GetCounters(counterName))
                        .Where(counter => counter.CounterName.Equals("Utilization Percentage"))
                        .ToList();

                    gotCounters = true;
                }
                catch
                {
                    gotCounters = false;
                }
            } while (!gotCounters);

            return gpuCounters;
        }

        private static float GetGPUUsage()
        {
            float gpuUsage = 0;

            bool gotUsage = false;
            do
            {
                List <PerformanceCounter> gpuCounters = GetGPUCounters();

                try
                {
                    gpuCounters.ForEach(x => x.NextValue());

                    Thread.Sleep(1000);

                    gpuUsage = gpuCounters.Sum(x => x.NextValue());

                    gotUsage = true;
                }
                catch
                {
                    gotUsage = false;
                }
            } while (!gotUsage);

            return gpuUsage;
        }

    }
}
