using System.Globalization;
using System.Runtime.Versioning;

namespace MKVToMP4Converter
{
    [SupportedOSPlatform("windows")]
    public static class UpdateFileMetadata
    {
        public static void Update(string directory, VideoInfo videoInfo)
        {
            string outputFilePath = Path.Combine(directory, videoInfo.OutputFile);
            var tFile = TagLib.File.Create(outputFilePath);

            // Confirm that video duration is correct.
            var expectedDuration = TimeSpan.ParseExact(videoInfo.Duration, @"h\:m\:s", CultureInfo.InvariantCulture);
            var actualDuration = TimeSpan.FromSeconds((int)(tFile.Properties.Duration.TotalSeconds));
            double difference = Math.Abs(expectedDuration.Subtract(actualDuration).TotalSeconds);
            if (difference > 1)
            {
                string message = "Video duration is not correct.";
                Program.LogWarning(message);
            }

            // Set year.
            tFile.Tag.Year = uint.Parse(videoInfo.Year);
            tFile.Save();

            // Set created and modified dates.
            // (Not using 1/1 because conversion to local time could move the year back one year.)
            var releaseDate = DateTime.SpecifyKind(new DateTime(int.Parse(videoInfo.Year), 1, 2, 0, 0, 0), DateTimeKind.Utc);
            File.SetCreationTimeUtc(outputFilePath, releaseDate);
            File.SetLastWriteTimeUtc(outputFilePath, releaseDate);
        }
    }
}
