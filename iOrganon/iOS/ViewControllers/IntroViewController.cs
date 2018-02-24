using Foundation;
using System;
using UIKit;
using CoreGraphics;
using System.Drawing;
namespace iOrganon.iOS
{


    class IntroDataSource : UITableViewSource
    {
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_CELL_INTRO");

        public IntroDataSource(DBMgr dbMgr)
        {
          
        }

        public override nint RowsInSection(UITableView tableview, nint section) => DBMgr.Instance.IntroItems.Count;
        public override nint NumberOfSections(UITableView tableView) => 1;

       /* public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
        {
  
        }*/

        /*public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
           
            var item = dbMgr.IntroItems[indexPath.Row];
            nfloat height =  HeightAdjusted(tableView,item.Para);
            return height;
        }*/

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.CellAt(indexPath);

            /*Utils.itemIntroSelected = dbMgr.IntroItems[indexPath.Row];

            UIStoryboard board = UIStoryboard.FromName("Main", null);
            UIViewController viewController = (UIViewController)board.InstantiateViewController("IntroDetailsID");
            //self.NavigationController.PushViewController(viewController, true);
            tableView.DeselectRow(indexPath, true);*/
        }

        public nfloat HeightAdjusted(UITableView tableView,string labelDisplay)
        {
            nfloat nHeight = 0.0F;

            var size = labelDisplay.StringSize(UIFont.FromName("Helvetica", 20.0f),
                                               new SizeF((float)tableView.Frame.Width, float.MaxValue)
                                               ,UILineBreakMode.WordWrap); // The 70 can be any number, the width is the important part
            return size.Height; // 16 is for padding


            /*var font = labelDisplay.Font;
            var size = labelDisplay.Frame.Size;

            for (var maxSize = labelDisplay.Font.PointSize;
                 maxSize >= labelDisplay.MinimumScaleFactor * labelDisplay.Font.PointSize; maxSize = 900f)
            {
                font = font.WithSize(maxSize);
                var constraintSize = new CGSize(size.Width, float.MaxValue);
                size = (new NSString(labelDisplay.Text)).StringSize(font, constraintSize, UILineBreakMode.WordWrap);
            }


            nHeight = size.Height;
            return nHeight;*/
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }
           // NavigationController.PopToRootViewController(true);
            var item = DBMgr.Instance.IntroItems[indexPath.Row];
            cell.TextLabel.Text = item.Id;
            cell.DetailTextLabel.Text = item.Para;
            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;

            /*cell.DetailTextLabel.LineBreakMode = UILineBreakMode.WordWrap;
            cell.DetailTextLabel.Lines = 0;
            cell.DetailTextLabel.TextAlignment = UITextAlignment.Right;
            //nfloat hght = HeightAdjusted(cell.DetailTextLabel);

            cell.DetailTextLabel.Frame = new CGRect(cell.DetailTextLabel.Frame.X, 
                                                    cell.DetailTextLabel.Frame.Y,
                                                    cell.DetailTextLabel.Frame.Size.Width, 
                                                    HeightAdjusted(tableView,cell.DetailTextLabel.Text));*/


     
            return cell;
        }
    }

    public partial class IntroViewController : UITableViewController
    {
        public DBMgr dbmgr { get; set; }
        public IntroViewController (IntPtr handle) : base (handle)
        {
 
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigateToIntroDetailSegue")
            {
                IntroDetailViewController controller = segue.DestinationViewController as IntroDetailViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                controller.bSearch = false;
                controller.bIntroduction = true;
                controller.nCurrentItemIndex = indexPath.Row;
                Utils.itemReadingSelected = DBMgr.Instance.IntroItems[indexPath.Row].Para;
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
            TableView.Source = new IntroDataSource(DBMgr.Instance);
 
            View.BackgroundColor = Utils.getThemeColor();
            this.Title = "Introduction";
        }
    }
}