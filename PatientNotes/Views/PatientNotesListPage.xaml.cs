using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace PatientNotes
{
	public partial class PatientNotesListPage : ContentPage
	{
		public PatientNotesListPage()
		{
			InitializeComponent();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			listView.ItemsSource = await App.PatientNotesManager.GetAllItemsAsync();
		}

		async void OnItemAdded(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new PatientNotesItemPage
			{
				PatientNotesItem = new PatientNotesItem()
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
