using Foundation;
using System;
using UIKit;

namespace iOrganon.iOS
{
    class ChapterDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL_CHAPTER");

        public ChapterDataSource(DBMgr dbMgr)
        {
          
        }

        public override nint RowsInSection(UITableView tableview, nint section) => DBMgr.Instance.ChapterItems.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }

            var item = DBMgr.Instance.ChapterItems[indexPath.Row];
            cell.TextLabel.Text = item.Chapter;
            cell.DetailTextLabel.Text = item.Reading;
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;

            return cell;
        }
    }
    public partial class ChapterReadViewController : UITableViewController
    {
        public DBMgr dbmgr { get; set; }

        public ChapterReadViewController (IntPtr handle) : base (handle)
        {
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigateToIntroDetailSegue")
            {
                IntroDetailViewController controller = segue.DestinationViewController as IntroDetailViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                controller.bSearch = false;
                controller.bIntroduction = false;
                controller.nCurrentItemIndex = indexPath.Row;
                Utils.itemReadingSelected = DBMgr.Instance.ChapterItems[indexPath.Row].Reading;
                TableView.DeselectRow(indexPath, true);
            }

        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = Utils.getThemeColor();
            this.Title = "Chapters";

            TableView.Source = new ChapterDataSource(DBMgr.Instance);

            View.BackgroundColor = Utils.getThemeColor();
            // TableView.RegisterNibForCellReuse(SummaryTableViewCell.Nib, "ITEM_CELL");

        }
    }
}