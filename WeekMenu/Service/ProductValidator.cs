using WeekMenu.Interfaces;
using WeekMenu.Model;

namespace WeekMenu.Service
{
	public class ProductValidator : IValidator<Product>
	{
		public bool Invalid(Product product)
		{
			return product == null
				|| string.IsNullOrWhiteSpace(product.Name)
				|| string.IsNullOrWhiteSpace(product.Category);
		}
	}
}
