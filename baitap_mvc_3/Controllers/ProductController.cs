using baitap_mvc_3.App_Start;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Product model = db.Products.Find(id);
            return View(model);
        }
        public JsonResult IncreaseQuantity(int productID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var product = db.Products.SingleOrDefault(m => m.ID == productID);
            product.Quantity += 1;
            db.SaveChanges();
            return Json(new { quantity = product.Quantity });
        }
        public JsonResult DecreaseQuantity(int productID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var product = db.Products.SingleOrDefault(m => m.ID == productID);
            if (product.Quantity > 1)
            {
                product.Quantity -= 1;
            }
            db.SaveChanges();
            return Json(new { quantity = product.Quantity });
        }
        [HttpPost]
        public JsonResult AddToCart(int? productSize, int productID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            //lấy ra sản phẩm
            var product = db.Products.SingleOrDefault(m => m.ID == productID);
            //lấy ảnh profile của sản phẩm
            var productProfile = db.Images.FirstOrDefault(m => m.ProductID == productID);
            Cartitem cart = new Cartitem
            {
                Name = product.Name,
                Size = productSize,
                Quantity = product.Quantity,
                Price = product.Price,
                Total = product.Price * product.Quantity,
                ProductID = product.ID,
                CustomerID = 3
            };
            if (productProfile != null)
            {
                cart.Url = productProfile.Url;
            }
            db.Cartitems.Add(cart);
            db.SaveChanges();

            // Cập nhật lại giá trị Quantity của sản phẩm thành 1
            product.Quantity = 1;
            db.SaveChanges();

            return Json(new { status = "Đã thêm vào giỏ hàng" });
        }
    }
}