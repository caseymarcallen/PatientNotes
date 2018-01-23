using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using ZXing.Net.Mobile.Forms;

namespace PatientNotes
{
	public partial class PatientListPage : ContentPage
	{
		public PatientListPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			listView.ItemsSource = await App.PatientNotesManager.GetAllPatientsAsync();
		}

		async void OnItemAdded(object sender, EventArgs e)
		{
		    var scanPage = new ZXingScannerPage();
		    scanPage.OnScanResult += (result) =>
		    {
		        // Stop scanning
		        scanPage.IsScanning = false;

		        // Pop the page and show the result
		        Device.BeginInvokeOnMainThread(async () =>
		        {
		            await Navigation.PopAsync();

		            if (string.IsNullOrEmpty(result.Text))
		            {
		                await DisplayAlert("Error", "No Patient Barcode Found", "OK");
                    }
                    else
                    { 
		                await Navigation.PushAsync(new PatientNotesItemPage
		                {
		                    PatientNotesItem = new PatientNotesItem {  Patient = result.Text}
		                });
		            }
		        });
		    };
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
            
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushAsync(new PatientNotesListPage
			{
				Patient = e.SelectedItem?.ToString()
			});
		}
	}
}
