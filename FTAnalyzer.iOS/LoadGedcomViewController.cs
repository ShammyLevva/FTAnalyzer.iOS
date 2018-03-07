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
        public LoadGedcomViewController (IntPtr handle) : base (handle)
        {
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
		}

        partial void SelectGedcomButtonEvent(UIButton sender)
        {
            pickedFile = CrossFilePicker.Current.PickFile();
//            var pickedFile = CrossFilePicker.Current.PickFile();
//            var filename = pickedFile.Result.FileName;
//            var filepath = pickedFile.Result.FilePath;
//            Console.WriteLine("Filepath: " + filepath + " and Filename: " + filename);
        }

        async Task<FileData> PickGedcomFile()
        {
            var pickedFile = await CrossFilePicker.Current.PickFile();
            return pickedFile;
        }

        public string LoadFile (string filename) {
            var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var filePath = Path.Combine (documentsPath, filename);
            return File.ReadAllText (filePath);
        }

	}
}