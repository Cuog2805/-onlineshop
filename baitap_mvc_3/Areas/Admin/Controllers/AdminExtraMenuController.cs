using baitap_mvc_3.Models;
using System.IO;
using System.Web.Mvc;

namespace baitap_mvc_3.Areas.Admin.Controllers
{
    public class AdminExtraMenuController : Controller
    {
        // GET: Admin/AdminExtraMenu
        public ActionResult SizeDelete(int sizeID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var sizeDelete = db.Sizes.Find(sizeID);
            db.Sizes.Remove(sizeDelete);
            db.SaveChanges();
            return Redirect("/Admin/AdminHome/Index");
        }
        [HttpPost]
        public JsonResult SizeAdd(int sizeInfo)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var sizeNew = new Size();
            sizeNew.Size1 = sizeInfo;
            db.Sizes.Add(sizeNew);
            db.SaveChanges();
            return Json(new { sizeID = sizeNew.ID, sizeInfo = sizeNew.Size1});
        }
        public ActionResult BrandDelete(int brandID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var brandDelete = db.Brands.Find(brandID);
            db.Brands.Remove(brandDelete);
            db.SaveChanges();
            return Redirect("/Admin/AdminHome/Index");
        }
        [HttpPost]
        public JsonResult BrandAdd(string brandInfo)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var brandNew = new Brand();
            brandNew.Name = brandInfo;
            db.Brands.Add(brandNew);
            db.SaveChanges();
            return Json(new { brandID = brandNew.ID, brandInfo = brandNew.Name});
        }
        public ActionResult CategoryDelete(int categoryID)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var categoryDelete = db.Categories.Find(categoryID);
            db.Categories.Remove(categoryDelete);
            db.SaveChanges();
            return Redirect("/Admin/AdminHome/Index");
        }
        [HttpPost]
        public JsonResult CategoryAdd(string categoryInfo)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var categoryNew = new Category();
            categoryNew.Name = categoryInfo;
            db.Categories.Add(categoryNew);
            db.SaveChanges();
            return Json(new { categoryID = categoryNew.ID, categoryInfo = categoryNew.Name });
        }
    }
}