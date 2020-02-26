/*******************************************************************************************************************************/
// Project: Snappy
// Filename: SnappyRun.cs
// Description: This the main class for screen captures. 
// JT, 02/25/2020
/*******************************************************************************************************************************/

namespace Snappy
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Timers;

    internal class SnappyRun
    {

        public static System.Timers.Timer frequencyTimer;

        public static string FileName
        {
            get
            {
                return string.Format("{0}{1}snap-{2:yyMMdd_hhmmss_tt}.jpg",
                Settings.path,
                System.IO.Path.DirectorySeparatorChar, 
                DateTime.Now);
            }
        }

        public static void DoScreenCapture(object source, ElapsedEventArgs e)
        {
            // Determine the size of the "virtual screen", including all monitors.
            int screenLeft   =  SystemInformation.VirtualScreen.Left;
            int screenTop    =  SystemInformation.VirtualScreen.Top;
            int screenWidth  =  SystemInformation.VirtualScreen.Width;
            int screenHeight =  SystemInformation.VirtualScreen.Height;

            // Create a bitmap of the appropriate size to receive the screenshot.
            using (Bitmap bmp = new Bitmap(screenWidth, screenHeight))
            {
                // Draw the screenshot into our bitmap.
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(screenLeft, screenTop, 0, 0, bmp.Size);
                }

                // Stuff the bitmap into a file
                bmp.Save(FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        public static void StartSnappy()
        {

            DateTime startTime = DateTime.Now;
            int runtimeFrequency = Int32.Parse(Settings.frequency);

            frequencyTimer = new System.Timers.Timer(1000 * runtimeFrequency);
            frequencyTimer.Elapsed += new ElapsedEventHandler(DoScreenCapture);
            frequencyTimer.AutoReset = true;
            frequencyTimer.Enabled = true;
        }
    }
}
