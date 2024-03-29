﻿using System.Runtime.Versioning;
using System.Text.Json;

namespace MKVToMP4Converter
{
    [SupportedOSPlatform("windows")]
    public class Program
    {
        static void Main(string[] args)
        {
            LogStartStop("MKVToMP4Converter started");

            string baseDirectory = args[0];
            string outputDirectory = @"C:\Users\gkim\Videos\VideoProc";

            var directoryInfo = new DirectoryInfo(baseDirectory);
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            foreach (DirectoryInfo videoDirectory in directories)
            {
                HandleVideo(videoDirectory.ToString(), outputDirectory);
            }

            LogStartStop("MKVToMP4Converter finished");

            Environment.Exit(0);
        }

        private static void HandleVideo(string videoDirectory, string outputDirectory)
        {
            LogInfo($"Converting \"{videoDirectory}\" started");

            var startedDateTime = DateTime.UtcNow;

            VideoInfo videoInfo = ReadVideoInfoFile(videoDirectory);

            ConvertVideo.Convert(videoDirectory, outputDirectory, videoInfo);

            UpdateFileMetadata.Update(outputDirectory, videoInfo);

            var finishedDateTime = DateTime.UtcNow;

            Database.AddVideoInfo(videoInfo, "V4", startedDateTime, finishedDateTime);

            LogInfo($"Converting \"{videoDirectory}\" finished");
        }

        private static VideoInfo ReadVideoInfoFile(string videoDirectory)
        {
            var videoInfo = new VideoInfo();

            var fileName = Path.Combine(videoDirectory, @"VideoInfo.txt");
            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    string json = reader.ReadToEnd();
                    VideoInfo? deserializedJson = JsonSerializer.Deserialize<VideoInfo>(json);
                    if (deserializedJson != null)
                    {
                        videoInfo = deserializedJson;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Encountered error \"{ex.Message}\" while reading file \"{fileName}\".");
                throw;
            }

            return videoInfo;
        }

        public static void LogError(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Error - {message}");
            Console.ForegroundColor = originalColor;
        }

        public static void LogInfo(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {message}");
            Console.ForegroundColor = originalColor;
        }

        public static void LogStartStop(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {message}");
            Console.ForegroundColor = originalColor;
        }

        public static void LogWarning(string message)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: {message}");
            Console.ForegroundColor = originalColor;
        }
    }
}
