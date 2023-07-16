using baitap_mvc_3.App_Start;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var userLogin = db.Customers.SingleOrDefault(m => m.Email == email & m.Password == password);
            if(userLogin != null)
            {
                Session["customer-login"] = userLogin;
                CookieHelper.Create("customer-email", userLogin.Email, DateTime.Now.AddDays(10));
                CookieHelper.Create("customer-password", userLogin.Password, DateTime.Now.AddDays(10));
                return RedirectToAction("Index", "Home");
            }  
            else
            {
                TempData["WrongInput"] = "Wrong email or password";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("customer-login");
            CookieHelper.Remove("customer-email");
            CookieHelper.Remove("customer-password");
            return RedirectToAction("Index", "Home");
        }
    }
}