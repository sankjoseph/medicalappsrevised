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
    [Register ("SearchViewController")]
    partial class SearchViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchBar mySearchBar { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mySearchBar != null) {
                mySearchBar.Dispose ();
                mySearchBar = null;
            }
        }
    }
}