using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Security;
using myProj.Model;
using Ninject.Activation;
using Xamarin.Forms.PlatformConfiguration;
using Context = Android.Content.Context;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDeliveryActivity")]
    public class NewDeliveryActivity : AppCompatActivity, IOnMapReadyCallback , ILocationListener
    {
        public string TAG { get; set; }
        string locationProvider;
         GoogleMap originMap,destinationMap;
        Button saveButton;
        EditText packageNameEditText;
        MapFragment mapFragment,destinationMapFragment;
        double latitude, longitude;
        LocationManager locationManager;

        public void OnLocationChanged(Location location)
        {
            latitude = location.Latitude;
            longitude = location.Longitude;
            mapFragment.GetMapAsync(this);
            destinationMapFragment.GetMapAsync(this);
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            
            if (this.originMap == null)
               this.originMap = googleMap;
            else
              this.destinationMap = googleMap;
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(latitude, longitude));
            marker.SetTitle("Your location");
            googleMap.AddMarker(marker);

            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(latitude, longitude), 10));
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
           
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            // Create your application here
            SetContentView(Resource.Layout.NewDelivery);
            
            saveButton = FindViewById<Button>(Resource.Id.saveButton);
            packageNameEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);
            
            mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.mapFragment);
            destinationMapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.destinationMapFragment);
            
            saveButton.Click += SaveButton_Click;
            InitializeLocationManager();
        }

        

        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            
            
                locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);

                
                
                    var location = locationManager.GetLastKnownLocation(LocationManager.NetworkProvider);
                    if (location != null)
                    {
                        latitude = location.Latitude;
                        longitude = location.Longitude;
                    }


                    mapFragment.GetMapAsync(this);
                    destinationMapFragment.GetMapAsync(this);
                
                
            
        
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            
            var origionLocation = originMap.CameraPosition.Target;

            var destinationLocation = originMap.CameraPosition.Target;

            Delivery delivery = new Delivery()
            {
                Name = packageNameEditText.Text,
                status = 0,
                OriginLatitude = origionLocation.Latitude,
                OriginLongitude = origionLocation.Longitude,
                DestinationLatitude = destinationLocation.Latitude,
                DestinationLongitude = destinationLocation.Longitude
            };  
            await Delivery.InsertDelivery(delivery);
        }

        private void InitializeLocationManager()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);
            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + locationProvider + ".");
        }
    }
}