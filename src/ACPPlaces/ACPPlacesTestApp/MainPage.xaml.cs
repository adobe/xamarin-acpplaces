/*
Copyright 2020 Adobe
All Rights Reserved.

NOTICE: Adobe permits you to use, modify, and distribute this file in
accordance with the terms of the Adobe license agreement accompanying
it. If you have received this file from a source other than Adobe,
then your use, modification, or distribution of it requires the prior
written permission of Adobe. (See LICENSE-MIT for details)
*/

using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace ACPPlacesTestApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //ACPPlaces API's

        void OnClickGetExtensionVersion(object sender, EventArgs args)
        {
            string version = DependencyService.Get<IACPPlacesExtensionService>().GetACPPlacesExtensionVersion().Task.Result;
            Console.WriteLine("OnClickGetExtensionVersion:: " + version);
        }
        
        void OnClickGetNearbyPointOfInterests(object sender, EventArgs args)
        {
            TaskCompletionSource<string> taskCompletionSource = DependencyService.Get<IACPPlacesExtensionService>().GetNearbyPointOfInterests();
            string nearByPois = taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine("OnClickGetNearbyPointOfInterests:: " + nearByPois);
        }

        void OnClickProcessGeofence(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPPlacesExtensionService>().ProcessGeofence().Task.Result;
            Console.WriteLine("OnClickProcessGeofence:: " + result);
        }

        void OnClickGetCurrentPointsOfInterests(object sender, EventArgs args)
        {
            TaskCompletionSource<string> taskCompletionSource = DependencyService.Get<IACPPlacesExtensionService>().GetCurrentPointsOfInterests();
            string currentPois = taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine("OnClickGetNearbyPointOfInterests:: " + currentPois);
        }

        void OnClickGetLastKnownLocation(object sender, EventArgs args)
        {
            TaskCompletionSource<string> taskCompletionSource = DependencyService.Get<IACPPlacesExtensionService>().GetLastKnownLocation();
            string lastKnownLocation = taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine("OnClickGetLastKnownLocation:: " + lastKnownLocation);
        }

        void OnClickClear(object sender, EventArgs args)
        {
            string version = DependencyService.Get<IACPPlacesExtensionService>().Clear().Task.Result;
            Console.WriteLine("Clear:: " + version);
        }

        void OnClickSetAuthorizationStatus(object sender, EventArgs args)
        {
            string result = DependencyService.Get<IACPPlacesExtensionService>().SetAuthorizationStatus().Task.Result;
            Console.WriteLine("OnClickSetAuthorizationStatus:: " + result);
        }
    }
}
