using Microsoft.AspNetCore.Mvc;
using System.Data;
using WeekMenu.Interfaces;
using WeekMenu.Model;

namespace WeekMenu.Controllers
{
	[Route("api/[controller]")]
	public class ProductController : ControllerBase
	{
		private readonly IRepository<Product> _repository;
		private readonly IValidator<Product> _productValidator;

		public ProductController(
			IRepository<Product> repository,
			IValidator<Product> productValidator)
		{
			_repository = repository;
			_productValidator = productValidator;
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
			var products = await _repository.GetListAsync();
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
			if (id <= 0) { return BadRequest("Wrong id"); }
			var product = await _repository.GetAsync(id);
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
		public async Task<ActionResult> Create(Product product)
		{
			if (_productValidator.Invalid(product)) { return BadRequest(); }
			try
			{
				await _repository.CreateAsync(product);
			}
			catch (DBConcurrencyException ex)
			{
				return NotFound($"It seems like there is no such product: {ex.Message}");
			}
			catch (Exception ex)
			{
				return Problem(ex.Message, nameof(Create));
			}
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
			if (_productValidator.Invalid(product)) { return BadRequest(); }
			await _repository.UpdateAsync(product);
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
		public async Task<ActionResult> Delete([FromRoute] int id)
		{
			try
			{
				await _repository.DeleteAsync(id);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message, nameof(Delete));
			}
			return Ok();
		}
	}
}
