using baitap_mvc_3.App_Start;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        [UserAuthenrize]
        public ActionResult CartIndex(int? customerID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            if(customerID == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cartitem> cartItemList = db.Cartitems.Where(m => m.CustomerID == customerID).ToList();
            return View(cartItemList);
        }
        public ActionResult CartDelete(int id)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var cartDelete = db.Cartitems.Find(id);
            db.Cartitems.Remove(cartDelete);
            db.SaveChanges();
            return RedirectToAction("CartIndex");
        }
        [HttpPost]
        public JsonResult IncreaseQuantity(int cartItemID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var cart = db.Cartitems.SingleOrDefault(m => m.ID == cartItemID);
            cart.Quantity += 1;
            cart.Total = cart.Quantity * cart.Price;
            db.SaveChanges();
            var totalSum = db.Cartitems.Sum(m => m.Total);
            return Json(new { quantity = cart.Quantity, total = cart.Total, totalSummary = totalSum });
        }
        [HttpPost]
        public JsonResult DecreaseQuantity(int cartItemID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var cart = db.Cartitems.SingleOrDefault(m => m.ID == cartItemID);
            if (cart.Quantity > 1)
            {
                cart.Quantity -= 1;
            }
            cart.Total = cart.Quantity * cart.Price;
            db.SaveChanges();
            var totalSum = db.Cartitems.Sum(m => m.Total);
            return Json(new { quantity = cart.Quantity, total = cart.Total, totalSummary = totalSum });
        }
        [HttpPost]
        public JsonResult ChooseSize(int productSize, int productID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var cart = db.Cartitems.SingleOrDefault(m => m.ProductID == productID);
            cart.Size = productSize;
            db.SaveChanges();
            return Json(new { status = "đã chọn lại Size!" });
        }
    }
}