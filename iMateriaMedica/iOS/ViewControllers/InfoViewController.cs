using Foundation;
using System;
using UIKit;

namespace iMateriaMedica.iOS
{
    public partial class InfoViewController : UIViewController
    {
        public InfoViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "Information";
        }
    }
}