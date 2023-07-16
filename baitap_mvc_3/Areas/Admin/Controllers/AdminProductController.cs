using baitap_mvc_3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap_mvc_3.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        //Menu
        public ActionResult ProductMenu()
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            List<Product> productList = db.Products.ToList();
            return View(productList);
        }
        //Create
        public ActionResult ProductCreate()
        {
            Product model = new Product();
            model.Sizes.Add(new Size() {});
            return View(model);
        }
        [HttpPost]
        public ActionResult ProductCreate(Product model, List<HttpPostedFileBase> fileImages)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            //add attributes
            model.Quantity = 1;
            db.Products.Add(model);
            //add image
            var path = "";
            foreach(var item in fileImages)
            {
                if(item != null)
                {
                    if (Path.GetExtension(item.FileName).ToLower() == ".jpg" || Path.GetExtension(item.FileName).ToLower() == ".png")
                    {
                        //save image in Data folder
                        path = Path.Combine(Server.MapPath("/Data/")) + item.FileName;
                        item.SaveAs(path);
                        //save image from Data in model
                        Image img = new Image();
                        img.Url = "/Data/" + item.FileName;
                        img.ProductID = model.ID;
                        model.Images.Add(img);
                    }
                }
            }

            db.SaveChanges();
            return RedirectToAction("ProductMenu");
        }
        public ActionResult SizeCreate(int proID)
        {
            return View();
        }
        //Edit
        public ActionResult ProductEdit(int? id)
        {
            if(id == null)
            {
                return View("ProductMenu");
            }
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Product model = db.Products.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult ProductEdit(Product model)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            var productUpdate = db.Products.Find(model.ID);

            //productUpdate.ID = model.ID;
            productUpdate.Name = model.Name.Trim();
            productUpdate.Price = model.Price;
            productUpdate.Color = model.Color.Trim();
            productUpdate.Decription = model.Decription.Trim();
            productUpdate.BrandID = model.BrandID;
            productUpdate.CategoryID = model.CategoryID;

            db.SaveChanges();
            return RedirectToAction("ProductMenu");
        }
        //Delete
        public ActionResult ProductDelete(int id)
        {
            baitap_mvc2Entities db = new baitap_mvc2Entities();
            Product productDel = db.Products.Find(id);
            db.Products.Remove(productDel);
            db.SaveChanges();
            return RedirectToAction("ProductMenu");
        }
    }
}