using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using ZXing.Net.Mobile.Forms;

namespace PatientNotes
{
	public partial class PatientNotesListPage : ContentPage
	{
		public PatientNotesListPage()
		{
			InitializeComponent();
		}

	    public static readonly BindableProperty PatientProperty =
	        BindableProperty.Create("Patient", typeof(string), typeof(PatientNotesListPage), null);

	    public string Patient
	    {
	        get { return (string)GetValue(PatientProperty); }
	        set { SetValue(PatientProperty, value); }
	    }

        protected override async void OnAppearing()
		{
			base.OnAppearing();
			listView.ItemsSource = await App.PatientNotesManager.GetAllPatientItemsAsync(Patient);
		}

		async void OnItemAdded(object sender, EventArgs e)
		{
		    await Navigation.PushAsync(new PatientNotesItemPage
		    {
		        PatientNotesItem = new PatientNotesItem {  Patient = Patient}
		    });
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushAsync(new PatientNotesItemPage
			{
				PatientNotesItem = e.SelectedItem as PatientNotesItem
			});
		}

		async void OnRateApplication(object sender, EventArgs e)
		{
			//await Navigation.PushAsync(new RateAppPage());
		}
	}
}
