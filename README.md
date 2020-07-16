# Adobe Experience Platform - Places plugin for Xamarin apps
![CI](https://github.com/adobe/xamarin-acpplaces/workflows/CI/badge.svg)
[![ACPPlaces.Android](https://buildstats.info/nuget/Adobe.ACPPlaces.Android)](https://www.nuget.org/packages/Adobe.ACPSignal.Android/)
[![ACPPlaces.iOS](https://buildstats.info/nuget/Adobe.ACPPlaces.iOS)](https://www.nuget.org/packages/Adobe.ACPPlaces.iOS/)
[![GitHub](https://img.shields.io/github/license/adobe/xamarin-acpplaces)](https://github.com/adobe/xamarin-acpplaces/blob/master/LICENSE)

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Sample App](#sample-app)
- [Contributing](#contributing)
- [Licensing](#licensing)

## Prerequisites

Xamarin development requires the installation of [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/). Information regarding installation for Xamarin development is available for [Mac](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019) or [Windows](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019).

 An [Apple developer account](https://developer.apple.com/programs/enroll/) and the latest version of Xcode (available from the App Store) are required if you are [building an iOS app](https://docs.microsoft.com/en-us/visualstudio/mac/installation?view=vsmac-2019).

## Installation

**Package Manager Installation**

The ACPPlaces Xamarin NuGet package for Android or iOS can be added to your project by right clicking the _"Packages"_ folder within the project you are working on then selecting _"Manage NuGet Packages"_. In the window that opens, ensure that your selected source is `nuget.org` and search for _"Adobe.ACP"_. After selecting the Xamarin AEP SDK packages that are required, click on the _"Add Packages"_ button. After exiting the _"Add Packages"_ menu, right click the main solution or the _"Packages"_ folder and select _"Restore"_ to ensure the added packages are downloaded.

**Manual installation**

Local ACPPlaces NuGet packages can be created via the included Makefile. If building for the first time, run:

```
make setup
```

followed by:

```
make release
```

The created NuGet packages can be found in the `bin` directory. This directory can be added as a local nuget source and packages within the directory can be added to a Xamarin project following the steps in the "Package Manager Installation" above.

## Usage

The ACPPlaces binding can be opened by loading the ACPPlaces.sln with Visual Studio. The following targets are available in the solution:

- Adobe.ACPPlaces.iOS - The ACPPlaces iOS binding.
- Adobe.ACPPlaces.Android - The ACPCore Android binding.
- ACPPlacesTestApp - The Xamarin.Forms base app used by the iOS and Android test apps.
- ACPPlacesTestApp.iOS - The Xamarin.Forms based iOS manual test app.
- ACPPlacesTestApp.Android - The Xamarin.Forms based Android manual test app.
- ACPPlacesiOSUnitTests - iOS unit test app.
- ACPPlacesAndroidUnitTests - Android unit test app.

### [Places](https://aep-sdks.gitbook.io/docs/using-mobile-extensions/adobe-places)

#### Initialization

##### Before using places
You also need to [initialize Core](https://github.com/adobe/xamarin-acpcore#core) for using Places.

**iOS:**
```c#
// Import the SDK
using Com.Adobe.Marketing.Mobile;

public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
//{Your App related code}

   //Registering Core and Places.
   ACPCore.SetWrapperType(ACPMobileWrapperType.Xamarin);           
   ACPPlaces.RegisterExtension();
   ACPCore.ConfigureWithAppID("{your-launch-id}");
   ACPCore.Start(null);
   
//{Your App related code}
}
```

**Android:**

```c#
// Import the SDK
using Com.Adobe.Marketing.Mobile;

protected override void OnCreate(Bundle savedInstanceState)
{

//{Your App related code}
  ACPCore.SetWrapperType(WrapperType.Xamarin);

   //Registering Core and Places.
    ACPCore.Application = this.Application;
    ACPPlaces.RegisterExtension();
    ACPCore.Start(null);
    ACPCore.ConfigureWithAppID("{your-launch-id}");
            
//{Your App related code}
}
```

#### Places methods

##### Getting Places version:

**iOS and Android**

```c#
ACPPlaces.GetACPPlacesExtensionVersion();
```

##### Getting Current Points of Interests:

**Android**
```c#
ACPPlaces.GetACPPlacesExtensionVersion(IAdobeCallback callback);
```
**iOS** 
```c#
ACPPlaces.GetACPPlacesExtensionVersion(Action<NSArray<ACPPlacesPoi>> callback);
```

##### Getting Nearby Points Of Interests:

**Android**
```c#
Location location = new Location("ACPPlacesTestApp.Xamarin");
//San Jose down town coordinates.
location.Latitude = 37.3309;
location.Longitude = -121.8939;
ACPPlaces.GetNearbyPointsOfInterest(Location location, int count, IAdobeCallback callback);
```
**iOS** and 
```c#
CLLocationCoordinate2D coordinate = new CLLocationCoordinate2D();
coordinate.Latitude = 37.3309;
coordinate.Longitude = -121.8939;
ACPPlaces.GetNearbyPointsOfInterest(CLLocation location, int count, Action<NSArray<ACPPlacesPoi>> callback); //Coordinates of San Jose Downtown.
```

##### Process Geofence():

**Android**

```c#
GeofenceBuilder builder = new GeofenceBuilder();
builder.SetCircularRegion(37.3309, -121.8939, 2000);
builder.SetExpirationDuration(60 * 60 * 100); //one hour
builder.SetRequestId("SanJose Downtown");
builder.SetLoiteringDelay(10000);
builder.SetTransitionTypes(Geofence.GeofenceTransitionEnter);
builder.SetExpirationDuration(50000);
builder.SetNotificationResponsiveness(100);
IGeofence geofence = builder.build();
ACPPlaces.ProcessGeofence(IGeofence geofence, int transitionType);
```

**iOS**

```c#
CLLocationCoordinate2D coordinate = new CLLocationCoordinate2D();
coordinate.Latitude = 37.3309;
coordinate.Longitude = -121.8939;
CLCircularRegion circularRegion = new CLCircularRegion(coordinate, 2000, "ACPPlacesTestApp.xamarin")
ACPPlaces.ProcessRegionEvent(CLCircularRegion circularRegion, ACPRegionEventType regionEventType);
```

##### Geting Last Known Location:

**Android**

```c#
ACPPlaces.GetLastKnownLocation(IAdobeCallback callback);
```

**iOS**

```c#
ACPPlaces.GetLastKnownLocation(Action<CLLocation>);
```

#### Clear

**Android**

```c#
ACPPlaces.Clear();
```

**iOS**

```c#
ACPPlaces.Clear();
```

#### Setting Authorization Status

**Android**

```c#
ACPPlaces.SetAuthorizationStatus(PlacesAuthorizationStatus authorizationStatus);
```

**iOS**

```c#
ACPPlaces.SetAuthorizationStatus(CLAuthorizationStatus authorizationStatus);
```
##### Running Tests

iOS and Android unit tests are included within the ACPPlaces binding solution. They must be built from within Visual Studio then manually triggered from the unit test app that is deployed to an iOS or Android device.

## Sample App

A Xamarin Forms sample app is provided in the Xamarin ACPPlaces solution file.

## Contributing
Looking to contribute to this project? Please review our [Contributing guidelines](.github/CONTRIBUTING.md) prior to opening a pull request.

We look forward to working with you!

## Licensing
This project is licensed under the Apache V2 License. See [LICENSE](LICENSE) for more information.
