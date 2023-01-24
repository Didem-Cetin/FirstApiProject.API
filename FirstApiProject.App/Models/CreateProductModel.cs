using System.ComponentModel.DataAnnotations;

namespace FirstApiProject.App.Models
{
	public class CreateProductModel
	{

		public string Name { get; set; }

		public string Category { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
	}
}
