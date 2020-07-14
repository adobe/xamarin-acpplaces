using System;
using Com.Adobe.Marketing.Mobile;
using NUnit.Framework;
using System.Threading.Tasks;
using CoreLocation;
using Foundation;
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
            ACPPlaces.GetNearbyPointsOfInterest(new CLLocation(37.3309, 121.8939), 0, (pois) => {
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
            ACPPlaces.GetLastKnownLocation((location) => {
                Assert.That(location, Is.Null);
                latch.Signal();
                latch.Dispose();
            });
            latch.Wait();
        }
    }
}
