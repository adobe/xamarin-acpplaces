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

using NUnit.Framework;
using Com.Adobe.Marketing.Mobile;
using System.Threading.Tasks;
using Android.Locations;
using Java.Util;
using Java.Lang;

namespace ACPPlacesAndroidUnitTest
{
    [TestFixture]
    public class TestsSample
    {
        static TaskCompletionSource<Object> taskCompletionSource;

        [SetUp]
        public void Setup() {
            taskCompletionSource = new TaskCompletionSource<Java.Lang.Object>();
        }

        [Test]
        public void TestGetACPPlacesExtensionVersion()
        {
            string version = ACPPlaces.ExtensionVersion();
            Assert.That(version, Is.EqualTo("1.4.2"));
        }

        [Test]
        public void TestGetNearbyPointOfInterests()
        {
            Location location = new Location("ACPPlacesTestApp.Xamarin");
            location.Latitude = 37.3309;
            location.Longitude = 121.8939;
            ACPPlaces.GetNearbyPointsOfInterest(location, 0, new AdobeCallback());
            AbstractList pois = (AbstractList)taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
            Assert.That(pois.Size, Is.EqualTo(0));
        }

        [Test]
        public void TestGetCurrentPointOfInterests()
        {
            Location location = new Location("ACPPlacesTestApp.Xamarin");
            //Random coordinates
            location.Latitude = 137.3309;
            location.Longitude = 11.8939;
            ACPPlaces.GetCurrentPointsOfInterest(new AdobeCallback());
            AbstractList pois = (AbstractList)taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
            Assert.That(pois.Size, Is.EqualTo(0));
        }

        [Test]
        public void TestGetLastKnownLocation()
        {
            Location location = new Location("ACPPlacesTestApp.Xamarin");
            //Random coordinates
            location.Latitude = 137.3309;
            location.Longitude = 11.8939;
            ACPPlaces.GetLastKnownLocation(new AdobeCallback());
            Location lastLocation = (Location)taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
            Assert.That(lastLocation, Is.Null);
        }

                
        class AdobeCallback : Object, IAdobeCallback
        {
            public void Call(Object result)
            {
                taskCompletionSource.SetResult(result);
            }
        }
    }
}
