using FirstApiProject.API.Entities;
using FirstApiProject.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiProject.API.Controllers
{
	[Route("[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private DatabaseContext _databaseContext;

		public ProductController(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}
		[HttpGet]
		public IActionResult List()
		{
			return Ok(_databaseContext.Products.ToList());  
		}

		[HttpGet("{Id}")]
		public IActionResult GetById(int id)
		{
			//Product productId = _databaseContext.Products.FirstOrDefault(x=>x.Id==id);

			Product productId = _databaseContext.Products.Find(id);
			if (productId==null)
			{
				return NotFound(id);
			}
			return Ok(productId);
		}

		[HttpPost]
		public IActionResult Create(CreatePruductModel model)
		{
			if (model.Name == "Dido")
			{
			return BadRequest(model);
			}

			Product product = new()
			{
				Name = model.Name,
				Category= model.Category,
				Description= model.Description,
				Price= model.Price
			};

			_databaseContext.Products.Add(product);
			_databaseContext.SaveChanges();

			return Created("", model);
			
		}

		[HttpPut("{id}")]
		public IActionResult Edit([FromRoute] int id, [FromBody] UpdatePruductModel model)
		{
			Product product = _databaseContext.Products.Find(id);

			if (product==null)
			{
				return NotFound();
			}

			product.Name = model.Name;
			product.Category = model.Category;
			product.Description = model.Description;
			product.Price = model.Price;

			_databaseContext.SaveChanges();
			return Ok(product);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete([FromRoute] int id)
		{
			Product product = _databaseContext.Products.Find(id);
			if (product ==null)
			{
				return NotFound();
			}
			_databaseContext.Products.Remove(product);
			_databaseContext.SaveChanges();
			return Ok();
		}

	}
}
