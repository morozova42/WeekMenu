using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WeekMenu.Interfaces
{
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// Gets list of entries
		/// </summary>
		public Task<List<T>> GetListAsync();

		/// <summary>
		/// Gets an entry with given id
		/// </summary>
		/// <param name="id">Entry's id</param>
		/// <exception cref="KeyNotFoundException"></exception>
		public Task<T> GetAsync(int id);

		/// <summary>
		/// Creates an entry
		/// </summary>
		/// <param name="entity">Entity to create</param>
		public Task<EntityEntry<T>> CreateAsync(T entity);

		/// <summary>
		/// Updates an entry
		/// </summary>
		/// <param name="entity">Entity to update</param>
		/// <returns></returns>
		public Task<EntityEntry<T>> UpdateAsync(T entity);

		/// <summary>
		/// Deletes an entry with given id
		/// </summary>
		/// <param name="id">Entry's id</param>
		/// <exception cref="KeyNotFoundException"></exception>
		public Task DeleteAsync(int id);
	}
}
