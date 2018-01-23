using SQLite;

namespace PatientNotes
{
	[Table("PatientNotesitem")]
	public class PatientNotesItem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public bool Done { get; set; }
	}
}
