using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeekMenu.Model;

namespace WeekMenu.EtintyTypeConfiguration
{
	public class ProductSetConfiguration : IEntityTypeConfiguration<ProductSet>
	{
		public void Configure(EntityTypeBuilder<ProductSet> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.Id).IsUnique();
			builder.Property(x => x.Measure).HasConversion<string>();
		}
	}
}