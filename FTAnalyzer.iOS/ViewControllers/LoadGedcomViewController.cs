using System;
using CloudKit;
using System.Threading.Tasks;
using CoreGraphics;
using MobileCoreServices;
using UIKit;
using Foundation;

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
            // Allow the Document picker to select files with ged extension
            string[] allowedUTIs = { 
                UTType.CreatePreferredIdentifier(UTType.TagClassFilenameExtension, "ged", null),
                UTType.CreatePreferredIdentifier(UTType.ImportedTypeDeclarationsKey, "com.ftanalyzer.ged", "public-plain-text")
            };
            var picker = new UIDocumentPickerViewController(allowedUTIs, UIDocumentPickerMode.Open);
            picker.DidPickDocumentAtUrls += (sndr, pArgs) =>
            {
                ThisApp.OpenDocument(pArgs.Urls[0]);
            };
            ThisApp.DocumentLoaded += () =>
            {
                PerformSegue("SegueToGedcomDocument", null);
            };

            // Display the document picker
            PresentViewController(picker, true, null);
        }
	}
}