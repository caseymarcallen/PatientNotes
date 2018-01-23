using System.Threading.Tasks;

namespace PatientNotes
{
	public interface IBingSpeechService
	{
		Task<SpeechResult> RecognizeSpeechAsync(string filename);
	}
}
