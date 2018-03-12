using System;
using Foundation;
using UIKit;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.iOS
{
    [Register("GedcomDocument")]
    public class GedcomDocument : UIDocument
    {
        readonly FamilyTree _familyTree = FamilyTree.Instance;
        NSString _dataModel;

        public GedcomDocument(NSUrl url) : base(url) 
        {
            Contents = "";
        }

        public GedcomDocument(NSUrl url, string contents) : base(url)
        {
            // Set the default document text
            Contents = contents;
        }

        public string Contents
        {
            get { return _dataModel.ToString(); }
            set { _dataModel = new NSString(value); }
        }


        #region Override Methods
        public override bool LoadFromContents(NSObject contents, string typeName, out NSError outError)
        {
            outError = null;
            if (contents != null)
                _dataModel = NSString.FromData((NSData)contents, NSStringEncoding.UTF8);

            // Inform caller that the document has been modified
            RaiseDocumentModified(this);

            return true;
        }
        #endregion

        #region Events
        public delegate void DocumentModifiedDelegate(GedcomDocument document);
        public event DocumentModifiedDelegate DocumentModified;

        internal void RaiseDocumentModified(GedcomDocument document)
        {
            // Inform caller
            DocumentModified?.Invoke(document);
        }
        #endregion
    }
}
