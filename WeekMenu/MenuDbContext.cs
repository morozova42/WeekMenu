using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeekMenu.EtintyTypeConfiguration;
using WeekMenu.Model;

namespace WeekMenu
{
	public class MenuDbContext : DbContext
	{
		public MenuDbContext(DbContextOptions<MenuDbContext> options) : base(options) { }

		public DbSet<Product> Products { get; set; }
		public DbSet<Recipe> Recipes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var configuration = new ConfigurationBuilder()
				.AddUserSecrets<MenuDbContext>()
				.Build();

			optionsBuilder.UseSqlServer(configuration.GetConnectionString("WeekMenuDbEF"));

			Console.WriteLine(configuration.GetDebugView());

			optionsBuilder
			.EnableSensitiveDataLogging(true)
			.LogTo(Console.WriteLine,
					new[] { DbLoggerCategory.Database.Command.Name },
					LogLevel.Information);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
			/*modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new RecipeConfiguration());*/
		}
	}
}
