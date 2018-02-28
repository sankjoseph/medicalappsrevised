// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace iMateriaMedica.iOS
{
    [Register ("NotesDetailsViewController")]
    partial class NotesDetailsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField txtNoteName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView txtNoteObservations { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (txtNoteName != null) {
                txtNoteName.Dispose ();
                txtNoteName = null;
            }

            if (txtNoteObservations != null) {
                txtNoteObservations.Dispose ();
                txtNoteObservations = null;
            }
        }
    }
}