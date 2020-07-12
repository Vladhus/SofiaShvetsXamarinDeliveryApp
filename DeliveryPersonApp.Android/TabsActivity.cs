using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.Design.Widget;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.AccessibilityServices;
using Android.Content.Res;

namespace DeliveryPersonApp.Android
{
    [Activity(Label = "TabsActivity")]
    public class TabsActivity : global::Android.Support.V4.App.FragmentActivity
    {
        TabLayout tabLayout;
        public string userId;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tabs);

            userId = Intent.GetStringExtra("userId");

            
            tabLayout = FindViewById<TabLayout>(Resource.Id.mainTabLayout1);
            tabLayout.TabSelected += TabLayout_TabSelected;

            TabNavigation(new DeliveringFragment());
        }

        private void TabLayout_TabSelected(object sender, TabLayout.TabSelectedEventArgs e)
        {
            switch (e.Tab.Position)
            {
                case 0:
                    TabNavigation(new DeliveringFragment());
                    break;
                case 1:
                    TabNavigation(new WaitingFragment());
                    break;
                case 2:
                    TabNavigation(new DeliveredFragment());
                    break;
            }
        }

        private void TabNavigation(global::Android.Support.V4.App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.contentFrame, fragment);
            transaction.Commit();
        }
    }
}