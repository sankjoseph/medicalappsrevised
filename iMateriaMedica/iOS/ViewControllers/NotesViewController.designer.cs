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
    [Register ("NotesViewController")]
    partial class NotesViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barbtnLeft { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barbtnRight { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (barbtnLeft != null) {
                barbtnLeft.Dispose ();
                barbtnLeft = null;
            }

            if (barbtnRight != null) {
                barbtnRight.Dispose ();
                barbtnRight = null;
            }
        }
    }
}