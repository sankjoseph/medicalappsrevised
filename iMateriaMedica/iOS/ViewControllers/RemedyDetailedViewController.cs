using Foundation;
using System;
using UIKit;
using System.IO;
using System.Text.RegularExpressions;

namespace iMateriaMedica.iOS
{

    public partial class RemedyDetailedViewController : UIViewController
    {
        int Max = 118;  
        public int nCurrentItemIndex;
        public bool bSearch;
        public RemedyDetailedViewController (IntPtr handle) : base (handle)
        {
            
        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }
        private void OnSwipeDetectedLeft()
        {
            if (bSearch)
            {
                return;
            }

            if (nCurrentItemIndex >= Max)// MAX
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
            ///
            string fileName = "Message.htm"; // remember case-sensitive
            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);


            var textHtml = File.ReadAllText(localHtmlUrl);

            Remedy remedy;
            if (bSearch)
            {
                remedy = DBMgr.Instance.FilteredRemedies[nCurrentItemIndex];
            }
            else
            {
                remedy = DBMgr.Instance.RemedyItems[nCurrentItemIndex];
            }
            string str = Regex.Replace(textHtml, "<NAME>",remedy.Name);
  
            str = Regex.Replace(str, "<SUBH>", remedy.SubHeading);

            str = Regex.Replace(str, "<GENERALDESC>", remedy.Desciption);


            if (bSearch && Utils.searchString != null && Utils.searchString.Length > 0)
            {
                string searchStringlocal = "<font size = 3 face = Verdana color = red>";
                searchStringlocal = String.Concat(searchStringlocal, Utils.searchString);

                searchStringlocal = String.Concat(searchStringlocal, "</font>");

                str = Regex.Replace(str, Utils.searchString, searchStringlocal, RegexOptions.IgnoreCase);
            }


            myWebView.LoadHtmlString(str, null);
            //myWebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
            myWebView.ScalesPageToFit = false;

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            myWebView.BackgroundColor = Utils.getThemeColor();

            View.BackgroundColor = Utils.getThemeColor();
 
            /////
            UISwipeGestureRecognizer recognizerLeft = new UISwipeGestureRecognizer(OnSwipeDetectedLeft);
            recognizerLeft.Direction = UISwipeGestureRecognizerDirection.Left;
            View.AddGestureRecognizer(recognizerLeft);

            UISwipeGestureRecognizer recognizerRight = new UISwipeGestureRecognizer(OnSwipeDetectedRight);
            recognizerRight.Direction = UISwipeGestureRecognizerDirection.Right;
            View.AddGestureRecognizer(recognizerRight);

            PopulateNewDetails();
            //////
            //txtViewIntroDetails.Text = Utils.itemIntroSelected.Para;
        }
    }
}