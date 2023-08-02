using baitap_mvc_3.App_Start;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.Areas.Admin.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: Admin/AdminUser
        [AdminAuthenrize(idChucnang = 1]
        public ActionResult UserMenu()
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var customerList = db.Customers.ToList();
            return View(customerList);
        }
        [AdminAuthenrize(idChucnang = 3)]
        [HttpPost]
        public JsonResult UserBlock(int customerID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var customer = db.Customers.Find(customerID);
            if(customer.Status == true)
            {
                customer.Status = false;
                db.SaveChanges();
                return Json(new { status = "Block success!!" });
            }
            else
            {
                customer.Status = true;
                db.SaveChanges();
                return Json(new { status = "Unblock success!!" });
            }
        }
    }
}