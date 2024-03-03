using WeekMenu.Model;

namespace WeekMenu
{
	public static class DbInitializer
	{
		public static void Initialize(MenuDbContext context)
		{
			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();

			if (context.Products.Any())
			{
				return;
			}

			var products = new Product[]
			{
				new Product{ Name = "Ананас", IsSuperFood = false, Category = "Фрукты" },
				new Product{ Name = "Малина", IsSuperFood = false, Category = "Ягоды" },
				new Product{ Name = "Груша зелёная", IsSuperFood = false, Category = "Фрукты" },
				new Product{ Name = "Кефир", IsSuperFood = false, Category = "Молочка" },
				new Product{ Name = "Киноа", IsSuperFood = false, Category = "Крупы" },
				new Product{ Name = "Кофе", IsSuperFood = false, Category = "Напитки" },
				new Product{ Name = "Ламинария", IsSuperFood = false, Category = "Морепродукты" },
				new Product{ Name = "Масло тыквенное", IsSuperFood = false, Category = "Масла" },
				new Product{ Name = "Огурец", IsSuperFood = false, Category = "Овощи" },
				new Product{ Name = "Перец болгарский", IsSuperFood = false, Category = "Овощи" },
				new Product{ Name = "Помидор", IsSuperFood = false, Category = "Овощи" },
				new Product{ Name = "Руккола", IsSuperFood = false, Category = "Зелень" },
				new Product{ Name = "Салат латук", IsSuperFood = false, Category = "Зелень" },
				new Product{ Name = "Укроп", IsSuperFood = false, Category = "Зелень" },
				new Product{ Name = "Фасоль", IsSuperFood = false, Category = "Крупы" },
				new Product{ Name = "Фундук", IsSuperFood = false, Category = "Орехи" },
				new Product{ Name = "Чиа", IsSuperFood = false, Category = "Крупы" },
				new Product{ Name = "Яблоко", IsSuperFood = false, Category = "Фрукты" }
			};

			context.Products.AddRange(products);
			context.SaveChanges();

			var recipes = new Recipe[]
			{
				new Recipe
				{
					Name = "Чиа-пуддинг малиновый",
					Description = "Семена чиа 2 ст л заливаем кефиром 150 мл на ночь. С утра добавляем 100 гр малины и 20 гр орехов на выбор.",
					Products = new List<ProductSet>()
					{
						new ProductSet {Product = products.First(x => x.Name == "Малина"), Amount = 100, Measure = Enum.Unit.Grams},
						new ProductSet {Product = products.First(x => x.Name == "Кефир"), Amount = 150, Measure = Enum.Unit.Milliliters},
						new ProductSet {Product = products.First(x => x.Category == "Орехи"), Amount = 20, Measure = Enum.Unit.Grams}
					}
				},
				new Recipe
				{
					Name = "Киноа с грушей",
					Description = "Варим киноа, 200 гр готовой, в нее мелко рубим половинку зеленой груши, добавляем 5 гр тыквенного масла, укроп 10 гр и рукколу 10 гр",
					Products = new List<ProductSet>()
					{
						new ProductSet {Product = products.First(x => x.Name == "Киноа"), Amount = 200, Measure = Enum.Unit.Grams},
						new ProductSet {Product = products.First(x => x.Name == "Груша зелёная"), Amount = 0.5, Measure = Enum.Unit.Pieces},
						new ProductSet {Product = products.First(x => x.Name == "Масло тыквенное"), Amount = 5, Measure = Enum.Unit.Grams},
						new ProductSet {Product = products.First(x => x.Name == "Руккола"), Amount = 10, Measure = Enum.Unit.Grams},
						new ProductSet {Product = products.First(x => x.Name == "Укроп"), Amount = 10, Measure = Enum.Unit.Grams},
					}
				},
			};

			context.Recipes.AddRange(recipes);
			context.SaveChanges();
		}
	}
}
