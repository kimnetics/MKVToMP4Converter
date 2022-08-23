﻿using System.Diagnostics;
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
                Thread.Sleep(10000);
                gpuCounters = GetGPUCounters();
                float gpuUsage1 = GetGPUUsage(gpuCounters);

                Thread.Sleep(10000);
                gpuCounters = GetGPUCounters();
                float gpuUsage2 = GetGPUUsage(gpuCounters);

                Thread.Sleep(10000);
                gpuCounters = GetGPUCounters();
                float gpuUsage3 = GetGPUUsage(gpuCounters);

                gpuUsage = (gpuUsage1 + gpuUsage2 + gpuUsage3) / 3;
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
