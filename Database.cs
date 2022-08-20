using System.Data.OleDb;
using System.Runtime.Versioning;

namespace MKVToMP4Converter
{
    [SupportedOSPlatform("windows")]
    public static class Database
    {
        private static string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\gkim\\Documents\\MKVToMP4Converter.accdb";

        public static void AddVideoInfo(VideoInfo videoInfo, string conversionVersionCode, DateTime startedDateTime, DateTime finishedDateTime)
        {
            using (var connection = new OleDbConnection(connectionString))
            {
                string insertSQL =
                    $@"INSERT INTO tblVideoInfo
(Title, MKVFile, CoverFile, OutputFile, [Year], Rating, Duration, QualityCode, ConversionVersionCode, StartedDateTime, FinishedDateTime)
VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                var command = new OleDbCommand(insertSQL, connection);
                command.Parameters.AddWithValue("@title", videoInfo.Title);
                command.Parameters.AddWithValue("@mkvFile", videoInfo.MKVFile);
                command.Parameters.AddWithValue("@coverFile", videoInfo.CoverFile);
                command.Parameters.AddWithValue("@outputFile", videoInfo.OutputFile);
                command.Parameters.AddWithValue("@year", videoInfo.Year);
                command.Parameters.AddWithValue("@rating", videoInfo.Rating);
                command.Parameters.AddWithValue("@duration", videoInfo.Duration);
                command.Parameters.AddWithValue("@quality", videoInfo.Quality);
                command.Parameters.AddWithValue("@conversionVersionCode", conversionVersionCode);
                command.Parameters.AddWithValue("@startedDateTime", startedDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                command.Parameters.AddWithValue("@finishedDateTime", finishedDateTime.ToString("yyyy-MM-dd HH:mm:ss"));

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Program.LogError(ex.Message);
                    throw;
                }
            }
        }
    }
}
