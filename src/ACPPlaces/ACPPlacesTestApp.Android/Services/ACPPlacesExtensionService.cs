using System.Threading.Tasks;
using Com.Adobe.Marketing.Mobile;
using Java.Lang;
using Android.Locations;
using Android.Gms.Location;
using Java.Util;

namespace ACPPlacesTestApp.Droid
{
    public class ACPPlacesExtensionService : IACPPlacesExtensionService
    {
        TaskCompletionSource<string> completionSource;        

        public ACPPlacesExtensionService()
        {
        }

        public TaskCompletionSource<string> Clear()
        {
            completionSource = new TaskCompletionSource<string>();
            ACPPlaces.Clear();
            completionSource.SetResult("Cleared client side Places data.");
            return completionSource;
        }

        public TaskCompletionSource<string> GetACPPlacesExtensionVersion()
        {
            completionSource = new TaskCompletionSource<string>();
            completionSource.SetResult("Extension Version:: " + ACPPlaces.ExtensionVersion());
            return completionSource;
        }
      
        public TaskCompletionSource<string> GetCurrentPointsOfInterests()
        {
            completionSource = new TaskCompletionSource<string>();
            ACPPlaces.GetCurrentPointsOfInterest(new AdobeCallBack(completionSource));
            return completionSource;
        }

        public TaskCompletionSource<string> GetLastKnownLocation()
        {
            completionSource = new TaskCompletionSource<string>();
            ACPPlaces.GetLastKnownLocation(new AdobeCallBack(completionSource));
            return completionSource;
        }

        public TaskCompletionSource<string> GetNearbyPointOfInterests()
        {
            Location location = new Location("ACPPlacesTestApp.Xamarin");
            //San Jose down town coordinates.
            location.Latitude = 37.3309;
            location.Longitude = 121.8939;
            completionSource = new TaskCompletionSource<string>();
            ACPPlaces.GetNearbyPointsOfInterest(location, 10, new AdobeCallBack(completionSource));
            return completionSource;
        }

        public TaskCompletionSource<string> ProcessGeofence()
        {
            completionSource = new TaskCompletionSource<string>();
            GeofenceBuilder builder = new GeofenceBuilder();
            builder.SetCircularRegion(37.3309, 121.8939, 2000);
            builder.SetExpirationDuration(60 * 60 * 100); //one hour
            builder.SetRequestId("SanJose Downtown");
            builder.SetLoiteringDelay(10000);
            builder.SetTransitionTypes(Geofence.GeofenceTransitionEnter);
            builder.SetExpirationDuration(50000);
            builder.SetNotificationResponsiveness(100);
            ACPPlaces.ProcessGeofence(builder.Build(), 1); //1 is Geofence Enter Transition.
            completionSource.SetResult("Geofence Processing Completed");
            return completionSource;
        }

        public TaskCompletionSource<string> SetAuthorizationStatus()
        {
            completionSource = new TaskCompletionSource<string>();
            ACPPlaces.SetAuthorizationStatus(PlacesAuthorizationStatus.Always);
            completionSource.SetResult("Authorization status set to always.");
            return completionSource;
        }        
    }

    class AdobeCallBack : Java.Lang.Object, IAdobeCallback
    {
        TaskCompletionSource<string> completionSource = null;
        public AdobeCallBack(TaskCompletionSource<string> completionSource) {
            this.completionSource = completionSource;
        }

        public void Call(Object result)
        {
            if (result is AbstractList) {
                AbstractList placesPOIs = (AbstractList) result;                
                string poiNames = "";
                Object[] placesPoiArray = placesPOIs.ToArray();                
                foreach (Object placesPOI in placesPoiArray) {
                    poiNames += ((PlacesPOI)placesPOI).Name;
                }
                completionSource.SetResult(poiNames.EndsWith(",") ? poiNames.Substring(0, poiNames.Length - 1) : "None");
            }
            else if (result is Location) {
                Location location = (Location)result;
                string coordinates = "Latitude: " + location.Latitude.ToString() + " Logitutde: " + location.Longitude.ToString();
                completionSource.SetResult(coordinates);
            }
        }
    }
}


