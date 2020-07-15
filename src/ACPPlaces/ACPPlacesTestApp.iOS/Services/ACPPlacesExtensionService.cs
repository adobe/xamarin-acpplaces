/*
 Copyright 2020 Adobe. All rights reserved.
 This file is licensed to you under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License. You may obtain a copy
 of the License at http://www.apache.org/licenses/LICENSE-2.0
 Unless required by applicable law or agreed to in writing, software distributed under
 the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR REPRESENTATIONS
 OF ANY KIND, either express or implied. See the License for the specific language
 governing permissions and limitations under the License.
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
