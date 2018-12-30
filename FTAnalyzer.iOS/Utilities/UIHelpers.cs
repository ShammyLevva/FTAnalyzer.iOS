using UIKit;

namespace FTAnalyzer
{
    public static class UIHelpers
    {
        public static int ShowMessage(string message) => ShowMessage(message, "FTAnalyzer");
        public static int ShowMessage(string message, string title)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
            alert.PresentViewController(alert, true, null);
            return Yes;
        }

        public static int ShowYesNo(string message) => ShowYesNo(message, "FTAnalyzer");
        public static int ShowYesNo(string message, string title)
        {
            var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Yes", UIAlertActionStyle.Default, null));
            alert.AddAction(UIAlertAction.Create("No", UIAlertActionStyle.Destructive, null));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
            alert.PresentViewController(alert, true, null);
            return Yes;
        }

        public static int Yes => 1000;
        public static int No => 1001;
        public static int Cancel => (int) UIAlertActionStyle.Cancel;
    }
}