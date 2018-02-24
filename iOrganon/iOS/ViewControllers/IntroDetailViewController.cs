using Foundation;
using System;
using UIKit;
using System.IO;
using System.Text.RegularExpressions;
namespace iOrganon.iOS
{
    public partial class IntroDetailViewController : UIViewController
    {
        public  bool bSearch;
        public  bool bIntroduction;

        public int nCurrentItemIndex;

        public IntroDetailViewController (IntPtr handle) : base (handle)
        {
            nCurrentItemIndex = 0;
        }
        private void OnSwipeDetectedLeft()
        {
            if (bSearch)
            {
                return;
            }
            int nMax = (bIntroduction ? 150 : 293);
            if (nCurrentItemIndex >= nMax)// MAX
            {
                return;
            }
            nCurrentItemIndex++;

            PopulateNewDetails();

        }
        private void OnSwipeDetectedRight()
        {
            if (bSearch)
            {
                return;
            }
            if (nCurrentItemIndex <= 0)
            {
                return;
            }
            nCurrentItemIndex--;
  
            PopulateNewDetails();
        }
        private void PopulateNewDetails()
        {

            string fileName = "Message.htm"; // remember case-sensitive
            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);

            var text = File.ReadAllText(localHtmlUrl);

            if (bIntroduction)
            {
                string str = Regex.Replace(text, "<READING>", DBMgr.Instance.IntroItems[nCurrentItemIndex].Para);
                myWebView.LoadHtmlString(str, null);
                //myWebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
                myWebView.ScalesPageToFit = false;
                this.Title = DBMgr.Instance.IntroItems[nCurrentItemIndex].Id;

            }
            else
            {
                string str = Regex.Replace(text, "<READING>", DBMgr.Instance.ChapterItems[nCurrentItemIndex].Reading);
                myWebView.LoadHtmlString(str, null);
                //myWebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
                myWebView.ScalesPageToFit = false;
                this.Title = DBMgr.Instance.ChapterItems[nCurrentItemIndex].Id;
            }

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();



            /////
            UISwipeGestureRecognizer recognizerLeft = new UISwipeGestureRecognizer(OnSwipeDetectedLeft);
            recognizerLeft.Direction = UISwipeGestureRecognizerDirection.Left;
            View.AddGestureRecognizer(recognizerLeft);

            UISwipeGestureRecognizer recognizerRight = new UISwipeGestureRecognizer(OnSwipeDetectedRight);
            recognizerRight.Direction = UISwipeGestureRecognizerDirection.Right;
            View.AddGestureRecognizer(recognizerRight);

            //////
            //txtViewIntroDetails.Text = Utils.itemIntroSelected.Para;
            myWebView.BackgroundColor = Utils.getThemeColor();


            string fileName = "Message.htm"; // remember case-sensitive             string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);

            var text = File.ReadAllText(localHtmlUrl);

            string str = Regex.Replace(text, "<READING>", Utils.itemReadingSelected);

            if (bSearch && Utils.searchString !=null && Utils.searchString.Length >0)
            {
                string searchStringlocal = "<font size = 3 face = Verdana color = red>";
                searchStringlocal = String.Concat(searchStringlocal, Utils.searchString);

                searchStringlocal = String.Concat(searchStringlocal, "</font>");

                str = Regex.Replace(str, Utils.searchString, searchStringlocal,RegexOptions.IgnoreCase);
            }


            myWebView.LoadHtmlString(str,null);             //myWebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));             myWebView.ScalesPageToFit = false; 

            View.BackgroundColor = Utils.getThemeColor();
        }
    }
}