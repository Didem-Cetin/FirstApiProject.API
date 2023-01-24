using FirstApiProject.App.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using System.Net;

namespace FirstApiProject.App.Controllers
{
	public class HomeController : Controller
	{
		RestClient client=new RestClient("http://localhost:5263");

		public IActionResult Index()
		{
			RestRequest request = new RestRequest("/Product/List", Method.Get);
			//RestResponse<Product> product = client.ExecuteGet<Product>(request);
			List<Product> product=client.Get<List<Product>>(request);
			return View(product);
		}

        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
        public IActionResult Create(CreateProductModel model)
        {
			if (ModelState.IsValid)
			{
				RestRequest request = new RestRequest("/Product/Create",Method.Post);

				request.AddJsonBody(model);
				RestResponse<Product> response=client.ExecutePost<Product>(request);
				if (response.StatusCode != HttpStatusCode.Created)
				{
					ModelState.AddModelError("", "Servis Erişim Hatası!");
					return View(model);

				}


			}
            return RedirectToAction(nameof(Index));
        }
		[HttpGet]
        public IActionResult Edit(int id)
        {
			RestRequest request = new RestRequest($"/Product/GetById/{id}", Method.Get);

			RestResponse<Product> response = client.ExecuteGet<Product>(request);

			if (response.StatusCode == HttpStatusCode.OK)
            {
                return View(response.Data);
               
			}

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(int id, Product model)
        {
            RestRequest request = new RestRequest($"/Product/Edit/{id}", Method.Put);

         request.AddJsonBody(model);
			RestResponse response=client.ExecutePut<Product>(request);

			if (response.IsSuccessful)
			{
				return RedirectToAction(nameof(Index));
			}
			else
			{
				ModelState.AddModelError("", "Güncelleme yapılamadı !");
			}

			return View(model);

        }

        public IActionResult Delete(int id)
        {
			RestRequest request = new RestRequest($"/Product/Delete/{id}", Method.Delete);
			RestResponse response = client.Execute(request);

			if (response.StatusCode==HttpStatusCode.NotFound)
			{
				TempData["result"] = "Kayıt bulunamamıştır";
			}
			else
			{
				TempData["result"] = "Kayıt silinmiştir";
			}


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
		{
		
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}