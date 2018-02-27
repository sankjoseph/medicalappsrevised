using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace iMateriaMedica.iOS
{
    class RemedyDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL_REMEDY");

   
        public RemedyDataSource(DBMgr dbMgr)
        {

        }

        public override nint RowsInSection(UITableView tableview, nint section) => DBMgr.Instance.genIndexedTableItems[DBMgr.Instance.genKeys[section]].Count;
        public override nint NumberOfSections(UITableView tableView) =>  DBMgr.Instance.genKeys.Length;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }
            // NavigationController.PopToRootViewController(true);
            var item = DBMgr.Instance.genIndexedTableItems[DBMgr.Instance.genKeys[indexPath.Section]][indexPath.Row];
            cell.TextLabel.Text = item.Name;
            //cell.DetailTextLabel.Text = item.Name;
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;
            return cell;
        }
        public override String[] SectionIndexTitles(UITableView tableView)
        {
            return DBMgr.Instance.genKeys.ToArray();
        }
    }

    public partial class RemediesViewController : UITableViewController
    {
        public RemediesViewController (IntPtr handle) : base (handle)
        {
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "RemedyDetailsSegue")
            {
                RemedyDetailedViewController controller = segue.DestinationViewController as RemedyDetailedViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                controller.bSearch = false;
                controller.nCurrentItemIndex = indexPath.Row;
                controller.nCurrentIndexPath = indexPath; //for the remedies indexed tab
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
            TableView.Source = new RemedyDataSource(DBMgr.Instance);

            View.BackgroundColor = Utils.getThemeColor();
            this.Title = "Remedies";
        }
    }
}