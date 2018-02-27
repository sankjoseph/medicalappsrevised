using Foundation;
using System;
using UIKit;

//

namespace iMateriaMedica.iOS
{
    class NotesDataSource : UITableViewSource
    {
        public string[] Notes;
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_NOTES_CELL");

        public NotesDataSource(string[] strarry)
        {
            Notes = strarry;
        }
   
        public override nint RowsInSection(UITableView tableview, nint section) => Notes.Length;

    public override nint NumberOfSections(UITableView tableView) => 1;

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
        var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

        if (cell == null)
        {
            cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
        }
            cell.TextLabel.Text = Notes[indexPath.Row];
     
        cell.BackgroundColor = Utils.getThemeColor();
        cell.LayoutMargins = UIEdgeInsets.Zero;
        return cell;
    }
    
    }
    public partial class NotesViewController : UITableViewController
    {
        public string[] Notes = new string[3]; 

        public NotesViewController (IntPtr handle) : base (handle)
        {
  
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Notes[0] = "Sample Note1";
            Notes[1] = "Sample Note2";
            Notes[2] = "Use paid version for all features";
            TableView.Source = new NotesDataSource(Notes);
            View.BackgroundColor = Utils.getThemeColor();
            this.Title = "Notes";

        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }
    }
}