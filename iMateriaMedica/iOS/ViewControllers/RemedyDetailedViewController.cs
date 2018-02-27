//#define STDVERSION
using Foundation;
using System;
using UIKit;
using System.IO;
using System.Text.RegularExpressions;

namespace iMateriaMedica.iOS
{

    public partial class RemedyDetailedViewController : UIViewController
    {
        int Max = DBMgr.Instance.RemedyItems.Count - 1; //MAX index 0 to count-1 
        public int nCurrentItemIndex;
        public bool bSearch;
        public RemedyDetailedViewController(IntPtr handle) : base(handle)
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
            string str = Regex.Replace(textHtml, "<NAME>", remedy.Name);

            str = Regex.Replace(str, "<SUBH>", remedy.SubHeading);

            str = Regex.Replace(str, "<GENERALDESC>", remedy.Desciption);

            if (!Utils.Empty(remedy.Mind))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Mind </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Mind, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Nerves))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Nerves </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Nerves, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Head))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Head </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Head, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Face))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Face </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Face, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Eyes))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Eyes </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Eyes, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Ears))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Ears </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Ears, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Nose))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Nose </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Nose, "</p> \"", ");");

            }
                      
            if (!Utils.Empty(remedy.Mouth))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Mouth </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Mouth, "</p> \"", ");");

            } 
            if (!Utils.Empty(remedy.Throat))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Throat </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Throat, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Heart))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Heart </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Heart, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Chest))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Chest </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Chest, "</p> \"", ");");

            }

            if (!Utils.Empty(remedy.Stomach))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Stomach </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Stomach, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Liver))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Liver </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Liver, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Respiratory))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Respiratory </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Respiratory, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Kidney))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Kidney </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Kidney, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Abdomen))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Abdomen </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Abdomen, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Rectum))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Rectum </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Rectum, "</p> \"", ");");

            }



            if (!Utils.Empty(remedy.Urine))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Urine </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Urine, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Urinary))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Urinary </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Urinary, "</p> \"", ");");
            }


            if (!Utils.Empty(remedy.Extremities))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Extremities </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Extremities, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Skin))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Skin </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Skin, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Tissues))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Tissues </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Tissues, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Male))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Male </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Male, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Female))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Female </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Female, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Sexual))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Sexual </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Sexual, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Fever))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Fever </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Fever, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Back))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Back </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Back, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Stool))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Stool </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Stool, "</p> \"", ");");
            }
  
            if (!Utils.Empty(remedy.Sleep))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Sleep </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Sleep, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Modalities))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Modalities </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Modalities, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Uses))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Uses </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Uses, "</p> \"", ");");
            }
 
            if (!Utils.Empty(remedy.Rgeneral)||
                !Utils.Empty(remedy.Rantidote)||
                !Utils.Empty(remedy.Rinimical) ||
                !Utils.Empty(remedy.Rcompliment)||
                !Utils.Empty(remedy.Rcompare)||
                !Utils.Empty(remedy.Rcompatible)||
                !Utils.Empty(remedy.Rincompatible))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Relationship </h3> \"", ");");
            }
            /////relationship matters

            if (!Utils.Empty(remedy.Rgeneral))
            {
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Rgeneral, "</p> \"", ");");

            }

            if (!Utils.Empty(remedy.Rantidote))
            {
                 str = String.Concat(str, "document.write(", "\"<p>",
                                     "<font size = 3 face = Verdana color = white> Antidote:- </font>", remedy.Rantidote, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Rinimical))
            {
                str = String.Concat(str, "document.write(", "\"<p>",
                                    "<font size = 3 face = Verdana color = white> Inimical:- </font>",
                                    remedy.Rinimical, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Rcompliment))
            {
                str = String.Concat(str, "document.write(", "\"<p>", 
                                    "<font size = 3 face = Verdana color = white> Compliment:- </font>",
                                    remedy.Rcompliment, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Rcompare))
            {
                str = String.Concat(str, "document.write(", "\"<p>", 
                                    "<font size = 3 face = Verdana color = white> Compare:- </font>",
                                    remedy.Rcompare, "</p> \"", ");");

            }
            if (!Utils.Empty(remedy.Rcompatible))
            {
                str = String.Concat(str, "document.write(", "\"<p>", 
                                    "<font size = 3 face = Verdana color = white> Compatible:- </font>",
                                    remedy.Rcompatible, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Rincompatible))
            {
                str = String.Concat(str, "document.write(", "\"<p>",
                                    "<font size = 3 face = Verdana color = white> Incompatible:- </font>",
                                    remedy.Rincompatible, "</p> \"", ");");
            }


            if (!Utils.Empty(remedy.Bones))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Bones </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Bones, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Tongue))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Tongue </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Tongue, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Circulatory))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Circulatory </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Circulatory, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Blood))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Blood </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Blood, "</p> \"", ");");
            }
            ////

            if (!Utils.Empty(remedy.Spine))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Spine </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Spine, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Bowels))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Bowels </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Bowels, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Teeth))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Teeth </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Teeth, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Breast))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Breast </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Breast, "</p> \"", ");");
            }


            if (!Utils.Empty(remedy.Gastro))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Gastro </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Gastro, "</p> \"", ");");
            }
            if (!Utils.Empty(remedy.Spleen))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Spleen </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Spleen, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Neck))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Neck </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Neck, "</p> \"", ");");
            }


            if (!Utils.Empty(remedy.AlimentaryCanal))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Physiologic Dosage </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.AlimentaryCanal, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.PhysiologicDosage))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Physiologic Dosage </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.PhysiologicDosage, "</p> \"", ");");
            }

            if (!Utils.Empty(remedy.Dose))
            {
                str = String.Concat(str, "document.write(", "\"<h3>", "Dose </h3> \"", ");");
                str = String.Concat(str, "document.write(", "\"<p>", remedy.Dose, "</p> \"", ");");
            }
 

            /*string keywords = "Rickets Pupils";
            var keys = keywords.Split(' ');   

            foreach (string strkey in keys)  
        {  
                string searchStringlocal = "<font size = 3 face = Verdana color = red>";
                searchStringlocal = String.Concat(searchStringlocal, strkey);

                searchStringlocal = String.Concat(searchStringlocal, "</font>");

                str = Regex.Replace(str, strkey, searchStringlocal, RegexOptions.IgnoreCase);
        }  */

            //replace for red color for search
           if (bSearch && !Utils.Empty(Utils.searchString))
            {
                string searchStringlocal = "<font size = 3 face = Verdana color = red>";
                searchStringlocal = String.Concat(searchStringlocal, Utils.searchString);

                searchStringlocal = String.Concat(searchStringlocal, "</font>");

                str = Regex.Replace(str, Utils.searchString, searchStringlocal, RegexOptions.IgnoreCase);
            }


            /// 
            // Append fotter
            str = String.Concat(str, "</script> </body> </html>");

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