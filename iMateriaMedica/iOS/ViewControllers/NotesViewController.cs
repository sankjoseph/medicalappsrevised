using Foundation;
using System;
using UIKit;
using System.Collections.ObjectModel;
//

namespace iMateriaMedica.iOS
{
    class NotesDataSource : UITableViewSource
    {
        ObservableCollection<MMNote> Notes;
        static readonly NSString CELL_IDENTIFIER = new NSString("ITEM_NOTES_CELL");

        public NotesDataSource(ObservableCollection<MMNote> ArrNotes)
        {
            Notes = ArrNotes;
        }
        //public override void WillBeginEditing(UITableView tableView, NSIndexPath indexPath)
        //{
        //    tableView.BeginUpdates();
        //    // insert the 'ADD NEW' row at the end of table display
        //    tableView.InsertRows(new NSIndexPath[] {
        //    NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0), 0)
        //}, UITableViewRowAnimation.Fade);
        //    // create a new item and add it to our underlying data (it is not intended to be permanent)

        //    MMNote newNote = new MMNote();
        //    newNote.Id = DBMgrNotes.Instance.GetMaxId();
        //    newNote.Name = "(add new)";
        //    newNote.Observations = "";
        //    Notes.Add(newNote);
        //    tableView.EndUpdates(); // applies the changes
        //}

        //public override void DidEndEditing(UITableView tableView, NSIndexPath indexPath)
        //{
        //    tableView.BeginUpdates();
        //    // remove our 'ADD NEW' row from the underlying data
        //    Notes.RemoveAt((int)tableView.NumberOfRowsInSection(0) - 1); // zero based :)
        //                                                                      // remove the row from the table display
        //    tableView.DeleteRows(new NSIndexPath[] { NSIndexPath.FromRowSection(tableView.NumberOfRowsInSection(0) - 1, 0) }, UITableViewRowAnimation.Fade);
        //    tableView.EndUpdates(); // applies the changes
        //}
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, Foundation.NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    var item = Notes[indexPath.Row];
                    //item.Id
                    DBMgrNotes.Instance.DeleteNote(item.Id);

                    // remove the item from the underlying data source
                    Notes.RemoveAt(indexPath.Row);
                    // delete the row from the table
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.None:
                    Console.WriteLine("CommitEditingStyle:None called");
                    break;
            }
        }
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you wish to disable editing for a specific indexPath or for all rows
        }
        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {   // Optional - default text is 'Delete'
            return "Delete (" + Notes[indexPath.Row].Name + ")";
        }

        public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true; // return false if you don't allow re-ordering
        }
        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return UITableViewCellEditingStyle.Delete; // this example doesn't support Insert
        }
        public override nint RowsInSection(UITableView tableview, nint section) => Notes.Count;

        public override nint NumberOfSections(UITableView tableView) => 1;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_IDENTIFIER, indexPath);

            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value2, CELL_IDENTIFIER);
            }
            var item = Notes[indexPath.Row];

            cell.TextLabel.Text = item.Name;

            cell.BackgroundColor = Utils.getThemeColor();
            cell.LayoutMargins = UIEdgeInsets.Zero;
            return cell;
        }

    }
    public partial class NotesViewController : UITableViewController
    {
        public string[] Notes = new string[3];
        public ObservableCollection<MMNote> NoteItems { get; set; }
        UIBarButtonItem done;
        public NotesViewController(IntPtr handle) : base(handle)
        {
            NoteItems = new ObservableCollection<MMNote>();
        }
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NoteDetailsSegue")
            {
                NotesDetailsViewController controller = segue.DestinationViewController as NotesDetailsViewController;
                var indexPath = TableView.IndexPathForCell(sender as UITableViewCell);
                controller.bEditMode = true;
                controller.refParent = this;
                controller.nCurrentNoteIndex = indexPath.Row;
                TableView.DeselectRow(indexPath, true);
            }

        }
        public void ReloadNotes()
        {
            DBMgrNotes.Instance.ReloadNotes();
            TableView.ReloadData();
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

#if STDVERSION
            NoteItems = DBMgrNotes.Instance.NoteItems;

            barbtnLeft.Clicked += (s, e) => {
                if (TableView.Editing)
                    TableView.SetEditing(false, true); // if we've half-swiped a row
                                                       // TableView.WillBeginEditing(TableView);
                TableView.SetEditing(true, true);

                this.NavigationItem.SetLeftBarButtonItem(done, true);
            };

            done = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, args) =>
            {
                TableView.SetEditing(false, true);
                this.NavigationItem.SetLeftBarButtonItem(barbtnLeft, true);
            });

            barbtnRight.Clicked += (s, e) =>
            {
                var storyB = UIStoryboard.FromName("Main", null);
                NotesDetailsViewController detailscontroller = storyB.InstantiateViewController("NotesDetails") as NotesDetailsViewController;
                detailscontroller.bEditMode = false;
                detailscontroller.refParent = this;
                this.NavigationController.PushViewController(detailscontroller, false);
                //UINavigationController nav = this.NavigationController;
                //detailscontroller   = new NotesDetailsViewController();
                //nav.PushViewController(detailscontroller,false);
            };
#else

            for (int i = 0; i < 2;i++)
            {
                MMNote sample = new MMNote();
                sample.Id = Convert.ToString(i+1) ;
                sample.Name = "Sample Note" + sample.Id;
                sample.Observations = "samples";
                NoteItems.Add(sample);
            }

                MMNote sample2 = new MMNote();
                sample2.Id = "3" ;
                sample2.Name = "Use paid version for all features" ;
                sample2.Observations = "samples";
                NoteItems.Add(sample2);
#endif 
            //TableView.SetEditing(true, true);
            TableView.Source = new NotesDataSource(NoteItems);
            View.BackgroundColor = Utils.getThemeColor();
            this.Title = "Notes";

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

        }
    }
}