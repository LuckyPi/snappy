/*******************************************************************************************************************************/
// Project: snappy
// Filename: Settings.cs
// Description: This class is used for various settings that can be accessed throughout the program. Like a global without the globe
// JT, 02/25/2020
/*******************************************************************************************************************************/

namespace Snappy
{
    using System;

    internal class Settings
    {
        // Additional Special Folders Environment.SpecialFolder names can be found here https://docs.microsoft.com/en-us/windows/desktop/shell/knownfolderid
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        // sleep time to keep the application running
        public static string frequency = null;
        
        // sleep time to keep the application running
        public static string duration = null;
    }
}

