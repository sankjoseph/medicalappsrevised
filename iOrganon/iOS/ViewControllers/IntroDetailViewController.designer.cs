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

namespace iOrganon.iOS
{
    [Register ("IntroDetailViewController")]
    partial class IntroDetailViewController
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