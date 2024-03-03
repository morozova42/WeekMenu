using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeekMenu.Model;

namespace WeekMenu.Controllers
{
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private MenuDbContext _context;

		public ProductController(MenuDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Gets list of products
		/// </summary>
		/// <remarks>
		/// You can see all the products our app knows about.
		/// </remarks>
		/// <returns>Returns <see cref="Product"/> </returns>
		/// <response code="200">Success</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<Product>> GetProductsList()
		{
			var products = await _context.Products.ToListAsync();
			return Ok(products);
		}
	}
}
