using Foundation;
using System;
using UIKit;

using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

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
                 select item);

            DBMgr.Instance.FilteredRemedies = query.ToList();

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