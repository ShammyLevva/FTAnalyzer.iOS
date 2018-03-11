using System;
using CloudKit;
using System.Threading.Tasks;
using CoreGraphics;
using MobileCoreServices;
using UIKit;

namespace FTAnalyzer.iOS
{
    public partial class LoadGedcomViewController : UIViewController
    {
        FamilyTree _familyTree;

        public LoadGedcomViewController (IntPtr handle) : base (handle)
        {
            _familyTree = FamilyTree.Instance;
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();
            var height = UIScreen.MainScreen.Bounds.Height;
            var width = UIScreen.MainScreen.Bounds.Width;
            var label = new UILabel(new CGRect(width / 5.0f, 0, width, height / 2.0f))
            {
                Font = UIFont.FromName("Kunstler Script", height/8.0f),
                Text = "Family Tree Analyzer",
            };
            Add(label);
            TreeImage.AddGestureRecognizer(TapGestureRecogniser);
		}

        #region Computed Properties
        /// <summary>
        /// Returns the delegate of the current running application
        /// </summary>
        /// <value>The this app.</value>
        public AppDelegate ThisApp
        {
            get { return (AppDelegate)UIApplication.SharedApplication.Delegate; }
        }
        #endregion

        partial void SelectGedcomButtonEvent(UIButton sender)
        {
            PickFile();
        }

        void PickFile()
        {
            // Allow the Document picker to select a range of document types
            string[] allowedUTIs = { UTType.CreatePreferredIdentifier(UTType.TagClassFilenameExtension, "ged", null) };

            // Display the picker
            var picker = new UIDocumentPickerViewController (allowedUTIs, UIDocumentPickerMode.Open);
            // Wireup Document Picker
            picker.DidPickDocument += (sndr, pArgs) => {

                // IMPORTANT! You must lock the security scope before you can
                // access this file
                var securityEnabled = pArgs.Url.StartAccessingSecurityScopedResource();

                // Open the document
                ThisApp.OpenDocument(pArgs.Url);

                // IMPORTANT! You must release the security lock established
                // above.
                pArgs.Url.StopAccessingSecurityScopedResource();
            };
            picker.DidPickDocumentAtUrls += (sndr, pArgs) => {
                ThisApp.OpenDocument(pArgs.Urls[0]);
            };

            // Display the document picker
            PresentViewController(picker, true, null);
         }
	}
}