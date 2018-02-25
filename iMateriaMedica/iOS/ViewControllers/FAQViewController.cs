using Foundation;
using System;
using UIKit;
using System.IO;
namespace iMateriaMedica.iOS
{
    public partial class FAQViewController : UIViewController
    {
        public FAQViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.Title = "FAQ";
            string fileName = "FAQ.htm"; // remember case-sensitive
            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);
            myWebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
            myWebView.ScalesPageToFit = false;
        }
    }
}