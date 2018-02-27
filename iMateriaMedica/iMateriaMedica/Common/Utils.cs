
using System;
using UIKit;


namespace iMateriaMedica
{
    static class Utils
    {
        public static string searchString;
        public static UIColor getThemeColor()
        {
            return UIColor.FromRGB(226, 215, 188);
        }
        public static bool Empty(string inputString)
        {
            
            if (inputString != null && inputString.Length > 0)
            {
                return false;
            }
            return true;
        }
    }
}
