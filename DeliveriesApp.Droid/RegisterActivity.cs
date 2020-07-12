using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using myProj.Model;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText emailEditText, passwordEditText, confirmPasswordEditText;
        Button registerUserButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            emailEditText = FindViewById<EditText>(Resource.Id.rpEditEmailText);
            passwordEditText = FindViewById<EditText>(Resource.Id.rpEditPasswordText);
            confirmPasswordEditText = FindViewById<EditText>(Resource.Id.rpConfirmEditPasswordText);
            registerUserButton = FindViewById<Button>(Resource.Id.rpRegisterUserButton);

            registerUserButton.Click += RegisterUserButton_Click;

            string email = Intent.GetStringExtra("email");
            emailEditText.Text = email;

        }

        private async void RegisterUserButton_Click(object sender, EventArgs e)
        {
            var result = await UserLogin.RegisterUser(emailEditText.Text, passwordEditText.Text, confirmPasswordEditText.Text);
            if (result)
            {
                Toast.MakeText(this, "Success", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Try again", ToastLength.Long).Show();
            }
        }
    }
}