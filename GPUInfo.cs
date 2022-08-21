using System.Diagnostics;
using System.Runtime.Versioning;

namespace MKVToMP4Converter
{
    [SupportedOSPlatform("windows")]
    public static class GPUInfo
    {
        public static void WaitForGPULoadToLighten()
        {
            List<PerformanceCounter> gpuCounters;
            float gpuUsage;

            do
            {
                gpuCounters = GetGPUCounters();
                gpuUsage = GetGPUUsage(gpuCounters);
                Thread.Sleep(30000);
            } while (gpuUsage > 30);

            Thread.Sleep(20000);
        }

        private static List<PerformanceCounter> GetGPUCounters()
        {
            var category = new PerformanceCounterCategory("GPU Engine");
            string[] counterNames = category.GetInstanceNames();

            List<PerformanceCounter> gpuCounters = counterNames
                .Where(counterName => counterName.EndsWith("engtype_3D"))
                .SelectMany(counterName => category.GetCounters(counterName))
                .Where(counter => counter.CounterName.Equals("Utilization Percentage"))
                .ToList();

            return gpuCounters;
        }

        private static float GetGPUUsage(List<PerformanceCounter> gpuCounters)
        {
            try
            {
                gpuCounters.ForEach(x => x.NextValue());

                Thread.Sleep(1000);

                float gpuUsage = gpuCounters.Sum(x => x.NextValue());

                return gpuUsage;
            }
            catch
            {
                return 0;
            }
        }

    }
}
