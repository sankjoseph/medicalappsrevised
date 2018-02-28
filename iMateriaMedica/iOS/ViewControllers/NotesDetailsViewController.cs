using Foundation;
using System;
using UIKit;

namespace iMateriaMedica.iOS
{
    public partial class NotesDetailsViewController : UIViewController
    {
        public bool bEditMode;
        public int nCurrentNoteIndex;
        public NotesViewController refParent;
        public NotesDetailsViewController (IntPtr handle) : base (handle)
        {
            
        }
        /// <summary>
        /// Views the did load.
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = Utils.getThemeColor();

            if (bEditMode)
            {
                MMNote noteEditing = DBMgrNotes.Instance.NoteItems[nCurrentNoteIndex];
                txtNoteName.Text = noteEditing.Name;
                txtNoteObservations.Text = noteEditing.Observations;                
            }

            UIBarButtonItem done = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, args) =>
            {
                if (Utils.Empty(txtNoteName.Text))// no matter if edit or add mode
                {
                    Utils.ShowAlertOK("Please provide a name for this note",this);
                }
                else if (Utils.Empty(txtNoteObservations.Text))// no matter if edit or add mode
                {
                    Utils.ShowAlertOK("Please provide notes. This cannot be empty", this);
                }
                else if (!bEditMode)// add mode
                {
                    // validate for existing name

                    MMNote newNote = new MMNote();
                    newNote.Id = "";
                    newNote.Name = txtNoteName.Text;
                    newNote.Observations = txtNoteObservations.Text;
                    //Notes.Add(newNote);
                    string dbExistingId = DBMgrNotes.Instance.IfFoundNoteName(newNote);
                    if (!Utils.Empty(dbExistingId))
                    {
                        Utils.ShowAlertOK("Duplicate found, Please provide another name", this);
                    }
                    else
                    {
                        DBMgrNotes.Instance.InsertNote(newNote);
                        refParent.ReloadNotes();
                        this.NavigationController.PopToRootViewController(true);
                    }
                }
                else // edit mode
                {
                    //Update record;
                    MMNote noteEditing = DBMgrNotes.Instance.NoteItems[nCurrentNoteIndex];

                    noteEditing.Name = txtNoteName.Text;
                    noteEditing.Observations = txtNoteObservations.Text;
              
                    string dbExistingId = DBMgrNotes.Instance.IfFoundNoteName(noteEditing);
                    if (!Utils.Empty(dbExistingId) && !noteEditing.Id.Equals(dbExistingId))
                    {
                        Utils.ShowAlertOK("Duplicate found, Please provide another name", this);
                    }
                    else
                    {

                        DBMgrNotes.Instance.UpdateNote(noteEditing);
                        refParent.ReloadNotes();
                        this.NavigationController.PopToRootViewController(true);
                    }
            }
            });

            this.NavigationItem.SetRightBarButtonItem(done, true);
        }

    }
}