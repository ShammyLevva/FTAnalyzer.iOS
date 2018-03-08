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

namespace FTAnalyzer.iOS
{
    [Register ("GedcomDocumentViewController")]
    partial class GedcomDocumentViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView _familiesProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView _individualsProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView _relationshipsProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView _sourcesProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView _statusTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FTAnalyzer.iOS.GedcomDocumentViewController GedcomReport { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_familiesProgress != null) {
                _familiesProgress.Dispose ();
                _familiesProgress = null;
            }

            if (_individualsProgress != null) {
                _individualsProgress.Dispose ();
                _individualsProgress = null;
            }

            if (_relationshipsProgress != null) {
                _relationshipsProgress.Dispose ();
                _relationshipsProgress = null;
            }

            if (_sourcesProgress != null) {
                _sourcesProgress.Dispose ();
                _sourcesProgress = null;
            }

            if (_statusTextView != null) {
                _statusTextView.Dispose ();
                _statusTextView = null;
            }

            if (GedcomReport != null) {
                GedcomReport.Dispose ();
                GedcomReport = null;
            }
        }
    }
}