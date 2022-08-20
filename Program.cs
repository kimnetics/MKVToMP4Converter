using System.Text.Json;

namespace MKVToMP4Converter
{
    public class Program
    {
        static void Main(string[] args)
        {
            string videoDirectory = @"C:\Video\Alien";
            string outputDirectory = @"C:\Users\gkim\Videos\VideoProc";

            VideoInfo videoInfo = ReadVideoInfoFile(videoDirectory);

            ConvertVideo.Convert(videoDirectory, videoInfo);

            UpdateFileMetadata.Update(outputDirectory, videoInfo);
        }

        private static VideoInfo ReadVideoInfoFile(string directory)
        {
            var videoInfo = new VideoInfo();

            var fileName = Path.Combine(directory, @"VideoInfo.txt");
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
            Console.WriteLine($"Error - {message}");
        }
    }
}
