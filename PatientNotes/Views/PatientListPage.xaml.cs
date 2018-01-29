using System;
using System.Linq;
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
            var patients = await App.PatientNotesManager.GetAllPatientsAsync();
		    patients = patients.Select(p => string.IsNullOrEmpty(p) ? "Unknown" : p).ToList();

		    listView.ItemsSource = patients;
            
		    listView.SelectedItem = null;
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
	        var patientName = e.SelectedItem?.ToString();

	        if (patientName != null)
	        {
	            if (patientName == "Unknown")
	            {
	                patientName = null;
	            }

	            await Navigation.PushAsync(new PatientNotesListPage
	            {
	                Patient = patientName
	            });
	        }
	    }
	}
}
