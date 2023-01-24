using MFramework.Services.FakeData;
using Microsoft.EntityFrameworkCore;

namespace FirstApiProject.API.Entities
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			if (Database.CanConnect())
			{
				if (Products.Any() == false)
				{
					for (int i = 0; i < 10; i++)
					{
						Product product = new Product();
						product.Name = NameData.GetBankName();
						product.Category = NameData.GetDepartmentName();
						product.Description = TextData.GetSentence();
						product.Price = NumberData.GetNumber(1,1000);


						Products.Add(product);
					}

					SaveChanges();
				}
			}
		}

		public DbSet<Product> Products { get; set; }
	}
}
