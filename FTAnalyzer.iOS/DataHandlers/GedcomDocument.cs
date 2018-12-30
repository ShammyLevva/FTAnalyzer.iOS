using System;
using Foundation;
using UIKit;
using FTAnalyzer.Utilities;
using System.Xml;
using System.IO;
using System.Text;

namespace FTAnalyzer
{
    [Register("GedcomDocument")]
    public class GedcomDocument : UIDocument
    {
        readonly FamilyTree _familyTree = FamilyTree.Instance;
        NSString _dataModel;

        public GedcomDocument(NSUrl url) : base(url)
        {
            URL = url;
            Contents = "";
        }

        public GedcomDocument(NSUrl url, string contents) : base(url)
        {
            // Set the default document text
            URL = url;
            Contents = contents;
        }

        public NSUrl URL { get; set; }

        public string Contents
        {
            get { return _dataModel.ToString(); }
            set { _dataModel = new NSString(value); }
        }

        public MemoryStream Stream
        {
            get 
            { 
                byte[] byteArray = Encoding.UTF8.GetBytes(Contents);
                return new MemoryStream(byteArray);
            }
        }


        #region Override Methods
        public override bool LoadFromContents(NSObject contents, string typeName, out NSError outError)
        {
            outError = null;
            if (contents != null)
            {
                _dataModel = NSString.FromData((NSData)contents, NSStringEncoding.UTF8);
                // Inform caller that the document has been modified
                RaiseDocumentModified(this);
            }

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
