using System.ComponentModel.DataAnnotations;

namespace FirstApiProject.API.Entities
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		[Required]
		[StringLength(50)]
		public string Category { get; set; }
		public string? Description { get; set; }
		[Required]
		public decimal Price { get; set; }
	}
}
