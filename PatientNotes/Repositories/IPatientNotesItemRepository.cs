using System.Collections.Generic;
using System.Threading.Tasks;

namespace PatientNotes
{
	public interface IPatientNotesItemRepository
	{
		Task<List<PatientNotesItem>> GetAllItemsAsync();
		Task<PatientNotesItem> GetItemAsync(int id);
		Task<int> SaveItemAsync(PatientNotesItem item);
		Task<int> DeleteItemAsync(PatientNotesItem item);
	}
}
