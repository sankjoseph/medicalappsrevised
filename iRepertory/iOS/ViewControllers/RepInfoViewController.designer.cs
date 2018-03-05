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

namespace iRepertory.iOS
{
    [Register ("RepInfoViewController")]
    partial class RepInfoViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem barbtnRight { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem btnLeft { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIWebView myWebView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (barbtnRight != null) {
                barbtnRight.Dispose ();
                barbtnRight = null;
            }

            if (btnLeft != null) {
                btnLeft.Dispose ();
                btnLeft = null;
            }

            if (myWebView != null) {
                myWebView.Dispose ();
                myWebView = null;
            }
        }
    }
}