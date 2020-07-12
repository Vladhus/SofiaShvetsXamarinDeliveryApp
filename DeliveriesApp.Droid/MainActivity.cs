using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Java.Security;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using myProj.Model;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "DeliveriesApp.Android", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        
        EditText emailEditText,passwordEditText;
        Button loginButton,registerButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
           
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            
            buttonsSetUp();

            loginButton.Click += LoginButton_Click;
            registerButton.Click += RegisterButton_Click;

        }

        private void RegisterButton_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(RegisterActivity));
            intent.PutExtra("email", emailEditText.Text);
            StartActivity(intent);
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {
            var email = emailEditText.Text;
            var password = passwordEditText.Text;
            

            var result = await UserLogin.Login(email, password);
            if (result)
            {
                Toast.MakeText(this, "Welcome", ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(TabsActivity));
                StartActivity(intent);
                Finish();

            }
            else
            {
                Toast.MakeText(this, "Try again later", ToastLength.Long).Show();
            }
        }

        private void buttonsSetUp()
        {
            emailEditText = FindViewById<EditText>(Resource.Id.editEmailText);
            passwordEditText = FindViewById<EditText>(Resource.Id.editPasswordText);
            loginButton = FindViewById<Button>(Resource.Id.signinButton);
            registerButton = FindViewById<Button>(Resource.Id.RegisterButton);
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}