using System.Threading;

namespace MKVToMP4Converter
{
    internal class Program
    {
        static void Main(string[] args)
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

            // Enter file name.
            SendMouseAndKeyboard.KeyboardSendCharacters(@"C:\Video\Alien\Alien_t01 - Director's Cut.mkv");
            SendMouseAndKeyboard.KeyboardSendCR();
            Thread.Sleep(2000);
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
