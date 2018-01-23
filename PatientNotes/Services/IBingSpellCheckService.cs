using System.Threading.Tasks;

namespace PatientNotes
{
	public interface IBingSpellCheckService
	{
		Task<SpellCheckResult> SpellCheckTextAsync(string text);
	}
}
