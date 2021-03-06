﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace PatientNotes
{
	public class PatientNotesItemRepository : IPatientNotesItemRepository
	{
		readonly SQLiteAsyncConnection connection;

		public PatientNotesItemRepository(string dbPath)
		{
			connection = new SQLiteAsyncConnection(dbPath);
			connection.CreateTableAsync<PatientNotesItem>().Wait();
	    }

	    public Task<List<PatientNotesItem>> GetAllItemsAsync()
	    {
	        return connection.Table<PatientNotesItem>().ToListAsync();
	    }

	    public async Task<List<string>> GetAllPatientsAsync()
	    {
	        var items = await connection.Table<PatientNotesItem>().ToListAsync();
	        return items.GroupBy(item => item.Patient).Select(item => item.Key).ToList();
	    }

	    public Task<PatientNotesItem> GetItemAsync(int id)
	    {
	        return connection.Table<PatientNotesItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
	    }

	    public Task<List<PatientNotesItem>> GetAllPatientItemsAsync(string patient)
	    {
	        return connection.Table<PatientNotesItem>().Where(i => i.Patient == patient).ToListAsync();
	    }

        public Task<int> SaveItemAsync(PatientNotesItem item)
		{
			if (item.ID != 0)
			{
				return connection.UpdateAsync(item);
			}
			else
			{
				return connection.InsertAsync(item);
			}
		}

		public Task<int> DeleteItemAsync(PatientNotesItem item)
		{
			return connection.DeleteAsync(item);
		}
	}
}

