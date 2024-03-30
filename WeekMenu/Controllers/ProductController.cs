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
		/// <returns>List of <see cref="Product"/> </returns>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Product>> GetProductsList()
		{
			var products = await _context.Products.ToListAsync();
			return Ok(products);
		}

		/// <summary>
		/// Gets product with given id
		/// </summary>
		/// <param name="id">Product's id</param>
		/// <returns><see cref="Product"/></returns>
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<Product>> GetProduct([FromRoute] int id)
		{
			var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
			if (product == null) { return NotFound(); }
			return Ok(product);
		}

		/// <summary>
		/// Adds a <see cref="Product"/> in db
		/// </summary>
		/// <param name="product"><see cref="Product"/> for adding</param>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Create([FromBody] Product product)
		{
			if (product == null) { return BadRequest(); }
			await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		/// <summary>
		/// Modifies <see cref="Product"/> in db
		/// </summary>
		/// <param name="product"><see cref="Product"/> with modified properties</param>
		/// <returns>Modified <see cref="Product"/></returns>
		[HttpPatch]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Modify([FromBody] Product product)
		{
			if (product == null) { return BadRequest(); }
			if (!_context.Products.Any(x => x.Id == product.Id))
			{
				return NotFound();
			}
			_context.Products.Update(product);
			await _context.SaveChangesAsync();
			return Ok(product);
		}

		/// <summary>
		/// Deletes a <see cref="Product"/> with given id
		/// </summary>
		/// <param name="id">Product's id</param>
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult> Delete([FromRoute]int id)
		{
			var productToDelete = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
			if (productToDelete == null) {
				return NotFound();
			}
			_context.Products.Remove(_context.Products.Single(x => x.Id == id));
			await _context.SaveChangesAsync();
			return Ok();
		}
	}
}
