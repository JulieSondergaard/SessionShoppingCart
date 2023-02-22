using Microsoft.AspNetCore.Mvc;

namespace Session.Controllers
{

	/// <summary>
	/// This controller takes care of the http method of deleting from the shopping cart in session. The method is taking a
	/// a parameter (product name) and looks for a product name in the list, similar to it, if there is a match, the first
	/// object with that name will be deleted. Furthermore there are responses to empty cart, product not found etc.
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class DeleteController : ControllerBase
	{
		[HttpDelete(Name = "DeleteItemFromCart")]
		public string Delete(string productName)
		{
			bool productRemoved = false;
			string responseString;
			Product removedProduct = new Product();
			List<Product> productsInCart = (List<Product>)HttpContext.Session.GetObjectFromJson<IEnumerable<Product>>("ShoppingCart");

			if (productsInCart != null)
			{
				foreach (Product product in productsInCart)
				{
					if (product.Name.ToLower() == productName.ToLower())
					{
						removedProduct = product;
						productRemoved = true;
					}
				}

				if (productRemoved)
				{
					productsInCart.Remove(removedProduct);
					HttpContext.Session.SetObjectAsJson("ShoppingCart", productsInCart);

					responseString = $"{char.ToUpper(removedProduct.Name[0]) + removedProduct.Name.Substring(1)} was removed from the shopping cart.";

					if (productsInCart.Count > 0)
					{
						responseString += $"\nProducts left in cart:";
						foreach (Product product in productsInCart)
						{
							responseString += $"\n{char.ToUpper(product.Name[0]) + product.Name.Substring(1)}";
						}
					}
					else
					{
						responseString += "\nCart is empty";
					}
				}
				else
				{
					responseString = "Product not found";
				}
			}
			else
			{
				responseString = "No products found in cart.";
			}
			return responseString;
		}
	}
}