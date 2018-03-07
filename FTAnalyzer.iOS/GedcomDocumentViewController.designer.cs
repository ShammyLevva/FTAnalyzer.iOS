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
        UIKit.UIProgressView FamiliesProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        FTAnalyzer.iOS.GedcomDocumentViewController GedcomDocumentViewController { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView IndividualsProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView RelationshipsProgress { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIProgressView SourcesProgress { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (FamiliesProgress != null) {
                FamiliesProgress.Dispose ();
                FamiliesProgress = null;
            }

            if (GedcomDocumentViewController != null) {
                GedcomDocumentViewController.Dispose ();
                GedcomDocumentViewController = null;
            }

            if (IndividualsProgress != null) {
                IndividualsProgress.Dispose ();
                IndividualsProgress = null;
            }

            if (RelationshipsProgress != null) {
                RelationshipsProgress.Dispose ();
                RelationshipsProgress = null;
            }

            if (SourcesProgress != null) {
                SourcesProgress.Dispose ();
                SourcesProgress = null;
            }
        }
    }
}