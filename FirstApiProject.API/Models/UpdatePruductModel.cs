namespace FirstApiProject.API.Models
{
	public class UpdatePruductModel
	{
		public string Name { get; set; }

		public string Category { get; set; }
		public string? Description { get; set; }
		public decimal Price { get; set; }
	}
}
