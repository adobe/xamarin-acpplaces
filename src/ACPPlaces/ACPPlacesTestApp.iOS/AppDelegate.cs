/*
Copyright 2020 Adobe
All Rights Reserved.

NOTICE: Adobe permits you to use, modify, and distribute this file in
accordance with the terms of the Adobe license agreement accompanying
it. If you have received this file from a source other than Adobe,
then your use, modification, or distribution of it requires the prior
written permission of Adobe. (See LICENSE-MIT for details)
*/

using Foundation;
using UIKit;
using Com.Adobe.Marketing.Mobile;
using Xamarin.Forms;

namespace ACPPlacesTestApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            //Registering Adobe Extensions
            ACPCore.SetWrapperType(ACPMobileWrapperType.Xamarin);           
            ACPSignal.RegisterExtension();
            ACPPlaces.RegisterExtension();

            ACPCore.ConfigureWithAppID("{your-launch-id}");
            ACPCore.Start(null);

            DependencyService.Register<IACPPlacesExtensionService, ACPPlacesExtensionService>();

            return base.FinishedLaunching(app, options);
        }

        // Called when the application is launched and every time the app returns to the foreground.
        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            ACPCore.LifecycleStart(null);
        }

        // Called when the application is about to enter the background, be suspended, or when the user receives an interruption such as a phone call or text.
        public override void OnResignActivation(UIApplication uiApplication)
        {
            base.OnResignActivation(uiApplication);
            ACPCore.LifecyclePause();
        }
    }    
}
