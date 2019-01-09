using System;
using CloudKit;
using System.Threading.Tasks;
using CoreGraphics;
using MobileCoreServices;
using UIKit;
using Foundation;
using FTAnalyzer.Utilities;
using System.IO;
using System.Net;

namespace FTAnalyzer.iOS
{
    public partial class LoadGedcomViewController : UIViewController
    {
        FamilyTree _familyTree;
        AppDelegate App => (AppDelegate)UIApplication.SharedApplication.Delegate;

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

        partial void SelectGedcomButtonEvent(UIButton sender)
        {
            // Allow the Document picker to select files with ged extension
            string[] allowedUTIs = { 
                UTType.CreatePreferredIdentifier(UTType.TagClassFilenameExtension, "ged", null),
                UTType.CreatePreferredIdentifier(UTType.ImportedTypeDeclarationsKey, "com.ftanalyzer.ged", "public-plain-text")
            };
            var picker = new UIDocumentPickerViewController(allowedUTIs, UIDocumentPickerMode.Open);
            picker.DidPickDocumentAtUrls += (sndr, pArgs) => App.OpenDocument(pArgs.Urls[0]);
            App.DocumentLoaded += () => PerformSegue("SegueToGedcomDocument", null);

            // Display the document picker
            PresentViewController(picker, true, null);
        }

        partial void PasteGedcomButtonEvent(UIButton sender)
        {
            UIPasteboard clipboard = UIPasteboard.General;
            if(clipboard.HasStrings && clipboard.String.StartsWith("http"))
            {
                App.DocumentLoaded += () => PerformSegue("SegueToGedcomDocument", null);
                NSUrl url = CreateTempfileFromURL(clipboard.String);
                if(url != null)
                    App.OpenDocument(url);
            }
        }

        NSUrl CreateTempfileFromURL(string webURL)
        {
            try
            {
                string tempfile = UIHelpers.CreateTempFile();
                if (!string.IsNullOrEmpty(tempfile))
                {
                    StreamWriter writer = new StreamWriter(tempfile);
                    using (WebClient client = new WebClient())
                    {
                        string s = client.DownloadString(webURL);
                        writer.Write(s);
                    }
                    writer.Close();
                    return new NSUrl(tempfile);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}