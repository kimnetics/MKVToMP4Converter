namespace MKVToMP4Converter
{
    public static class ConvertVideo
    {
        public static void Convert(string directory, VideoInfo videoInfo)
        {
            // Start VideoProc.
            var startVideoProc = new Thread(new ThreadStart(StartVideoProc));
            startVideoProc.Start();
            Thread.Sleep(2000);

            // Select Video.
            SendMouseAndKeyboard.MouseSetCursorPos(1050, 650);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);

            // Select Plus Video.
            SendMouseAndKeyboard.MouseSetCursorPos(910, 400);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);

            // Enter MKV file name.
            SendMouseAndKeyboard.KeyboardSendCharacters(Path.Combine(directory, videoInfo.MKVFile));
            SendMouseAndKeyboard.KeyboardSendCR();
            Thread.Sleep(3000);

            // Select Option.
            SendMouseAndKeyboard.MouseSetCursorPos(1460, 500);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);

            // Select Name & Tag.
            SendMouseAndKeyboard.MouseSetCursorPos(1410, 350);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);

            // Select Add Artwork.
            SendMouseAndKeyboard.MouseSetCursorPos(1000, 800);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);

            // Enter cover file name.
            SendMouseAndKeyboard.KeyboardSendCharacters(Path.Combine(directory, videoInfo.CoverFile));
            SendMouseAndKeyboard.KeyboardSendCR();
            Thread.Sleep(3000);

            // Select Output Name.
            SendMouseAndKeyboard.MouseSetCursorPos(1310, 490);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(100);

            // Enter output file name.
            SendMouseAndKeyboard.KeyboardSendControlA();
            SendMouseAndKeyboard.KeyboardSendCharacters(videoInfo.OutputFile);
            Thread.Sleep(100);

            // Select Title.
            SendMouseAndKeyboard.MouseSetCursorPos(1310, 540);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(100);

            // Enter title.
            SendMouseAndKeyboard.KeyboardSendControlA();
            SendMouseAndKeyboard.KeyboardSendCharacters(videoInfo.Title);
            Thread.Sleep(100);

            // Select Artist.
            SendMouseAndKeyboard.MouseSetCursorPos(1310, 590);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(100);

            // Clear artist.
            SendMouseAndKeyboard.KeyboardSendControlA();
            SendMouseAndKeyboard.KeyboardSendBackspace();
            Thread.Sleep(100);

            // Select Comment.
            SendMouseAndKeyboard.MouseSetCursorPos(1310, 700);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(100);

            // Enter rating.
            SendMouseAndKeyboard.KeyboardSendControlA();
            SendMouseAndKeyboard.KeyboardSendCharacters(videoInfo.Rating);
            Thread.Sleep(100);

            // Select Done.
            SendMouseAndKeyboard.MouseSetCursorPos(1620, 1040);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);

            // Select Run.
            SendMouseAndKeyboard.MouseSetCursorPos(1760, 980);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(3000);

            // Wait for conversion to complete.
            GPUInfo.WaitForGPULoadToLighten();

            // Select Close form.
            SendMouseAndKeyboard.MouseSetCursorPos(1800, 350);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);

            // Select Close form.
            SendMouseAndKeyboard.MouseSetCursorPos(1650, 440);
            Thread.Sleep(100);
            SendMouseAndKeyboard.MouseClick();
            Thread.Sleep(1000);
        }

        private static void StartVideoProc()
        {
            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo.FileName = @"C:\Program Files (x86)\Digiarty\VideoProc Converter\VideoProcConverter.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
