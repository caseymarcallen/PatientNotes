using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PatientNotes
{
	public partial class App : Application
	{
		static IPatientNotesItemRepository PatientNotesItemRepository;

		public static IPatientNotesItemRepository PatientNotesManager
		{
			get
			{
				if (PatientNotesItemRepository == null)
				{
					PatientNotesItemRepository = new PatientNotesItemRepository(DependencyService.Get<IFileHelper>().GetLocalFilePath("PatientNotesSQLite.db3"));
				}
				return PatientNotesItemRepository;
			}
		}

		public App()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new PatientNotesListPage()) { BarBackgroundColor = Color.FromHex("#F35220")};
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
