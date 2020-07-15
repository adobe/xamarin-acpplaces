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

using Com.Adobe.Marketing.Mobile;
using NUnit.Framework;
using CoreLocation;
using System.Threading;


namespace ACPPlacesiOSUnitTest
{
    [TestFixture]
    public class ACPPlacesUnitTest
    {

        CountdownEvent latch;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetACPPlacesExtensionVersion()
        {
            string version = ACPPlaces.ExtensionVersion;
            Assert.That(version, Is.EqualTo("1.3.1"));
        }

        [Test]
        public void TestGetNearbyPointOfInterests()
        {
            latch = new CountdownEvent(1);
            ACPPlaces.GetNearbyPointsOfInterest(new CLLocation(37.3309, 121.8939), 0, (pois) =>
            {
                Assert.That(pois.Count, Is.EqualTo(0));
                latch.Signal();
                latch.Dispose();
            });
            latch.Wait(1000);
        }

        [Test]
        public void TestGetCurrentPointOfInterests()
        {
            latch = new CountdownEvent(1);

            ACPPlaces.GetCurrentPointsOfInterest((pois) => {
                Assert.That(pois.Count, Is.EqualTo(0));
                latch.Signal();
                latch.Dispose();
            });
            latch.Wait(1000);
        }

        [Test]
        public void TestGetLastKnownLocation()
        {
            latch = new CountdownEvent(1);
            ACPPlaces.GetLastKnownLocation((location) =>
            {
                Assert.IsNotNull(location);
                latch.Signal();
                latch.Dispose();
            });
            latch.Wait();
        }
    }
}
