﻿using Foundation;
using UIKit;

namespace PwnedPass2.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register ("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {            
        public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
        {
            // Override point for customization after application launch.
            // If not required for your application you can safely delete this method
            global::Xamarin.Forms.Forms.Init();
            App.InitHash(new Hash());
            App.InitAPI(new GetApi());
            LoadApplication(new App(new AppSetup()));
            return base.FinishedLaunching(application, launchOptions);
        }        
    }
}

