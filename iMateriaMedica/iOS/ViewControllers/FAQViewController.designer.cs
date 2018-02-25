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
    [Register ("FAQViewController")]
    partial class FAQViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView myWebView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (myWebView != null) {
                myWebView.Dispose ();
                myWebView = null;
            }
        }
    }
}