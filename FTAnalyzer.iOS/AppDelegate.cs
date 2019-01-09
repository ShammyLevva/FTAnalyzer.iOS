using System;
using Foundation;
using FTAnalyzer.Utilities;
using UIKit;

namespace FTAnalyzer
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        public GedcomDocument Document { get; set; }
        public GedcomDocumentViewController DocumentViewController { get; set;} 
        public override UIWindow Window { get; set; }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.FTAStartupAction);
            return true;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
        }

        public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }

        public void OpenDocument(NSUrl url)
        {
            Document = new GedcomDocument(url);

            // Open the document
            Document.Open((success) => {
                if (success)
                    DocumentViewController.ProcessDocument();
                else
                    Console.WriteLine("Failed to Open Document");
            });
            // Inform caller
            RaiseDocumentLoaded();
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.LoadGEDCOMEvent);
        }

        public string GetVersion() => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        public int GetBuild() => int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString());
        public string Version => $"v{GetVersion()} (Build {GetBuild()}-iOS)";

        #region Events
        public delegate void DocumentLoadedDelegate();
        public event DocumentLoadedDelegate DocumentLoaded;

        internal void RaiseDocumentLoaded()
        {
            // Inform caller
            DocumentLoaded?.Invoke();
        }
        #endregion
    }
}

