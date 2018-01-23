using System.Threading.Tasks;

namespace PatientNotes
{
	public interface IAuthenticationService
	{
		Task InitializeAsync();
		string GetAccessToken();
	}
}
