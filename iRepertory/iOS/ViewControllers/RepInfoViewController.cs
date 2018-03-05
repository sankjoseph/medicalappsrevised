using Foundation;
using System;
using UIKit;

using System.IO;
using System.Text.RegularExpressions;


namespace iRepertory.iOS
{
    public partial class RepInfoViewController : UIViewController
    {

        public bool bSearch;
        public bool bFromSectionFilter;
        public int nCurrentItemIndex;
        public RepInfoViewController (IntPtr handle) : base (handle)
        {
            nCurrentItemIndex = 0;
            bSearch = false;
            bFromSectionFilter = false;
        }

        private void PopulateNewDetails()
        {
            string fileName = "";
            string title = "";
   
            var item = (bFromSectionFilter) ?DBMgrKent.Instance.FilteredSectionRepItems[nCurrentItemIndex] :DBMgrKent.Instance.FilteredRepItems[nCurrentItemIndex];
 
  
            fileName = item.FileName;
            title = item.Section;

            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);

            myWebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
            myWebView.ScalesPageToFit = false;
            //myWebView.
            Title = title;

        }
        private void OnSwipeDetectedLeft()
        {
            if (!bSearch)
            {
                return;
            }

            int Max = (bFromSectionFilter) ? DBMgrKent.Instance.FilteredSectionRepItems.Count - 1 : DBMgrKent.Instance.FilteredRepItems.Count - 1;
            if (nCurrentItemIndex >= Max)// MAX
            {
                return;
            }

            nCurrentItemIndex++;
  
            PopulateNewDetails();
 
        }
   
        private void OnSwipeDetectedRight()
        {
            if (!bSearch)
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
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            string title = "Remedies";
            if (!Utils.Empty(this.NavigationController.TabBarItem.Title))
            {
                title = this.NavigationController.TabBarItem.Title;
            }
          
            string fileName = "";

            if (bSearch)
            {
                var item = (bFromSectionFilter) ?DBMgrKent.Instance.FilteredSectionRepItems[nCurrentItemIndex] :DBMgrKent.Instance.FilteredRepItems[nCurrentItemIndex];
 
                fileName = item.FileName;
                title = item.Section;
            }
            else
            {
                btnLeft.Enabled = false;
                barbtnRight.Enabled = false;
                if (title.Equals("Introduction"))
                {
                    fileName = "preface.html"; // remember case-sensitive
                }

                if (title.Equals("Use"))
                {
                    fileName = "use.html"; // remember case-sensitive
                }
                if (title.Equals("Remedies"))
                {
                    fileName = "remedies.html"; // remember case-sensitive
                }
            }
            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);

            myWebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
            myWebView.ScalesPageToFit = false;

            //myWebView.
            Title = title;

  
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            myWebView.BackgroundColor = Utils.getThemeColor();

            View.BackgroundColor = Utils.getThemeColor();



            UISwipeGestureRecognizer recognizerLeft = new UISwipeGestureRecognizer(OnSwipeDetectedLeft);
            recognizerLeft.Direction = UISwipeGestureRecognizerDirection.Left;
            View.AddGestureRecognizer(recognizerLeft);

            UISwipeGestureRecognizer recognizerRight = new UISwipeGestureRecognizer(OnSwipeDetectedRight);
            recognizerRight.Direction = UISwipeGestureRecognizerDirection.Right;
            View.AddGestureRecognizer(recognizerRight);

         
            btnLeft.Clicked += (s, e) =>
            {
                if (myWebView.CanGoBack)
                {
                    myWebView.GoBack();
                }
      
            };
            barbtnRight.Clicked += (s, e) => 
            {
                if (myWebView.CanGoForward)
                {
                    myWebView.GoForward();
                }
            };
 
        }
    }
}