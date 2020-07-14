using System.Threading.Tasks;
using Com.Adobe.Marketing.Mobile;
using Android.Runtime;
using Android.Locations;
using Android.Gms.Location;

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

        public void Call(Java.Lang.Object result)
        {
            if (result is JavaList) {
                JavaList<PlacesPOI> placesPOIs = (JavaList<PlacesPOI>) result;
                string poiNames = "";
                foreach (PlacesPOI placesPOI in placesPOIs) {
                    poiNames += placesPOI.Name;
                }
                completionSource.SetResult(poiNames.Substring(0, poiNames.Length - 1));
            }
            else if (result is Location) {
                Location location = (Location)result;
                string coordinates = "Latitude: " + location.Latitude.ToString() + " Logitutde: " + location.Longitude.ToString();
                completionSource.SetResult(coordinates);
            }
        }
    }
}


