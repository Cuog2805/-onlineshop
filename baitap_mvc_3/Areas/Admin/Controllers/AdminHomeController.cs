using baitap_mvc_3.App_Start;
using baitap_mvc_3.Controllers;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace baitap_mvc_3.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        public ActionResult Index()
        {
            CookieHelper.Create("cookie_test", "admin", DateTime.Now.AddDays(10));
            if (Session["admin"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            //check db
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var nhanvien = db.Nhanviens.SingleOrDefault(m => m.Username == Username && m.Password == Password);


            if(nhanvien != null)
            {
                Session["admin"] = nhanvien;
                CookieHelper.Create("username-cookie", nhanvien.Username, DateTime.Now.AddDays(10));
                CookieHelper.Create("pass-cookie", nhanvien.Password, DateTime.Now.AddDays(10));
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Wrong username or password";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("admin");
            CookieHelper.Remove("username-cookie");
            CookieHelper.Remove("pass-cookie");
            return RedirectToAction("Login");
        }
        public ActionResult PermissionError()
        {
            return View("PermissionError");
        }
    }
}