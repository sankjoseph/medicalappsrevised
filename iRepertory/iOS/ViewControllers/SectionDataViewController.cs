using Foundation;
using System;
using UIKit;

namespace iRepertory.iOS
{
    class RepSectionDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL_REPERTORY");


        public RepSectionDataSource(DBMgrKent dbMgr)
        {

        }

        public override nint RowsInSection(UITableView tableview, nint section) => DBMgrKent.Instance.FilteredSectionRepItems.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }
            // NavigationController.PopToRootViewController(true);
            var item = DBMgrKent.Instance.FilteredSectionRepItems[indexPath.Row];
            cell.TextLabel.Text = item.SubSection;
            //cell.DetailTextLabel.Text = item.Name;
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;
            return cell;
        }

    }
    public partial class SectionDataViewController : UITableViewController
    {
        public SectionDataViewController (IntPtr handle) : base (handle)
        {
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "RepSectionDetailsSegue")
            {
                RepInfoViewController controller = segue.DestinationViewController as RepInfoViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                controller.bSearch = true;
                controller.bFromSectionFilter = true;
                controller.nCurrentItemIndex = indexPath.Row;
                TableView.DeselectRow(indexPath, true);
            }
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.Source = new RepSectionDataSource(DBMgrKent.Instance);
            View.BackgroundColor = Utils.getThemeColor();

        }
    }
}