using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Threading.Tasks;
using baitap_mvc_3.Models;
using baitap_mvc_3.App_Start;
using System.Web.WebPages;

namespace baitap_mvc_3.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(string sortOrder, string search = "")
		{
			baitap_mvc2Entities db = new baitap_mvc2Entities();
			//Search
			List<Product> productList = db.Products.Where(m => m.Name.Contains(search)).ToList();
			foreach(var item in productList)
			{
				item.Quantity = 1;
			}

            //Sort
            if (sortOrder == "asc")
			{
				productList = db.Products.OrderBy(m => m.Price).ToList();
			}
			else if(sortOrder == "desc")
			{
				productList = db.Products.OrderByDescending(m => m.Price).ToList();
			}

			db.SaveChanges();
			return View(productList);
		}
		public ActionResult Brand(string brandName, string sortOrder)
		{
            baitap_mvc2Entities db = new baitap_mvc2Entities();
			if (brandName.IsEmpty())
			{
				return View("Error");
			}
			List<Product> productList = db.Products.Where(m => m.Brand.Name == brandName).ToList();
			ViewBag.BrandName = brandName;

            //Sort
            if (sortOrder == "asc")
            {
                productList = productList.OrderBy(m => m.Price).ToList();
            }
            else if (sortOrder == "desc")
            {
                productList = productList.OrderByDescending(m => m.Price).ToList();
            }

            return View(productList);
		}
        public ActionResult Category(string categoryName, string sortOrder)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            if (categoryName.IsEmpty())
            {
                return View("Error");
            }
            List<Product> productList = db.Products.Where(m => m.Category.Name == categoryName).ToList();
            ViewBag.Category = categoryName;

            //Sort
            if (sortOrder == "asc")
            {
                productList = productList.OrderBy(m => m.Price).ToList();
            }
            else if (sortOrder == "desc")
            {
                productList = productList.OrderByDescending(m => m.Price).ToList();
            }

            return View(productList);
        }
    }
}