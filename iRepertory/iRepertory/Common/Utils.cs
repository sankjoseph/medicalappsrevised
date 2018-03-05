using System;
using UIKit;
namespace iRepertory
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
        public static void ShowAlertOK(string message, UIViewController controller)
        {
            UIAlertController alert = UIAlertController.Create("Repertory", message, UIAlertControllerStyle.Alert);

            // Configure the alert
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (action) => { }));

            // Display the alert
            controller.PresentViewController(alert, true, null);

        }
    }
}
