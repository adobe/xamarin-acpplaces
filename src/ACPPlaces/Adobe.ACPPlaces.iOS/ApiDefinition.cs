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

using ObjCRuntime;
using Foundation;
using CoreLocation;

namespace Com.Adobe.Marketing.Mobile
{
	// @interface ACPPlaces : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPPlaces
	{
		// +(void)clear;
		[Static]
		[Export("clear")]
		void Clear();

		// +(NSString * _Nonnull)extensionVersion;
		[Static]
		[Export("extensionVersion")]
		string ExtensionVersion { get; }

		// +(void)getLastKnownLocation:(void (^ _Nullable)(CLLocation * _Nullable))callback;
		[Static]
		[Export("getLastKnownLocation:")]
		void GetLastKnownLocation([NullAllowed] Action<CLLocation> callback);

		// +(void)getNearbyPointsOfInterest:(CLLocation * _Nonnull)currentLocation limit:(NSUInteger)limit callback:(void (^ _Nullable)(NSArray<ACPPlacesPoi *> * _Nullable))callback;
		[Static]
		[Export("getNearbyPointsOfInterest:limit:callback:")]
		void GetNearbyPointsOfInterest(CLLocation currentLocation, nuint limit, [NullAllowed] Action<NSArray<ACPPlacesPoi>> callback);

		// +(void)getNearbyPointsOfInterest:(CLLocation * _Nonnull)currentLocation limit:(NSUInteger)limit callback:(void (^ _Nullable)(NSArray<ACPPlacesPoi *> * _Nullable))callback errorCallback:(void (^ _Nullable)(ACPPlacesRequestError))errorCallback;
		[Static]
		[Export("getNearbyPointsOfInterest:limit:callback:errorCallback:")]
		void GetNearbyPointsOfInterest(CLLocation currentLocation, nuint limit, [NullAllowed] Action<NSArray<ACPPlacesPoi>> callback, [NullAllowed] Action<ACPPlacesRequestError> errorCallback);

		// +(void)getCurrentPointsOfInterest:(void (^ _Nullable)(NSArray<ACPPlacesPoi *> * _Nullable))callback;
		[Static]
		[Export("getCurrentPointsOfInterest:")]
		void GetCurrentPointsOfInterest([NullAllowed] Action<NSArray<ACPPlacesPoi>> callback);

		// +(void)processRegionEvent:(CLRegion * _Nonnull)region forRegionEventType:(ACPRegionEventType)eventType;
		[Static]
		[Export("processRegionEvent:forRegionEventType:")]
		void ProcessRegionEvent(CLRegion region, ACPRegionEventType eventType);

		// +(void)registerExtension;
		[Static]
		[Export("registerExtension")]
		void RegisterExtension();

		// +(void)setAuthorizationStatus:(CLAuthorizationStatus)status;
		[Static]
		[Export("setAuthorizationStatus:")]
		void SetAuthorizationStatus(CLAuthorizationStatus status);
	}

	// @interface ACPPlacesPoi : NSObject
	[BaseType(typeof(NSObject))]
	interface ACPPlacesPoi : INativeObject
	{
		// @property (nonatomic, strong) NSString * _Nullable identifier;
		[NullAllowed, Export("identifier", ArgumentSemantic.Strong)]
		string Identifier { get; set; }

		// @property (nonatomic, strong) NSString * _Nullable name;
		[NullAllowed, Export("name", ArgumentSemantic.Strong)]
		string Name { get; set; }

		// @property (nonatomic) double latitude;
		[Export("latitude")]
		double Latitude { get; set; }

		// @property (nonatomic) double longitude;
		[Export("longitude")]
		double Longitude { get; set; }

		// @property (nonatomic) NSUInteger radius;
		[Export("radius")]
		nuint Radius { get; set; }

		// @property (nonatomic, strong) NSDictionary<NSString *,NSString *> * _Nullable metaData;
		[NullAllowed, Export("metaData", ArgumentSemantic.Strong)]
		NSDictionary<NSString, NSString> MetaData { get; set; }

		// @property (nonatomic) Boolean userIsWithin;
		[Export("userIsWithin")]
		byte UserIsWithin { get; set; }
	}
}