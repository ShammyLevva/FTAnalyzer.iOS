// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FTAnalyzer.iOS
{
    [Register ("LoadGedcomViewController")]
    partial class LoadGedcomViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView Messages { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITapGestureRecognizer TapGestureRecogniser { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView TreeImage { get; set; }

        [Action ("ImageTapEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ImageTapEvent (UIKit.UITapGestureRecognizer sender);

        [Action ("SelectGedcomButtonEvent:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void SelectGedcomButtonEvent (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (Messages != null) {
                Messages.Dispose ();
                Messages = null;
            }

            if (TapGestureRecogniser != null) {
                TapGestureRecogniser.Dispose ();
                TapGestureRecogniser = null;
            }

            if (TreeImage != null) {
                TreeImage.Dispose ();
                TreeImage = null;
            }
        }
    }
}