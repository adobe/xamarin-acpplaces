using System;
using NUnit.Framework;
using Com.Adobe.Marketing.Mobile;
using System.Threading.Tasks;
using Android.Locations;
using Android.Runtime;

namespace ACPPlacesAndroidUnitTest
{
    [TestFixture]
    public class TestsSample
    {
        static TaskCompletionSource<Java.Lang.Object> taskCompletionSource;

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
            JavaList<PlacesPOI> pois = (JavaList<PlacesPOI>)taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
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
            JavaList<PlacesPOI> pois = (JavaList<PlacesPOI>)taskCompletionSource.Task.ConfigureAwait(false).GetAwaiter().GetResult();
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

                
        class AdobeCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object result)
            {
                taskCompletionSource.SetResult(result);
            }
        }
    }
}
