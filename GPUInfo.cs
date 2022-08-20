using System.Diagnostics;

namespace MKVToMP4Converter
{
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
                Thread.Sleep(60000);
            } while (gpuUsage > 50);

            Thread.Sleep(3000);
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
