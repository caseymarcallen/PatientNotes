using SQLite;

namespace PatientNotes
{
	[Table("PatientNotesitem")]
	public class PatientNotesItem
	{
	    public PatientNotesItem()
	    {
	        Patient = "Unknown";
	    }

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
	    public string Patient { get; set; }
    }
}
