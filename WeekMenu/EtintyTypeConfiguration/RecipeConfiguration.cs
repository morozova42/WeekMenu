using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeekMenu.Model;

namespace WeekMenu.EtintyTypeConfiguration
{
	public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
	{
		public void Configure(EntityTypeBuilder<Recipe> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.Id).IsUnique();
		}
	}
}
 