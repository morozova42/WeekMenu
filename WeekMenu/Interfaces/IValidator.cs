namespace WeekMenu.Interfaces
{
	public interface IValidator<T> where T : class
	{
		/// <summary>
		/// Check if the model is invalid
		/// </summary>
		/// <returns>true if model is invalid</returns>
		public bool Invalid(T model);
	}
}
