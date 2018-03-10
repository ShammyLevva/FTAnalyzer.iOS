using System;
using System.IO;
using System.Threading.Tasks;
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
            //having picked the file now display the GedcomDocumentViewController and load the tree
            //await LoadTreeAsync(filename);
        }


	}
}