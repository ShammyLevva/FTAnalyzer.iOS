using Foundation;
using System;
using UIKit;
using System.Threading.Tasks;
using System.Xml;
using FTAnalyzer;

namespace FTAnalyzer.iOS
{
    public partial class GedcomDocumentViewController : UIViewController
    {
        IProgress<string> _messages;
        IProgress<int> _sources;
        IProgress<int> _individuals;
        IProgress<int> _families;
        IProgress<int> _relationships;
        FamilyTree _familyTree;
        GedcomDocument _document;

        public GedcomDocumentViewController(GedcomDocument document)
        {
            _document = document;
            _messages = new Progress<string>(AppendMessage);
            _sources = new Progress<int>(percent => SetProgress(_sourcesProgress, percent));
            _individuals = new Progress<int>(percent => SetProgress(_individualsProgress, percent));
            _families = new Progress<int>(percent => SetProgress(_familiesProgress, percent));
            _relationships = new Progress<int>(percent => SetProgress(_relationshipsProgress, percent));
            _familyTree = FamilyTree.Instance;
        }

        public IProgress<string> Messages => _messages;
        public IProgress<int> Sources => _sources;
        public IProgress<int> Individuals => _individuals;
        public IProgress<int> Families => _families;
        public IProgress<int> Relationships => _relationships;

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
            {
                _statusTextView.Text = message;
            }
            else
            {
                _statusTextView.Text += message;
            }
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
            XmlDocument doc = _familyTree.LoadTreeHeader(filename, outputText);
            if (doc == null) return false;
            var sourceProgress = new Progress<int>(value => { SetProgress(_sourcesProgress, value); });
            var individualProgress = new Progress<int>(value => { SetProgress(_individualsProgress, value); });
            var familyProgress = new Progress<int>(value => { SetProgress(_familiesProgress, value); });
            var RelationshipProgress = new Progress<int>(value => { SetProgress(_relationshipsProgress, value); });
            await Task.Run(() => _familyTree.LoadTreeSources(doc, sourceProgress, outputText));
            await Task.Run(() => _familyTree.LoadTreeIndividuals(doc, individualProgress, outputText));
            await Task.Run(() => _familyTree.LoadTreeFamilies(doc, familyProgress, outputText));
            await Task.Run(() => _familyTree.LoadTreeRelationships(doc, RelationshipProgress, outputText));
            return true;
        }

        //public override bool ReadFromUrl(NSUrl url, string typeName, out NSError outError)
        //{
        //    outError = NSError.FromDomain(NSError.OsStatusErrorDomain, -4);

        //    GedcomDocumentViewController documentViewController = null;
        //    BindingListViewController<IDisplayIndividual> individualsViewController = null;
        //    BindingListViewController<IDisplayFamily> familiesViewController = null;
        //    BindingListViewController<IDisplaySource> sourcesViewController = null;
        //    BindingListViewController<IDisplayOccupation> occupationsViewController = null;
        //    BindingListViewController<IDisplayFact> factsViewController = null;
        //    UITabBarController tabbedViewController = null;

        //    InvokeOnMainThread(() =>
        //    {
        //        var handle = new IntPtr();
        //        documentViewController = new GedcomDocumentViewController(handle);
        //        documentViewController.ClearAllProgress();

        //        var mainListsViewController = tabbedViewController.ChildViewControllers[1] as UITabBarController;

        //        individualsViewController = new BindingListViewController<IDisplayIndividual>("Individuals");
        //        familiesViewController = new BindingListViewController<IDisplayFamily>("Families");
        //        sourcesViewController = new BindingListViewController<IDisplaySource>("Sources");
        //        occupationsViewController = new BindingListViewController<IDisplayOccupation>("Occupations");
        //        factsViewController = new BindingListViewController<IDisplayFact>("Facts");

        //        mainListsViewController.AddChildViewController(individualsViewController);
        //        mainListsViewController.AddChildViewController(familiesViewController);
        //        mainListsViewController.AddChildViewController(sourcesViewController);
        //        mainListsViewController.AddChildViewController(occupationsViewController);
        //        mainListsViewController.AddChildViewController(factsViewController);
        //    });

        //    var document = _familyTree.LoadTreeHeader(url.Path, documentViewController.Messages);
        //    if (document == null)
        //    {
        //        documentViewController.Messages.Report("\n\nUnable to load file " + url.Path + "\n");
        //        return false;
        //    }

        //    _familyTree.LoadTreeSources(document, documentViewController.Sources, documentViewController.Messages);
        //    _familyTree.LoadTreeIndividuals(document, documentViewController.Individuals, documentViewController.Messages);
        //    _familyTree.LoadTreeFamilies(document, documentViewController.Families, documentViewController.Messages);
        //    _familyTree.LoadTreeRelationships(document, documentViewController.Relationships, documentViewController.Messages);

        //    individualsViewController.RefreshDocumentView(_familyTree.AllDisplayIndividuals);
        //    familiesViewController.RefreshDocumentView(_familyTree.AllDisplayFamilies);
        //    sourcesViewController.RefreshDocumentView(_familyTree.AllDisplaySources);
        //    occupationsViewController.RefreshDocumentView(_familyTree.AllDisplayOccupations);
        //    factsViewController.RefreshDocumentView(_familyTree.AllDisplayFacts);

        //    documentViewController.Messages.Report("\n\nFinished loading file " + url.Path + "\n");
        //    return true;
        //}
 
    }
}