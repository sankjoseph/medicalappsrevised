using Foundation;
using System;
using UIKit;

namespace iMateriaMedica.iOS
{
    public partial class LaunchScreenController : UIViewController
    {
        public LaunchScreenController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
#if STDVERSION
            myLabel.Hidden = true;
#endif

        }
    }
}