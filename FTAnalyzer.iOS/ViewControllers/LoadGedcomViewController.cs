using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using CoreGraphics;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
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

        partial void ImageTapEvent(UITapGestureRecognizer sender)
        {
            Task<string>.Run(() => PickGedcomFile());
        }

        partial void SelectGedcomButtonEvent(UIButton sender)
        {
            //CrossFilePicker.Current.PickFile();
            Task<string>.Run(() => PickGedcomFile());
        }

        async Task PickGedcomFile()
        {
            FileData pickedFile = await CrossFilePicker.Current.PickFile();
            var filename = pickedFile.FilePath + pickedFile.FileName;

            await LoadTreeAsync(filename);
        }

        async Task<bool> LoadTreeAsync(string filename)
        {
            //var outputText = new Progress<string>(value => { rtbOutput.AppendText(value); });
            //XmlDocument doc = await Task.Run(() => _familyTree.LoadTreeHeader(filename, outputText));
            //if (doc == null) return false;
            //var sourceProgress = new Progress<int>(value => { pbSources.Value = value; });
            //var individualProgress = new Progress<int>(value => { pbIndividuals.Value = value; });
            //var familyProgress = new Progress<int>(value => { pbFamilies.Value = value; });
            //var RelationshipProgress = new Progress<int>(value => { pbRelationships.Value = value; });
            //await Task.Run(() => _familyTree.LoadTreeSources(doc, sourceProgress, outputText));
            //await Task.Run(() => _familyTree.LoadTreeIndividuals(doc, individualProgress, outputText));
            //await Task.Run(() => _familyTree.LoadTreeFamilies(doc, familyProgress, outputText));
            //await Task.Run(() => _familyTree.LoadTreeRelationships(doc, RelationshipProgress, outputText));
            var message = UIAlertController.Create("FTAnalyzer", "Hello World!",UIAlertControllerStyle.Alert);
            await DoNothing();
            PresentViewController(message, true, null);
            return true;
        }

        Task DoNothing() { return null; }

        public string LoadFile (string filename) {
            var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var filePath = Path.Combine (documentsPath, filename);
            return File.ReadAllText (filePath);
        }

	}
}