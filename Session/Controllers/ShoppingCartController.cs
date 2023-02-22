using Microsoft.AspNetCore.Mvc;

namespace Session.Controllers
{
	/// <summary>
	/// This class takes care of the adding products to the shopping cart
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class ShoppingCartController : ControllerBase
	{
		List<Product> products = new List<Product>();
		
		/// <summary>
		/// This get method is creating a new product from the parameters. Checking if the session shopping cart is already having 
		/// items in it, if yes, then it gets all the items from the shoppingcart in session, and adding all products including the new
		/// product to the new list. Then it returns the new enumerable list of all the products in the shopping cart.
		/// </summary>
		/// <param name="productName"></param>
		/// <param name="productPrice"></param>
		/// <returns></returns>
		[HttpGet(Name = "GetShoppingCart")]
		public IEnumerable<Product> Get(string productName, double productPrice)
		{
			Product newProduct = new Product() { Name = productName.ToLower(), Price = productPrice };
	
			if (HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart") != null)
			{
				products.AddRange(HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart"));
			}
			products.Add(newProduct);

			HttpContext.Session.SetObjectAsJson("ShoppingCart", products);

			return products;
		}
	}
}