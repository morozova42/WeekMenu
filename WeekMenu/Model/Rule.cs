using WeekMenu.Enum;

namespace WeekMenu.Model
{
	public class Rule
	{
		public int Id { get; set; }
		public byte Times { get; set; }
		public byte PerDays { get; set; }
		public Meal Meal { get; set; }
	}
}
