using Foundation;
using System;
using UIKit;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Web;

namespace FTAnalyzer
{
    public partial class GedcomDocumentViewController : UIViewController
    {
        FamilyTree _familyTree;
        public IProgress<string> Messages { get; }
        public IProgress<int> Sources { get; }
        public IProgress<int> Individuals { get; }
        public IProgress<int> Families { get; }
        public IProgress<int> Relationships { get; }

        public GedcomDocument Document { get; set; }
        public AppDelegate App => (AppDelegate)UIApplication.SharedApplication.Delegate;

        public GedcomDocumentViewController(IntPtr handle) : base(handle)
        {
            Messages = new Progress<string>(AppendMessage);
            Sources = new Progress<int>(percent => SetProgress(_sourcesProgress, percent));
            Individuals = new Progress<int>(percent => SetProgress(_individualsProgress, percent));
            Families = new Progress<int>(percent => SetProgress(_familiesProgress, percent));
            Relationships = new Progress<int>(percent => SetProgress(_relationshipsProgress, percent));
            _familyTree = FamilyTree.Instance;
            App.DocumentViewController = this;
        }

        public void ProcessDocument()
        {
            ClearAllProgress();
            Document = App.Document;
            if (Document != null)
            {
                var result = Task.Run(() => LoadTreeAsync(Document.URL.ToString()));
                if (result.IsCompletedSuccessfully)
                    SetupViews();
            }
        }

        public void ClearAllProgress()
        {
            if (!NSThread.IsMain)
            {
                InvokeOnMainThread(ClearAllProgress);
                return;
            }
            _statusTextView.Text = string.Empty;
            _sourcesProgress.SetProgress(0, false);
            _individualsProgress.SetProgress(0, false);
            _familiesProgress.SetProgress(0, false);
            _relationshipsProgress.SetProgress(0, false);
        }

        void AppendMessage(string message)
        {
            if (!NSThread.IsMain)
            {
                InvokeOnMainThread(() => AppendMessage(message));
                return;
            }
            if (_statusTextView.Text == null)
                _statusTextView.Text = message;
            else
                _statusTextView.Text += message;
        }

        void SetProgress(UIProgressView progressBar, int percent)
        {
            if (!NSThread.IsMain)
            {
                InvokeOnMainThread(() => SetProgress(progressBar, percent));
                return;
            }
            progressBar.SetProgress(percent, true);
        }

        public async Task<bool> LoadTreeAsync(string filename)
        {
            var outputText = new Progress<string>(AppendMessage);
            string file = HttpUtility.UrlDecode(Path.GetFileName(filename));
            XmlDocument doc = await Task.Run(() => _familyTree.LoadTreeHeader(file, Document.Stream, outputText));
            if (doc != null)
            {
                await Task.Run(() => AppendMessage("\nFile loaded starting to Analyse\n"));
                await Task.Run(() => _familyTree.LoadTreeSources(doc, Sources, outputText));
                await Task.Run(() => _familyTree.LoadTreeIndividuals(doc, Individuals, outputText));
                await Task.Run(() => _familyTree.LoadTreeFamilies(doc, Families, outputText));
                await Task.Run(() => _familyTree.LoadTreeRelationships(doc, Relationships, outputText));
                await Task.Run(() => AppendMessage($"\n\nFinished loading and analysing file {file}\n"));
                return true;
            }
            return false;
        }

        void SetupViews()
        {

        }
    }
}