using System.Threading.Tasks;

namespace PatientNotes
{
	public interface ITextTranslationService
	{
		Task<string> TranslateTextAsync(string text);
	}
}
