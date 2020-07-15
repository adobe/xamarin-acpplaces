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
using System.Threading.Tasks;
using Com.Adobe.Marketing.Mobile;
using ObjCRuntime;
using Foundation;
using CoreLocation;

namespace ACPPlacesTestApp.iOS
{
    public class ACPPlacesExtensionService : IACPPlacesExtensionService
    {
        TaskCompletionSource<string> taskCompletionSource;

        public ACPPlacesExtensionService()
        {
        }

        public TaskCompletionSource<string> Clear()
        {
            taskCompletionSource = new TaskCompletionSource<string>();
            ACPPlaces.Clear();
            taskCompletionSource.SetResult("Successfully cleared the Placed data.");
            return taskCompletionSource;
        }

        public TaskCompletionSource<string> GetACPPlacesExtensionVersion()
        {
            taskCompletionSource = new TaskCompletionSource<string>();
            string version = ACPPlaces.ExtensionVersion;
            taskCompletionSource.SetResult("Places Extension Version:: "+ version);
            return taskCompletionSource;
        }

        public TaskCompletionSource<string> GetCurrentPointsOfInterests()
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            Action<NSArray<ACPPlacesPoi>> action = (pois) => {
                string poiNames = "";
                foreach (ACPPlacesPoi poi in pois) {
                    poiNames += (poi.Name + ",");
                }
                
                taskCompletionSource.SetResult(poiNames.EndsWith(",") ? poiNames.Substring(0, poiNames.Length - 1) : "None");
            };
            ACPPlaces.GetCurrentPointsOfInterest(action);
            return taskCompletionSource;
        }

        public TaskCompletionSource<string> GetLastKnownLocation()
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            Action<CLLocation> action = (location) => {                
                string coordinates = "Latitude: " + location.Coordinate.Latitude + " Logitutde: " + location.Coordinate.Longitude;                
                taskCompletionSource.SetResult(coordinates);
            };
            ACPPlaces.GetLastKnownLocation(action);
            return taskCompletionSource;
        }

        public TaskCompletionSource<string> GetNearbyPointOfInterests()
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            Action<NSArray<ACPPlacesPoi>> action = (pois) => {
                string poiNames = "";
                foreach (ACPPlacesPoi poi in pois)
                {
                    poiNames += (poi.Name + ",");
                }

                taskCompletionSource.SetResult(poiNames.EndsWith(",") ? poiNames.Substring(0, poiNames.Length - 1) : "None");
            };

            ACPPlaces.GetNearbyPointsOfInterest(new CLLocation(37.3309, -121.8939),10, action); //Coordinates of San Jose Downtown.
            return taskCompletionSource;
        }

        public TaskCompletionSource<string> ProcessGeofence()
        {
            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            CLLocationCoordinate2D coordinate = new CLLocationCoordinate2D();
            coordinate.Latitude = 37.3309;
            coordinate.Longitude = -121.8939;
            
            ACPPlaces.ProcessRegionEvent(new CLCircularRegion(coordinate, 2000, "ACPPlacesTestApp.xamarin"), ACPRegionEventType.Entry); //Coordinates points to San Jose Downtown.
            taskCompletionSource.SetResult("Successfully process Geofence.");
            return taskCompletionSource;
        }

        public TaskCompletionSource<string> SetAuthorizationStatus()
        {
            taskCompletionSource = new TaskCompletionSource<string>();
            ACPPlaces.SetAuthorizationStatus(CLAuthorizationStatus.Authorized);
            taskCompletionSource.SetResult("Successfully Set Authorization status to Authorized.");
            return taskCompletionSource;
        }
    }
}
