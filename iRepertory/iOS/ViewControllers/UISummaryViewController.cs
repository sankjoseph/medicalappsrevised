using Foundation;
using System;
using UIKit;

using System.Collections.Generic;
using System.Linq;

namespace iRepertory.iOS
{
    class RepSummaryDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL_REPERTORY");


        public RepSummaryDataSource(DBMgrKent dbMgr)
        {

        }

        public override nint RowsInSection(UITableView tableview, nint section) => DBMgrKent.Instance.Sections.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }
            // NavigationController.PopToRootViewController(true);
            var item = DBMgrKent.Instance.Sections[indexPath.Row];
            cell.TextLabel.Text = item;
            //cell.DetailTextLabel.Text = item.Name;
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;
            return cell;
        }

    }
    
    public partial class UISummaryViewController : UITableViewController
    {
        public UISummaryViewController (IntPtr handle) : base (handle)
        {
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "RepSectionDetailsSegue")
            {
                SectionDataViewController controller = segue.DestinationViewController as SectionDataViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                var itemSelected = DBMgrKent.Instance.Sections[indexPath.Row];

                IEnumerable<RepertoryItem> query =
         (from item in DBMgrKent.Instance.RepItems
                    where item.Section.ToUpper().Equals(itemSelected.ToUpper())
         select item);

                DBMgrKent.Instance.FilteredSectionRepItems = query.ToList();

              TableView.DeselectRow(indexPath, true);
            }

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            TableView.Source = new RepSummaryDataSource(DBMgrKent.Instance);
            Title = "Summary";
            View.BackgroundColor = Utils.getThemeColor();
 
        }
    }
}