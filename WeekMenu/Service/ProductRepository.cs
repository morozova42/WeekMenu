using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using WeekMenu.Interfaces;
using WeekMenu.Model;

namespace WeekMenu.Service
{
	public class ProductRepository : IRepository<Product>
	{
		private readonly MenuDbContext _context;
		public ProductRepository(MenuDbContext context)
		{
			_context = context;
		}

		public async Task<EntityEntry<Product>> CreateAsync(Product product)
		{
			var entry = await _context.Products.AddAsync(product);
			await _context.SaveChangesAsync();
			return entry;
		}

		public async Task DeleteAsync(int id)
		{
			var productToDelete = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
			if (productToDelete == null)
			{
				throw new KeyNotFoundException("There is no product with such id");
			}
			_context.Products.Remove(productToDelete);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Product>> GetListAsync()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<Product> GetAsync(int id)
		{
			var entry = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
			if (entry == null)
			{
				throw new KeyNotFoundException("There is no product with such id");
			}
			return entry;
		}

		public async Task<EntityEntry<Product>> UpdateAsync(Product product)
		{
			if (!_context.Products.Any(x => x.Id == product.Id))
			{
				throw new KeyNotFoundException("There is no product with such id");
			}
			var entry = _context.Products.Update(product);
			await _context.SaveChangesAsync();
			return entry;
		}
	}
}
