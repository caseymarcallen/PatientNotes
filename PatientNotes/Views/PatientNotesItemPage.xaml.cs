using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace PatientNotes
{
	public partial class PatientNotesItemPage : ContentPage
	{
		IBingSpeechService bingSpeechService;
		IBingSpellCheckService bingSpellCheckService;
		ITextTranslationService textTranslationService;
		bool isRecording = false;

		public static readonly BindableProperty PatientNotesItemProperty =
			BindableProperty.Create("PatientNotesItem", typeof(PatientNotesItem), typeof(PatientNotesItemPage), null);

		public PatientNotesItem PatientNotesItem
		{
			get { return (PatientNotesItem)GetValue(PatientNotesItemProperty); }
			set { SetValue(PatientNotesItemProperty, value); }
	    }

	    public static readonly BindableProperty IsProcessingProperty =
	        BindableProperty.Create("IsProcessing", typeof(bool), typeof(PatientNotesItemPage), false);

	    public bool IsProcessing
	    {
	        get { return (bool)GetValue(IsProcessingProperty); }
	        set { SetValue(IsProcessingProperty, value); }
	    }

	    public static readonly BindableProperty StatusProperty =
	        BindableProperty.Create("Status", typeof(string), typeof(PatientNotesItemPage), "");

	    public string Status
	    {
	        get { return (string)GetValue(StatusProperty); }
	        set { SetValue(StatusProperty, value); }
	    }

        public PatientNotesItemPage()
		{
			InitializeComponent();

			bingSpeechService = new BingSpeechService(new AuthenticationService(Constants.BingSpeechApiKey), Device.RuntimePlatform);
			bingSpellCheckService = new BingSpellCheckService();
			textTranslationService = new TextTranslationService(new AuthenticationService(Constants.TextTranslatorApiKey));
		}

		async void OnRecognizeSpeechButtonClicked(object sender, EventArgs e)
		{
			try
			{
				var audioRecordingService = DependencyService.Get<IAudioRecorderService>();
				if (!isRecording)
				{
					audioRecordingService.StartRecording();
				    Status = "Listening...";
					((Button)sender).Image = "microphone_on.png";
					IsProcessing = true;
				}
				else
				{
					audioRecordingService.StopRecording();
				}

				isRecording = !isRecording;
				if (!isRecording)
				{
				    Status = "Converting to text...";
					var speechResult = await bingSpeechService.RecognizeSpeechAsync(Constants.AudioFilename);
					Debug.WriteLine("Name: " + speechResult.Name);
					Debug.WriteLine("Confidence: " + speechResult.Confidence);

					if (!string.IsNullOrWhiteSpace(speechResult.Name))
					{
						PatientNotesItem.Name = char.ToUpper(speechResult.Name[0]) + speechResult.Name.Substring(1);
						OnPropertyChanged("PatientNotesItem");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
			finally
			{
				if (!isRecording)
				{
				    Status = "";
					((Button)sender).Image = "microphone_off.png";
					IsProcessing = false;
				}
			}
		}

		async void OnSpellCheckButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(PatientNotesItem.Name))
				{
					IsProcessing = true;

					var spellCheckResult = await bingSpellCheckService.SpellCheckTextAsync(PatientNotesItem.Name);
					foreach (var flaggedToken in spellCheckResult.FlaggedTokens)
					{
						PatientNotesItem.Name = PatientNotesItem.Name.Replace(flaggedToken.Token, flaggedToken.Suggestions.FirstOrDefault().Suggestion);
					}
					OnPropertyChanged("PatientNotesItem");

					IsProcessing = false;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		async void OnTranslateButtonClicked(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(PatientNotesItem.Name))
				{
					IsProcessing = true;

					PatientNotesItem.Name = await textTranslationService.TranslateTextAsync(PatientNotesItem.Name);
					OnPropertyChanged("PatientNotesItem");

					IsProcessing = false;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		async void OnSaveClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(PatientNotesItem.Name))
			{
				await App.PatientNotesManager.SaveItemAsync(PatientNotesItem);
			}
			await Navigation.PopAsync();
		}

		async void OnDeleteClicked(object sender, EventArgs e)
		{
			await App.PatientNotesManager.DeleteItemAsync(PatientNotesItem);
			await Navigation.PopAsync();
		}

		async void OnCancelClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}
	}
}
