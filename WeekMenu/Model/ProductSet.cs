using WeekMenu.Enum;

namespace WeekMenu.Model
{
	public class ProductSet
	{
		public int Id { get; set; }
		public Product Product { get; set; }
		public double Amount { get; set; }
		public Unit Measure { get; set; }
	}
}
