﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;
using Plugin.Media;
using ZXing.Net.Mobile.Android;

namespace PatientNotes.Droid
{
	[Activity(Label = "PatientNotes.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
    }
}
