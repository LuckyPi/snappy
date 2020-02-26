/*******************************************************************************************************************************/
// Project: snappy 
// Filename: Program.cs 
// Description: Automated screen captures  
// JT, 02/25/2020
/*******************************************************************************************************************************/

namespace Snappy
{
    using System;
    using System.Timers;
    using System.IO;
    using System.Linq;
    using System.Threading;

    class Program
    {

        static void Main(string[] args)
        {

            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Snappy");
                    Console.WriteLine("       ___           ");
                    Console.WriteLine("     [|   |=|{)_     ");
                    Console.WriteLine("      |___| \\/  )   ");
                    Console.WriteLine("       /|\\     /|   ");
                    Console.WriteLine("      / | \\    | \\ ");
                    Console.WriteLine();
                    Console.WriteLine("[*] the automagical screen capture utility");
                    Console.WriteLine();
                    Console.WriteLine("Usage: snappy.exe -f 10 -d 30");
                    Console.WriteLine();
                    Console.WriteLine("Flags:");
                    Console.WriteLine(" -f sets screen capture frequency in seconds");
                    Console.WriteLine(" -d sets screen capture duration in minutes");
                    //Console.WriteLine(" not implemented - -p   // file output path e.g. c:\\Users\\Public\\Pictures\\ - default is ");
                    Console.WriteLine();
                }
                else
                {
                    // Parses CLI mode/flag arguments using UtilityArguments class. 
                    UtilityArguments arguments = new UtilityArguments(args);

                    try
                    {
                        Settings.frequency = arguments.frequency;
                    }   
                    catch
                    {
                        Console.WriteLine(" how often should I take a screen capture in seconds: e.g. 30 sec = -f 30");
                    }
                    try
                    {
                        Settings.duration = arguments.duration;
                    }
                    catch
                    {
                        Console.WriteLine(" how long should I run in mins: e.g. 5 mins = -d 5");
                    }
                    try
                    {
                        //Settings.path = arguments.path;
                    }
                    catch
                    {
                        Console.WriteLine(" where should I write the screen capture: e.g. -p c:\\users\\public\\pictures");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Snappy");
                    Console.WriteLine("       ___           ");
                    Console.WriteLine("     [|   |=|{)_     ");
                    Console.WriteLine("      |___| \\/  )   ");
                    Console.WriteLine("       /|\\     /|   ");
                    Console.WriteLine("      / | \\    | \\ ");
                    Console.WriteLine();
                    Console.WriteLine("[*] the automagical screen capture utility");
                    Console.WriteLine();

                    // start
                    SnappyRun.StartSnappy();

                    // block until duration value had passed - ugly but it works and low CPU
                    int runDuration = Int32.Parse(Settings.duration);
                    Thread.Sleep(1000 * 60 * runDuration);                                                 
                }
            }

            // No or wrong arguments so we print help for the user.
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }    
            
        }

    }

}

