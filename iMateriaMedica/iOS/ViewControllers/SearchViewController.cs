using Foundation;
using System;
using UIKit;

using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace iMateriaMedica.iOS
{
    class SearchRemedyDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL_REMEDY");

        public SearchRemedyDataSource(DBMgr dbMgr)
        {

        }

        public override nint RowsInSection(UITableView tableview, nint section) => DBMgr.Instance.FilteredRemedies.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }
            // NavigationController.PopToRootViewController(true);
            var item = DBMgr.Instance.FilteredRemedies[indexPath.Row];
            cell.TextLabel.Text = item.Name;
            //cell.DetailTextLabel.Text = item.Name;
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;
            return cell;
        }
    }
    public partial class SearchViewController : UITableViewController
    {
        public void performSearch(string searchText)
        {
            Utils.searchString = searchText;

            IEnumerable<Remedy> query =
                (from item in DBMgr.Instance.RemedyItems
                 where item.Desciption.ToUpper().Contains(searchText.ToUpper())
                 || item.SubHeading.ToUpper().Contains(searchText.ToUpper())
                 || item.Mind.ToUpper().Contains(searchText.ToUpper())
                 || item.Head.ToUpper().Contains(searchText.ToUpper())
                 || item.Face.ToUpper().Contains(searchText.ToUpper())
                 || item.Eyes.ToUpper().Contains(searchText.ToUpper())
                 || item.Ears.ToUpper().Contains(searchText.ToUpper())
                 || item.Nose.ToUpper().Contains(searchText.ToUpper())
#if STDVERSION
                 || item.Mouth.ToUpper().Contains(searchText.ToUpper())
                 || item.Heart.ToUpper().Contains(searchText.ToUpper())
                 || item.Chest.ToUpper().Contains(searchText.ToUpper())
                 || item.Stomach.ToUpper().Contains(searchText.ToUpper())
                 || item.Abdomen.ToUpper().Contains(searchText.ToUpper())
                 || item.Rectum.ToUpper().Contains(searchText.ToUpper())
                 || item.Respiratory.ToUpper().Contains(searchText.ToUpper())
                 || item.Extremities.ToUpper().Contains(searchText.ToUpper())
                 || item.Skin.ToUpper().Contains(searchText.ToUpper())
                 || item.Male.ToUpper().Contains(searchText.ToUpper())
                 || item.Female.ToUpper().Contains(searchText.ToUpper())
                 || item.Fever.ToUpper().Contains(searchText.ToUpper())
                 || item.Back.ToUpper().Contains(searchText.ToUpper())
                 || item.Sleep.ToUpper().Contains(searchText.ToUpper())
                 || item.Modalities.ToUpper().Contains(searchText.ToUpper())
                 || item.Rgeneral.ToUpper().Contains(searchText.ToUpper())
                 || item.Rantidote.ToUpper().Contains(searchText.ToUpper())
                 || item.Rinimical.ToUpper().Contains(searchText.ToUpper())
                 || item.Rcompliment.ToUpper().Contains(searchText.ToUpper())
                 || item.Rcompare.ToUpper().Contains(searchText.ToUpper())
                 || item.Rcompatible.ToUpper().Contains(searchText.ToUpper())
                 || item.Rincompatible.ToUpper().Contains(searchText.ToUpper())
                 || item.Dose.ToUpper().Contains(searchText.ToUpper())
                 || item.Throat.ToUpper().Contains(searchText.ToUpper())
                 || item.Uses.ToUpper().Contains(searchText.ToUpper())
                 || item.Stool.ToUpper().Contains(searchText.ToUpper())
                 || item.Tissues.ToUpper().Contains(searchText.ToUpper())
                 || item.Nerves.ToUpper().Contains(searchText.ToUpper())
                 || item.Bones.ToUpper().Contains(searchText.ToUpper())
                 || item.Tongue.ToUpper().Contains(searchText.ToUpper())
                 || item.Circulatory.ToUpper().Contains(searchText.ToUpper())
                 || item.Blood.ToUpper().Contains(searchText.ToUpper())
                 || item.Spine.ToUpper().Contains(searchText.ToUpper())
                 || item.Teeth.ToUpper().Contains(searchText.ToUpper())
                 || item.Breast.ToUpper().Contains(searchText.ToUpper())
                 || item.Kidney.ToUpper().Contains(searchText.ToUpper())
                 || item.Gastro.ToUpper().Contains(searchText.ToUpper())
                 || item.Spleen.ToUpper().Contains(searchText.ToUpper())
                 || item.Neck.ToUpper().Contains(searchText.ToUpper())
                 || item.Urine.ToUpper().Contains(searchText.ToUpper())
                 || item.Urinary.ToUpper().Contains(searchText.ToUpper())
                 || item.PhysiologicDosage.ToUpper().Contains(searchText.ToUpper())
                 || item.AlimentaryCanal.ToUpper().Contains(searchText.ToUpper())
                 || item.Liver.ToUpper().Contains(searchText.ToUpper())
                 || item.Sexual.ToUpper().Contains(searchText.ToUpper())

#endif
                 select item);

            DBMgr.Instance.FilteredRemedies = query.ToList();

           

            /*
             *             ///
            string keywords = "Rickets Pupils";
            var keys = keywords.Split(' ');   

  
            

            IEnumerable<Remedy> query3 =
   (from item1 in DBMgr.Instance.RemedyItems
                where keys.Any(k => item1.Desciption.Contains(k))
                //|| keys.Any(k => item1.Mind.Contains(k))
                select item1);
          
            foreach (string str in keys)  
        {  
            Console.WriteLine(str);  
        }  
            DBMgr.Instance.FilteredRemedies = query3.ToList();
*/


            /*IEnumerable<Remedy> query3 =
               (from item1 in DBMgr.Instance.RemedyItems
                where Regex.IsMatch(item1.Desciption, @"\b(Pupils|Rickets|550)\b")
                select item1);*/
              



            /*searchText = "Pupils";
            IEnumerable<Remedy> query2 =
              (from item in DBMgr.Instance.RemedyItems
               where item.Desciption.ToUpper().Contains(searchText.ToUpper())
                select item);

            IEnumerable<Remedy> query3 = query.Intersect(query2); 

            DBMgr.Instance.FilteredRemedies = query3.ToList();*/

            
            /*searchText = "Rickets";
            IEnumerable<Remedy> query2 =
     (from item in DBMgr.Instance.RemedyItems
      where item.Desciption.ToUpper().Contains(searchText.ToUpper())
      || item.SubHeading.ToUpper().Contains(searchText.ToUpper())
      || item.Mind.ToUpper().Contains(searchText.ToUpper())
      || item.Head.ToUpper().Contains(searchText.ToUpper())
      || item.Face.ToUpper().Contains(searchText.ToUpper())
      || item.Eyes.ToUpper().Contains(searchText.ToUpper())
      || item.Ears.ToUpper().Contains(searchText.ToUpper())
      || item.Nose.ToUpper().Contains(searchText.ToUpper())
#if STDVERSION
                 || item.Mouth.ToUpper().Contains(searchText.ToUpper())
      || item.Heart.ToUpper().Contains(searchText.ToUpper())
      || item.Chest.ToUpper().Contains(searchText.ToUpper())
      || item.Stomach.ToUpper().Contains(searchText.ToUpper())
      || item.Abdomen.ToUpper().Contains(searchText.ToUpper())
      || item.Rectum.ToUpper().Contains(searchText.ToUpper())
      || item.Respiratory.ToUpper().Contains(searchText.ToUpper())
      || item.Extremities.ToUpper().Contains(searchText.ToUpper())
      || item.Skin.ToUpper().Contains(searchText.ToUpper())
#endif
                 select item);

            IEnumerable<Remedy> query3 =  query.Intersect(query2);*/

           

            //ItemsDataSource sample = (ItemsDataSource)TableView.Source;
            //sample.SetItemsDataSource(FilteredItems);

            TableView.ReloadData();
        }
        public SearchViewController (IntPtr handle) : base (handle)
        {
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "RemedyDetailsSegue")
            {
                RemedyDetailedViewController controller = segue.DestinationViewController as RemedyDetailedViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                controller.bSearch = true;
                controller.nCurrentItemIndex = indexPath.Row;
                TableView.DeselectRow(indexPath, true);
            }

        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();


            TableView.Source = new SearchRemedyDataSource(DBMgr.Instance);

            View.BackgroundColor = Utils.getThemeColor();
            this.Title = "Search";

            TableView.ReloadData();

            mySearchBar.BecomeFirstResponder();

            mySearchBar.SearchButtonClicked += (sender, e) =>
            {
                mySearchBar.ResignFirstResponder();
                string text = mySearchBar.Text.Trim();
                performSearch(text);

            };
            mySearchBar.TextChanged += (sender, e) =>
            {
                string text = e.SearchText;
                performSearch(text);
            };
        }
    }
}