using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Gms.Maps;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using myProj.Model;
using Android.Gms.Maps.Model;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "PickUpActivity")]
    public class PickUpActivity : Activity,IOnMapReadyCallback
    {
        Button pickUPbutton;
        MapFragment mapFragment;
        double lat, lng;
        string userId, deliveryId;

        public void OnMapReady(GoogleMap googleMap)
        {
            MarkerOptions marker = new MarkerOptions();
            marker.SetPosition(new LatLng(lat, lng));
            marker.SetTitle("Pick UP here");
            googleMap.AddMarker(marker);

            googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(lat, lng), 12));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.PickUpLayout);

            mapFragment = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.pickupMapFragment);
            pickUPbutton = FindViewById<Button>(Resource.Id.pickUpButton);

            pickUPbutton.Click += PickUPbutton_Click;

            lat = Intent.GetDoubleExtra("latitude", 0);
            lng = Intent.GetDoubleExtra("longitude", 0);
            userId = Intent.GetStringExtra("userId");
            deliveryId = Intent.GetStringExtra("deliveryId");

            mapFragment.GetMapAsync(this);

        }
        
        private async void PickUPbutton_Click(object sender, EventArgs e)
        {
            await Delivery.MarkAsPickerUp(deliveryId, userId);
        }
    }
}