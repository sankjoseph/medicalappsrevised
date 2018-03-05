using Foundation;
using System;
using UIKit;

using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace iRepertory.iOS
{
    class RepertoryDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL_REPERTORY");


        public RepertoryDataSource(DBMgrKent dbMgr)
        {

        }

        public override nint RowsInSection(UITableView tableview, nint section) => DBMgrKent.Instance.FilteredRepItems.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }
            // NavigationController.PopToRootViewController(true);
            var item = DBMgrKent.Instance.FilteredRepItems[indexPath.Row];
            cell.TextLabel.Text = item.SubSection;
            //cell.DetailTextLabel.Text = item.Name;
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;
            return cell;
        }
 
    }
    public partial class IndexViewController : UITableViewController
    {
        public IndexViewController (IntPtr handle) : base (handle)
        {
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "RepDetailsSegue")
            {
                RepInfoViewController controller = segue.DestinationViewController as RepInfoViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                controller.bSearch = true;
                controller.bFromSectionFilter = false;
                controller.nCurrentItemIndex = indexPath.Row;
                TableView.DeselectRow(indexPath, true);
            }

        }
        public void performSearch(string searchText)
        {
            Utils.searchString = searchText;

            var keys = searchText.Split(' ');




            IEnumerable<RepertoryItem> query =
                (from item1 in DBMgrKent.Instance.RepItems
                 where keys.All(k => item1.SubSection.ToUpper().Contains(k.ToUpper()))
  
                select item1);


            /*IEnumerable<RepertoryItem> query =
                //(from item in DBMgrKent.Instance.RepItems
                 //where item.SubSection.ToUpper().Contains(searchText.ToUpper())

                 //select item);*/

            DBMgrKent.Instance.FilteredRepItems = query.ToList();
            TableView.ReloadData();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.Source = new RepertoryDataSource(DBMgrKent.Instance);
            Title = "Search";
            View.BackgroundColor = Utils.getThemeColor();
 
            mySearchBar.BecomeFirstResponder();
            mySearchBar.SearchButtonClicked += (sender, e) =>
            {
                mySearchBar.ResignFirstResponder();
                string text = mySearchBar.Text.Trim();
                performSearch(text);

            };
            mySearchBar.TextChanged += (sender, e) =>
            {
                string text = e.SearchText.Trim();
                performSearch(text);
            };
        }
    }
}