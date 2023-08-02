using baitap_mvc_3.App_Start;
using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

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
            var userLogin = db.Customers.SingleOrDefault(m => m.Email == email & m.Password == password & m.Status == true);
            if (userLogin != null)
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
        public void UploadFile(Customer model, HttpPostedFileBase imageProfile)
        {
            var path = "";
            if (imageProfile != null)
            {
                if (Path.GetExtension(imageProfile.FileName).ToLower() == ".jpg" || Path.GetExtension(imageProfile.FileName).ToLower() == ".png")
                {
                    //save image in Data folder
                    path = Path.Combine(Server.MapPath("/Data/")) + imageProfile.FileName;
                    imageProfile.SaveAs(path);
                    //save image from Data in model
                    model.Url = "/Data/" + imageProfile.FileName;
                }
            }
        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(Customer model, string passwordRepeat, HttpPostedFileBase imageProfile)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            //filter
            if(db.Customers.Any(m => m.Email == model.Email))
            {
                TempData["customer-email-exist"] = "Email already exist";
                return View();
            }
            if (db.Customers.Any(m => m.Username == model.Username))
            {
                TempData["customer-username-exist"] = "Username already exist";
                return View();
            }
            if (model.Password.IsEmpty())
            {
                TempData["customer-password-error"] = "Enter password";
                return View();
            }
            if (model.Password != passwordRepeat)
            {
                TempData["customer-password-not-match"] = "Password is not match";
                return View();
            }
            if (imageProfile == null)
            {
                TempData["customer-image-null"] = "Choose profile image";
                return View();
            }

            UploadFile(model, imageProfile);
            model.Email.Trim();
            model.Username.Trim();
            model.Password.Trim();
            model.Address.Trim();
            model.Phone.Trim();
            model.Status = true;
            db.Customers.Add(model);
            db.SaveChanges();
            TempData["customer-signup-success"] = "Create account success!!";
            return View("Login");
        }
        [UserAuthenrize]
        public ActionResult UserProfile(int? id)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            if(id != null)
            {
                var model = db.Customers.Find(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult UserProfile(Customer model, HttpPostedFileBase imageProfileUpdate)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Customer userUpdate = db.Customers.Find(model.ID);
            userUpdate.Address = model.Address;
            userUpdate.Phone = model.Phone;
            UploadFile(userUpdate, imageProfileUpdate);
            db.SaveChanges();
            return RedirectToAction("UserProfile", "User", new {id = model.ID});
        }

        [HttpPost]
        public JsonResult PasswordReset(string customerID, string pass, string passNew)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Customer customer = db.Customers.Find(int.Parse(customerID));
            if(customer.Password.Trim() != pass)
            {
                return Json(new { status = "Wrong password" });
            }
            else if(passNew.IsEmpty())
            {
                return Json(new { status = "Enter new password" });
            }
            else
            {
                customer.Password = passNew;
                db.SaveChanges();
                return Json(new { status = "Change password success!" });
            }
        }
    }
}