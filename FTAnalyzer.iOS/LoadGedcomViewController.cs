using System;
using CoreGraphics;
using UIKit;

namespace FTAnalyzer.iOS
{
    public partial class LoadGedcomViewController : UIViewController
    {
        public LoadGedcomViewController (IntPtr handle) : base (handle)
        {
            var height = UIScreen.MainScreen.Bounds.Height;
            var width = UIScreen.MainScreen.Bounds.Width;
            var label = new UILabel(new CGRect(0, 0, width, height / 2));
            label.Font = UIFont.FromName("Kunstler Script", 72.0f);
            label.Text = "Family Tree Analyzer";
            Add(label);
        }
    }
}